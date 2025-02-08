using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DWSIM.FileStorage;
using DWSIM.AI.ConvergenceAssistant.Classes;
using Eto.Forms;
using System.Runtime.InteropServices.ComTypes;
using DWSIM.AI.ConvergenceAssistant.ANN;
using Tensorflow;
using System.Timers;

namespace DWSIM.AI.ConvergenceAssistant
{
    public class ActivityUpdateEventArgs
    {
        public ActivityUpdateEventArgs(string text) { Text = text; }
        public string Text { get; } // readonly
    }

    public class ManagerSettings
    {
        public bool AutoUpdateEnabled { get; set; } = true;

        public int DatabaseSaveThreshold { get; set; } = 1000;

        public int PTFlashTrainThreshold { get; set; } = 1000;

        public int PVFlashTrainThreshold { get; set; } = 1000;

        public int TVFlashTrainThreshold { get; set; } = 1000;

        public int PHFlashTrainThreshold { get; set; } = 1000;

        public int PSFlashTrainThreshold { get; set; } = 1000;

        public int GITrainThreshold { get; set; } = 100;

        public int GATrainThreshold { get; set; } = 100;

        public int EITrainThreshold { get; set; } = 100;

        public int EATrainThreshold { get; set; } = 100;

        public string HomeDirectory = Path.Combine(GlobalSettings.Settings.GetConfigFileDir(), "ConvergenceHelper");

        public int UpdateTimerInterval = 60;

    }

    public class Manager
    {

        public static ManagerSettings Settings = new ManagerSettings();

        public static FileDatabaseProvider Database = new FileDatabaseProvider();

        public static List<ConvergenceHelperMetaData> ModelsSummary = new List<ConvergenceHelperMetaData>();

        public static bool Initialized = false;

        public static Dictionary<string, ANNModel> LoadedModels = new Dictionary<string, ANNModel>();

        // Declare the delegate (if using non-generic pattern).
        public delegate void ActivityUpdateEventHandler(object sender, ActivityUpdateEventArgs e);

        public static event ActivityUpdateEventHandler ActivityUpdate;

        private static int LastTrainDataCount = 0;

        public static Timer UpdateTimer;

        public static void Initialize()
        {

            if (!Directory.Exists(Settings.HomeDirectory)) { Directory.CreateDirectory(Settings.HomeDirectory); }
            var datadir = Path.Combine(Settings.HomeDirectory, "data");
            if (!Directory.Exists(datadir)) { Directory.CreateDirectory(datadir); }
            var modelsdir = Path.Combine(Settings.HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }
            var configdir = Path.Combine(Settings.HomeDirectory, "config");
            if (!Directory.Exists(configdir)) { Directory.CreateDirectory(configdir); }
            LoadSettings();

            var dbfile = Path.Combine(datadir, "data.db.zip");
            if (!File.Exists(dbfile))
            {
                Database.CreateDatabase();
                Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            }
            else
            {
                var dbfile2 = Path.Combine(datadir, "data.db");
                if (File.Exists(dbfile2)) File.Delete(dbfile2);
                ZipFile.ExtractToDirectory(dbfile, datadir);
                Database.LoadDatabase(dbfile2);
                File.Delete(dbfile2);
            }

            var msfile = Path.Combine(modelsdir, "summary.json");
            if (!File.Exists(msfile))
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(ModelsSummary, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(msfile, data);
            }
            else
            {
                ModelsSummary = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConvergenceHelperMetaData>>(File.ReadAllText(msfile));
            }

            var col = Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            LastTrainDataCount = col.Query().Count();

            UpdateTimer = new Timer(Settings.UpdateTimerInterval * 1000);
            UpdateTimer.Elapsed += (s, e) =>
            {
                if (Settings.AutoUpdateEnabled) UpdateModels();
                SaveSettings();
            };
            UpdateTimer.Start();

            Initialized = true;

        }

