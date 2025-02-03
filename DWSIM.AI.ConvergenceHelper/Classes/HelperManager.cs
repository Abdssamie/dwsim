using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DWSIM.FileStorage;
using DWSIM.AI.ConvergenceHelper.Classes;
using DWSIM.AI.ConvergenceHelper.Training.Data;
using DWSIM.ExtensionMethods;
using Eto.Forms;

namespace DWSIM.AI.ConvergenceHelper
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
                ZipFile.ExtractToDirectory(dbfile, datadir);
                var dbfile2 = Path.Combine(datadir, "data.db");
                Database.LoadDatabase(dbfile2);
                File.Delete(dbfile2);
            }

            var msfile = Path.Combine(modelsdir, "summary.json");
            if (!File.Exists(msfile))
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(ModelsSummary);
                File.WriteAllText(msfile, data);
            }
            else
            {
                ModelsSummary = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConvergenceHelperMetaData>>(File.ReadAllText(msfile));
            }

            FlowsheetSolver.FlowsheetSolver.FlowsheetCalculationFinished += FlowsheetSolver_FlowsheetCalculationFinished;

            //ModelTrainer.Initialize();

            Initialized = true;

        }

        private static void FlowsheetSolver_FlowsheetCalculationFinished(object sender, EventArgs e, object extrainfo)
        {
            if (GlobalSettings.Settings.ConvergenceHelperEnabled) { 
                Task.Run(() => SaveDatabaseToFile());
                //UpdateModels();
            }
        }

        public static void UpdateModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.PTFlash).ToList();
            var data = entries.Select(x => new PTFlash_ConvergenceHelperTrainingDataInput {
                  Pressure = x.Pressure.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),  
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                VaporMolarFlows = x.VaporMolarFlows?.ToSingleArray(),
                Liquid1MolarFlows = x.Liquid1MolarFlows?.ToSingleArray(),
                Liquid2MolarFlows = x.Liquid2MolarFlows?.ToSingleArray(),
                SolidMolarFlows = x.SolidMolarFlows?.ToSingleArray()
            }).ToList();
            foreach (var d in data) d.PrepareData();
           ModelTrainer.PTFlash_Train(data, ta, plot);
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
                    foreach (var comp in comps) {
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

    }
}
