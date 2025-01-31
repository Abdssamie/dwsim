using DWSIM.AI.ConvergenceHelper.Training.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.AI.ConvergenceHelper.Classes
{
    public class ModelTrainer
    {

        public static void PTFlash_Train(List<PTFlash_ConvergenceHelperTrainingDataInput> data)
        {

            var model = new ANN.ANNModel();

            List<List<float>> data_transformed = new List<List<float>>();
            foreach (var d in data)
            {
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

            model.Parameters.BatchSize = data.Count()/10;

            model.PrepareData();
            model.Train();

        }

    }
}
