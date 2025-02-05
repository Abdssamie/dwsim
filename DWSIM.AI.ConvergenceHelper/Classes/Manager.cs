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

namespace DWSIM.AI.ConvergenceAssistant
{
    public class Manager
    {

        public static FileDatabaseProvider Database = new FileDatabaseProvider();

        public static string HomeDirectory = Path.Combine(GlobalSettings.Settings.GetConfigFileDir(), "ConvergenceHelper");

        public static List<ConvergenceHelperMetaData> ModelsSummary = new List<ConvergenceHelperMetaData>();

        public static bool Initialized = false;

        public static void Initialize()
        {
            if (!Directory.Exists(HomeDirectory)) { Directory.CreateDirectory(HomeDirectory); }
            var datadir = Path.Combine(HomeDirectory, "data");
            if (!Directory.Exists(datadir)) { Directory.CreateDirectory(datadir); }
            var modelsdir = Path.Combine(HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }
            var configdir = Path.Combine(HomeDirectory, "config");
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

            FlowsheetSolver.FlowsheetSolver.FlowsheetCalculationFinished += FlowsheetSolver_FlowsheetCalculationFinished;

            Initialized = true;

        }

        private static void FlowsheetSolver_FlowsheetCalculationFinished(object sender, EventArgs e, object extrainfo)
        {
            if (GlobalSettings.Settings.AIAssistedConvergenceLevel > 0)
            {
                Task.Run(() => SaveDatabaseToFile());
                //UpdateModels();
            }
        }

        public static void UpdateModels(TextArea ta = null, Eto.OxyPlot.Plot plot = null)
        {
            var col = Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.PTFlash).ToList();

            ModelUpdater.UpdatePTModels(ta, plot);

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
            var datadir = Path.Combine(HomeDirectory, "data");
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
            var configfile = Path.Combine(HomeDirectory, "config", "settings.json");
            if (File.Exists(configfile))
            {

            }
        }

        public static void SaveSettings()
        {

        }

        public static void AddToSummary(ConvergenceHelperMetaData mdata)
        {
            var modelsdir = Path.Combine(HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            ModelsSummary.Add(mdata);
            var msfile = Path.Combine(modelsdir, "summary.json");

            File.WriteAllText(msfile, Newtonsoft.Json.JsonConvert.SerializeObject(ModelsSummary, Newtonsoft.Json.Formatting.Indented));
        }

        public static void SaveModelToFile(ANNModel model)
        {
            var modelsdir = Path.Combine(HomeDirectory, "models");
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
            var modelsdir = Path.Combine(HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            if (File.Exists(modelfilepath))
            {
                var modelfile2 = Path.Combine(modelsdir, Path.ChangeExtension(modelfilepath, "json"));
                if (File.Exists(modelfile2)) File.Delete(modelfile2);
                ZipFile.ExtractToDirectory(modelsdir, modelsdir);
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ANN.ANNModel>(modelfilepath);
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

            var modeldata = ModelsSummary.Where(m => m.CompoundNames.Equals(mf1.ToArray()) && 
                        m.PropertyPackageName.Equals(request.ModelName)).OrderBy(m => m.TestingDataMSE).FirstOrDefault();

            if (modeldata == null) {return null;}

            var modelsdir = Path.Combine(HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }

            var model = LoadModelFromFile(Path.Combine(modelsdir, modeldata.ModelName + ".zip"));
            
            return model;
        
        }

    }
}
