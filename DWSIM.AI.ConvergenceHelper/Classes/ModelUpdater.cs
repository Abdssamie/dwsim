using DWSIM.AI.ConvergenceHelper.Training.Data;
using Eto.Forms;
using System.Linq;
using DWSIM.ExtensionMethods;
using DWSIM.AI.ConvergenceHelper.Classes;

namespace DWSIM.AI.ConvergenceHelper
{
    public class ModelUpdater
    {

        public static void UpdatePTModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.PTFlash).ToList();
            var data = entries.Select(x => new PTFlash_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                VaporMolarFlows = x.VaporMolarFlows?.ToSingleArray(),
                Liquid1MolarFlows = x.Liquid1MolarFlows?.ToSingleArray(),
                Liquid2MolarFlows = x.Liquid2MolarFlows?.ToSingleArray(),
                SolidMolarFlows = x.SolidMolarFlows?.ToSingleArray()
            }).ToList();

            foreach (var d in data) d.PrepareData();

            var model = ModelTrainer.PTFlash_Train(data, ta, plot);
            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("PTF_{0}_{1}c_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

            Manager.SaveModelToFile(model);

            var output = ModelEvaluator.PTFlash_Evaluate(model, data.First());
        }

        public static void UpdatePVModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.PVFlash).ToList();
            var data = entries.Select(x => new PVFlash_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                VaporFraction= x.VaporMolarFraction.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                VaporMolarFlows = x.VaporMolarFlows?.ToSingleArray(),
                Liquid1MolarFlows = x.Liquid1MolarFlows?.ToSingleArray(),
                Liquid2MolarFlows = x.Liquid2MolarFlows?.ToSingleArray(),
                SolidMolarFlows = x.SolidMolarFlows?.ToSingleArray()
            }).ToList();
            foreach (var d in data) d.PrepareData();

            var model = ModelTrainer.PVFlash_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("PVF_{0}_{1}c_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));


        }

        public static void UpdateTVModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.TVFlash).ToList();
            var data = entries.Select(x => new TVFlash_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                VaporFraction= x.VaporMolarFraction.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                VaporMolarFlows = x.VaporMolarFlows?.ToSingleArray(),
                Liquid1MolarFlows = x.Liquid1MolarFlows?.ToSingleArray(),
                Liquid2MolarFlows = x.Liquid2MolarFlows?.ToSingleArray(),
                SolidMolarFlows = x.SolidMolarFlows?.ToSingleArray()
            }).ToList();
            foreach (var d in data) d.PrepareData();

            var model = ModelTrainer.TVFlash_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("TVF_{0}_{1}c_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }

        public static void UpdatePHModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.PHFlash).ToList();
            var data = entries.Select(x => new PHFlash_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                MassEnthalpy= x.MassEnthalpy.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                VaporMolarFlows = x.VaporMolarFlows?.ToSingleArray(),
                Liquid1MolarFlows = x.Liquid1MolarFlows?.ToSingleArray(),
                Liquid2MolarFlows = x.Liquid2MolarFlows?.ToSingleArray(),
                SolidMolarFlows = x.SolidMolarFlows?.ToSingleArray()
            }).ToList();
            foreach (var d in data) d.PrepareData();

            var model = ModelTrainer.PHFlash_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("PHF_{0}_{1}c_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }

        public static void UpdatePSModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.PSFlash).ToList();
            var data = entries.Select(x => new PSFlash_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                MassEntropy= x.MassEntropy.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                VaporMolarFlows = x.VaporMolarFlows?.ToSingleArray(),
                Liquid1MolarFlows = x.Liquid1MolarFlows?.ToSingleArray(),
                Liquid2MolarFlows = x.Liquid2MolarFlows?.ToSingleArray(),
                SolidMolarFlows = x.SolidMolarFlows?.ToSingleArray()
            }).ToList();
            foreach (var d in data) d.PrepareData();

            var model = ModelTrainer.PSFlash_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("PSF_{0}_{1}c_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }

        public static void UpdateEIModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.EquilibriumReactorIsothermic).ToList();
            var data = entries.Select(x => new EquilibriumIsothermic_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                ReactionExtents = x.ReactionExtents?.ToSingleArray(),
            }).ToList();

            var model = ModelTrainer.EquilibriumReactorIsothermic_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.NumberOfReactions = entries[0].ReactionExtents.Count();
            model.MetaData.ModelName = string.Format("EI_{0}_{1}c_{2}r_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.NumberOfReactions,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }

        public static void UpdateEAModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.EquilibriumReactorAdiabatic).ToList();
            var data = entries.Select(x => new EquilibriumAdiabatic_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                Temperature2  = x.Temperature2.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                ReactionExtents = x.ReactionExtents?.ToSingleArray(),
            }).ToList();

            var model = ModelTrainer.EquilibriumReactorAdiabatic_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.NumberOfReactions = entries[0].ReactionExtents.Count();
            model.MetaData.ModelName = string.Format("EA_{0}_{1}c_{2}r_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.NumberOfReactions,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }

        public static void UpdateGIModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.GibbsReactorIsothermic).ToList();
            var data = entries.Select(x => new GibbsIsothermic_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                MixtureMolarFlows2 = x.MixtureMolarFlows2?.ToSingleArray(),
            }).ToList();

            var model = ModelTrainer.GibbsReactorIsothermic_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("GI_{0}_{1}c_{2}r_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.NumberOfReactions,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }

        public static void UpdateGAModels(TextArea ta, Eto.OxyPlot.Plot plot)
        {
            var col = Manager.Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
            var entries = col.Query().Where(x => x.RequestType == Interfaces.ConvergenceHelperRequestType.GibbsReactorAdiabatic).ToList();
            var data = entries.Select(x => new GibbsAdiabatic_ConvergenceHelperTrainingDataInput
            {
                Pressure = x.Pressure.ToSingleFromInvariant(),
                Temperature = x.Temperature.ToSingleFromInvariant(),
                Temperature2 = x.Temperature2.ToSingleFromInvariant(),
                MixtureMolarFlows = x.MixtureMolarFlows?.ToSingleArray(),
                MixtureMolarFlows2 = x.MixtureMolarFlows2?.ToSingleArray(),
            }).ToList();

            var model = ModelTrainer.GibbsReactorAdiabatic_Train(data, ta, plot);

            model.MetaData.CompoundNames = entries[0].CompoundNames;
            model.MetaData.NumberOfCompounds = entries[0].NumberOfCompounds;
            model.MetaData.PropertyPackageName = entries[0].ModelName;
            model.MetaData.ModelName = string.Format("GA_{0}_{1}c_{2}r_LU_{2}",
                model.MetaData.PropertyPackageName.Replace(' ', '_').Replace('-', '_').Replace('(', '_').Replace(')', '_'),
                model.MetaData.NumberOfCompounds,
                model.MetaData.NumberOfReactions,
                model.MetaData.LastUpdatedOn.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));

        }


    }
}
