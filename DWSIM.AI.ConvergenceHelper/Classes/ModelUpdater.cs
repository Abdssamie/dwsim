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
            ModelTrainer.PVFlash_Train(data, ta, plot);
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
            ModelTrainer.TVFlash_Train(data, ta, plot);
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
            ModelTrainer.PHFlash_Train(data, ta, plot);
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
            ModelTrainer.PSFlash_Train(data, ta, plot);
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
            ModelTrainer.EquilibriumReactorIsothermic_Train(data, ta, plot);
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
            ModelTrainer.EquilibriumReactorAdiabatic_Train(data, ta, plot);
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
            ModelTrainer.GibbsReactorIsothermic_Train(data, ta, plot);
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
            ModelTrainer.GibbsReactorAdiabatic_Train(data, ta, plot);
        }

    }
}