        public static void UpdateModels(TextArea ta = null, Eto.OxyPlot.Plot plot = null)
        {
            var col = Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var newcount = col.Query().Count();
            if (newcount - LastTrainDataCount > Settings.DatabaseSaveThreshold)
            {
                Task.Run(() =>
                {
                    ModelUpdater.UpdateAll(ta, plot);
                    LastTrainDataCount = newcount;
                }).ContinueWith(t =>
                {
                    if (t.Exception == null) SaveDatabaseToFile();
                });
            }
        }

        public static void StoreData(ConvergenceHelperTrainingData data)
        {
            Task.Run(() =>
            {
                lock (Database)
                {
                    var comps = data.CompoundNames.OrderBy(x => x).ToList();
                    var comps0 = data.CompoundNames.ToList();
                    var mf1 = new List<string>();
                    var mf2 = new List<string>();
                    var vf = new List<string>();
                    var lf1 = new List<string>();
                    var lf2 = new List<string>();
                    var sf = new List<string>();
                    var k1 = new List<string>();
                    var k2 = new List<string>();
                    foreach (var comp in comps)
                    {
                        mf1.Add(data.MixtureMolarFlows[comps0.IndexOf(comp)]);
                        if (data.MixtureMolarFlows2 != null) if (data.MixtureMolarFlows2 != null) mf2.Add(data.MixtureMolarFlows2[comps0.IndexOf(comp)]);
                        if (data.VaporMolarFlows != null) vf.Add(data.VaporMolarFlows[comps0.IndexOf(comp)]);
                        if (data.Liquid1MolarFlows != null) lf1.Add(data.Liquid1MolarFlows[comps0.IndexOf(comp)]);
                        if (data.Liquid2MolarFlows != null) lf2.Add(data.Liquid2MolarFlows[comps0.IndexOf(comp)]);
                        if (data.SolidMolarFlows != null) sf.Add(data.SolidMolarFlows[comps0.IndexOf(comp)]);
                        if (data.KValuesVL1 != null) k1.Add(data.KValuesVL1[comps0.IndexOf(comp)]);
                        if (data.KValuesVL2 != null) k2.Add(data.KValuesVL2[comps0.IndexOf(comp)]);
                    }
                    data.CompoundNames = comps.ToArray();
                    data.MixtureMolarFlows = mf1.ToArray();
                    if (data.MixtureMolarFlows2 != null) data.MixtureMolarFlows2 = mf2.ToArray();
                    if (data.VaporMolarFlows != null) data.VaporMolarFlows = vf.ToArray();
                    if (data.Liquid1MolarFlows != null) data.Liquid1MolarFlows = lf1.ToArray();
                    if (data.Liquid2MolarFlows != null) data.Liquid2MolarFlows = lf2.ToArray();
                    if (data.SolidMolarFlows != null) data.SolidMolarFlows = sf.ToArray();
                    if (data.KValuesVL1 != null) data.KValuesVL1 = k1.ToArray();
                    if (data.KValuesVL2 != null) data.KValuesVL2 = k2.ToArray();
                    data.Hash = data.GetBase64StringHash();
                    var col = Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
                    var entries = col.Query().Where(x => x.Hash == data.Hash);
                    if (entries.Count() == 0)
                    {
                        col.Insert(data);
                    }
                }
            });
        }

        public static void SaveDatabaseToFile()
        {
            var datadir = Path.Combine(Settings.HomeDirectory, "data");
            var zipfile = Path.Combine(datadir, "data.db.zip");
            var dbfile = Path.Combine(datadir, "data.db");
            Database.ExportDatabase(dbfile);
            using (var fstream = new FileStream(zipfile, FileMode.OpenOrCreate))
            {
                fstream.Position = 0;
                using (var archive = new ZipArchive(fstream, ZipArchiveMode.Create))
                    ZipFileExtensions.CreateEntryFromFile(archive, dbfile, "data.db", CompressionLevel.Optimal);
            }
            File.Delete(dbfile);
        }

