using DWSIM.AI.ConvergenceHelper.Training.Data;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.AI.ConvergenceHelper.Classes
{
    public class ModelTrainer
    {

        public static void PTFlash_Train(List<PTFlash_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
                var row = new List<float>();
                row.Add(d.Temperature);
                row.Add(d.Pressure);
                row.AddRange(d.MixtureMolarFlows);
                row.AddRange(d.VaporMolarFlows);
                row.AddRange(d.Liquid1MolarFlows);
                row.AddRange(d.Liquid2MolarFlows);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Temperature");
            labels.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("VaporMolarFlow" + i.ToString());
                labels_output.Add("VaporMolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid1MolarFlow" + i.ToString());
                labels_output.Add("Liquid1MolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid2MolarFlow" + i.ToString());
                labels_output.Add("Liquid2MolarFlow" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void PVFlash_Train(List<PVFlash_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
                var row = new List<float>();
                row.Add(d.VaporFraction);
                row.Add(d.Pressure);
                row.AddRange(d.MixtureMolarFlows);
                row.Add(d.Temperature);
                row.AddRange(d.VaporMolarFlows);
                row.AddRange(d.Liquid1MolarFlows);
                row.AddRange(d.Liquid2MolarFlows);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("VaporPressure");
            labels.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow" + i.ToString());
            }
            labels_output.Add("Temperature");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("VaporMolarFlow" + i.ToString());
                labels_output.Add("VaporMolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid1MolarFlow" + i.ToString());
                labels_output.Add("Liquid1MolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid2MolarFlow" + i.ToString());
                labels_output.Add("Liquid2MolarFlow" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void TVFlash_Train(List<TVFlash_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
                var row = new List<float>();
                row.Add(d.VaporFraction);
                row.Add(d.Temperature);
                row.AddRange(d.MixtureMolarFlows);
                row.Add(d.Pressure);
                row.AddRange(d.VaporMolarFlows);
                row.AddRange(d.Liquid1MolarFlows);
                row.AddRange(d.Liquid2MolarFlows);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("VaporPressure");
            labels.Add("Temperature");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow" + i.ToString());
            }
            labels_output.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("VaporMolarFlow" + i.ToString());
                labels_output.Add("VaporMolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid1MolarFlow" + i.ToString());
                labels_output.Add("Liquid1MolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid2MolarFlow" + i.ToString());
                labels_output.Add("Liquid2MolarFlow" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void PHFlash_Train(List<PHFlash_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
                var row = new List<float>();
                row.Add(d.Pressure);
                row.Add(d.MassEnthalpy);
                row.AddRange(d.MixtureMolarFlows);
                row.Add(d.Temperature);
                row.AddRange(d.VaporMolarFlows);
                row.AddRange(d.Liquid1MolarFlows);
                row.AddRange(d.Liquid2MolarFlows);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Pressure");
            labels.Add("Enthalpy");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow" + i.ToString());
            }
            labels_output.Add("Temperature");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("VaporMolarFlow" + i.ToString());
                labels_output.Add("VaporMolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid1MolarFlow" + i.ToString());
                labels_output.Add("Liquid1MolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid2MolarFlow" + i.ToString());
                labels_output.Add("Liquid2MolarFlow" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void PSFlash_Train(List<PHFlash_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
                var row = new List<float>();
                row.Add(d.Pressure);
                row.Add(d.MassEnthalpy);
                row.AddRange(d.MixtureMolarFlows);
                row.Add(d.Temperature);
                row.AddRange(d.VaporMolarFlows);
                row.AddRange(d.Liquid1MolarFlows);
                row.AddRange(d.Liquid2MolarFlows);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Pressure");
            labels.Add("Entropy");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow" + i.ToString());
            }
            labels_output.Add("Temperature");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("VaporMolarFlow" + i.ToString());
                labels_output.Add("VaporMolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid1MolarFlow" + i.ToString());
                labels_output.Add("Liquid1MolarFlow" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("Liquid2MolarFlow" + i.ToString());
                labels_output.Add("Liquid2MolarFlow" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void GibbsReactorIsothermic_Train(List<GibbsIsothermic_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                var row = new List<float>();
                row.Add(d.Temperature);
                row.Add(d.Pressure);
                row.AddRange(d.MixtureMolarFlows);
                row.AddRange(d.MixtureMolarFlows2);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Temperature");
            labels.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow_In_" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow_Out_" + i.ToString());
                labels_output.Add("MixtureMolarFlow2_Out_" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void GibbsReactorAdiabatic_Train(List<GibbsAdiabatic_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                var row = new List<float>();
                row.Add(d.Temperature);
                row.Add(d.Pressure);
                row.AddRange(d.MixtureMolarFlows);
                row.AddRange(d.MixtureMolarFlows2);
                row.Add(d.Temperature2);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Temperature");
            labels.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow_In_" + i.ToString());
            }
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow_Out_" + i.ToString());
                labels_output.Add("MixtureMolarFlow2_Out_" + i.ToString());
            }
            labels.Add("TemperatureOut");
            labels_output.Add("TemperatureOut");

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void EquilibriumReactorIsothermic_Train(List<EquilibriumIsothermic_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                var row = new List<float>();
                row.Add(d.Temperature);
                row.Add(d.Pressure);
                row.AddRange(d.MixtureMolarFlows);
                row.AddRange(d.ReactionExtents);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Temperature");
            labels.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow_" + i.ToString());
            }
            for (int i = 0; i < data.First().ReactionExtents.Count(); i++)
            {
                labels.Add("ReactionExtents_" + i.ToString());
                labels_output.Add("ReactionExtents_" + i.ToString());
            }

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

        public static void EquilibriumReactorAdiabatic_Train(List<EquilibriumAdiabatic_ConvergenceHelperTrainingDataInput> data, TextArea ta, Eto.OxyPlot.Plot plot)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
                var row = new List<float>();
                row.Add(d.Temperature);
                row.Add(d.Pressure);
                row.AddRange(d.MixtureMolarFlows);
                row.AddRange(d.ReactionExtents);
                data_transformed.Add(row);
            }

            var labels = new List<string>();
            var labels_output = new List<string>();
            labels.Add("Temperature");
            labels.Add("Pressure");
            for (int i = 0; i < data.First().MixtureMolarFlows.Count(); i++)
            {
                labels.Add("MixtureMolarFlow_" + i.ToString());
            }
            for (int i = 0; i < data.First().ReactionExtents.Count(); i++)
            {
                labels.Add("ReactionExtents_" + i.ToString());
                labels_output.Add("ReactionExtents_" + i.ToString());
            }
            labels.Add("TemperatureOut");
            labels_output.Add("TemperatureOut");

            model.Data = data_transformed;
            model.Parameters.Labels = labels;
            model.Parameters.Labels_Outputs = labels_output;

            model.Parameters.BatchSize = model.Data.Count / 2;
            model.Parameters.NumberOfLayers = 2;
            model.Parameters.NumberOfNeuronsOnFirstLayer = 100;
            model.Parameters.NumberOfEpochs = 10000;

            model.PrepareData();
            model.Train(null, ta, plot);

        }

    }
}