        public static void LoadSettings()
        {
            var configfile = Path.Combine(Settings.HomeDirectory, "config", "settings.json");
            if (File.Exists(configfile))
            {
                Settings = Newtonsoft.Json.JsonConvert.DeserializeObject<ManagerSettings>(File.ReadAllText(configfile));
            }
        }

        public static void SaveSettings()
        {
            var configfile = Path.Combine(Settings.HomeDirectory, "config", "settings.json");
            File.WriteAllText(configfile, Newtonsoft.Json.JsonConvert.SerializeObject(Settings, Newtonsoft.Json.Formatting.Indented));
        }

        public static void AddToSummary(ConvergenceHelperMetaData mdata)
        {
            var modelsdir = Path.Combine(Settings.HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            ModelsSummary.Add(mdata);
            var msfile = Path.Combine(modelsdir, "summary.json");

            File.WriteAllText(msfile, Newtonsoft.Json.JsonConvert.SerializeObject(ModelsSummary, Newtonsoft.Json.Formatting.Indented));
        }

        public static void SaveModelToFile(ANNModel model)
        {
            var modelsdir = Path.Combine(Settings.HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            var zipfile = Path.Combine(modelsdir, model.MetaData.ModelName + ".zip");
            var modelfile = Path.Combine(modelsdir, model.MetaData.ModelName + ".json");

            using (var ms = new MemoryStream())
            {
                Utils.SaveGraphToZipStream(model.session, model, ms);
                ms.Position = 0;
                model.SerializedModelData = Utils.StreamToBase64(ms);
            }

            var contents = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(modelfile, contents);

            using (var fstream = new FileStream(zipfile, FileMode.OpenOrCreate))
            {
                fstream.Position = 0;
                using (var archive = new ZipArchive(fstream, ZipArchiveMode.Create))
                    ZipFileExtensions.CreateEntryFromFile(archive, modelfile, Path.GetFileName(modelfile), CompressionLevel.Optimal);
            }

            AddToSummary(model.MetaData);

            File.Delete(modelfile);
        }

        public static ANNModel LoadModelFromFile(string modelfilepath)
        {
            var modelsdir = Path.Combine(Settings.HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            if (File.Exists(modelfilepath))
            {
                var modelfile2 = Path.Combine(modelsdir, Path.ChangeExtension(modelfilepath, "json"));
                if (File.Exists(modelfile2)) File.Delete(modelfile2);
                ZipFile.ExtractToDirectory(modelfilepath, modelsdir);
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ANN.ANNModel>(File.ReadAllText(modelfile2));
                File.Delete(modelfile2);
                return model;
            }

            return null;

        }

        public static ANNModel GetModel(ConvergenceHelperRequest request)
        {

            var comps = request.CompoundNames.OrderBy(x => x).ToList();
            var comps0 = request.CompoundNames.ToList();
            var mf1 = new List<double>();
            foreach (var comp in comps)
            {
                mf1.Add(request.MixtureMolarFlows[comps0.IndexOf(comp)]);
            }

            var modeldata = ModelsSummary.Where(m => m.CompoundNames.SequenceEqual(comps) &&
                        m.PropertyPackageName == request.ModelName &&
                        !Double.IsNaN(m.TrainingDataMSE)).OrderBy(m => m.TestingDataMSE).FirstOrDefault();

            if (modeldata == null) { return null; }

            if (LoadedModels.ContainsKey(modeldata.ModelName)) return LoadedModels[modeldata.ModelName];

            var modelsdir = Path.Combine(Settings.HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            var model = LoadModelFromFile(Path.Combine(modelsdir, modeldata.ModelName + ".zip"));

            if (!LoadedModels.ContainsKey(model.MetaData.ModelName)) LoadedModels.Add(model.MetaData.ModelName, model);

            return model;

        }

    }
}
