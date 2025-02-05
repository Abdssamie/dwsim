using DWSIM.AI.ConvergenceHelper.ANN;
using DWSIM.AI.ConvergenceHelper.Training.Data;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.AI.ConvergenceHelper
{
    public class ModelEvaluator
    {

        public static PTFlash_ConvergenceHelperTrainingDataOutput PTFlash_Evaluate(ANNModel model, PTFlash_ConvergenceHelperTrainingDataInput d)
        {

            if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
            var row = new List<float>();
            row.Add(d.Temperature);
            row.Add(d.Pressure);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new PTFlash_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.VaporMolarFlows = predicted.GetRange(0, c).ToArray();
            output.Liquid1MolarFlows = predicted.GetRange(c - 1, c).ToArray();
            output.Liquid2MolarFlows = predicted.GetRange(2*c - 1, c).ToArray();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (output.VaporMolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.VaporMolarFlows[i] = 0f;
                if (output.Liquid1MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid1MolarFlows[i] = 0f;
                if (output.Liquid2MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid2MolarFlows[i] = 0f;
            }

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (d.MixtureMolarFlows[i] > 0f)
                {
                    output.VaporMolarFlows[i] = output.VaporMolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid1MolarFlows[i] = output.Liquid1MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid2MolarFlows[i] = output.Liquid2MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                }
            }

            var total0 = d.MixtureMolarFlows.Sum();
            var total = output.VaporMolarFlows.Sum() + output.Liquid1MolarFlows.Sum() + output.Liquid2MolarFlows.Sum();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x / total * total0).ToArray();

            return output;

        }

        public static PHPSFlash_ConvergenceHelperTrainingDataOutput PHFlash_Evaluate(ANNModel model, PHFlash_ConvergenceHelperTrainingDataInput d)
        {

            if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
            var row = new List<float>();
            row.Add(d.Pressure);
            row.Add(d.MassEnthalpy);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new PHPSFlash_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.Temperature = predicted[0];
            output.VaporMolarFlows = predicted.GetRange(1, c).ToArray();
            output.Liquid1MolarFlows = predicted.GetRange(c, c).ToArray();
            output.Liquid2MolarFlows = predicted.GetRange(2*c, c).ToArray();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (output.VaporMolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.VaporMolarFlows[i] = 0f;
                if (output.Liquid1MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid1MolarFlows[i] = 0f;
                if (output.Liquid2MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid2MolarFlows[i] = 0f;
            }

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (d.MixtureMolarFlows[i] > 0f)
                {
                    output.VaporMolarFlows[i] = output.VaporMolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid1MolarFlows[i] = output.Liquid1MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid2MolarFlows[i] = output.Liquid2MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                }
            }

            var total0 = d.MixtureMolarFlows.Sum();
            var total = output.VaporMolarFlows.Sum() + output.Liquid1MolarFlows.Sum() + output.Liquid2MolarFlows.Sum();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x / total * total0).ToArray();

            return output;

        }

        public static PHPSFlash_ConvergenceHelperTrainingDataOutput PSFlash_Evaluate(ANNModel model, PSFlash_ConvergenceHelperTrainingDataInput d)
        {

            if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
            var row = new List<float>();
            row.Add(d.Pressure);
            row.Add(d.MassEntropy);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new PHPSFlash_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.Temperature = predicted[0];
            output.VaporMolarFlows = predicted.GetRange(1, c).ToArray();
            output.Liquid1MolarFlows = predicted.GetRange(c, c).ToArray();
            output.Liquid2MolarFlows = predicted.GetRange(2*c, c).ToArray();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (output.VaporMolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.VaporMolarFlows[i] = 0f;
                if (output.Liquid1MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid1MolarFlows[i] = 0f;
                if (output.Liquid2MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid2MolarFlows[i] = 0f;
            }

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (d.MixtureMolarFlows[i] > 0f)
                {
                    output.VaporMolarFlows[i] = output.VaporMolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid1MolarFlows[i] = output.Liquid1MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid2MolarFlows[i] = output.Liquid2MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                }
            }

            var total0 = d.MixtureMolarFlows.Sum();
            var total = output.VaporMolarFlows.Sum() + output.Liquid1MolarFlows.Sum() + output.Liquid2MolarFlows.Sum();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x / total * total0).ToArray();

            return output;

        }

        public static PVFlash_ConvergenceHelperTrainingDataOutput PVFlash_Evaluate(ANNModel model, PVFlash_ConvergenceHelperTrainingDataInput d)
        {

            if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
            var row = new List<float>();
            row.Add(d.Pressure);
            row.Add(d.VaporFraction);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new PVFlash_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.Temperature = predicted[0];
            output.VaporMolarFlows = predicted.GetRange(1, c).ToArray();
            output.Liquid1MolarFlows = predicted.GetRange(c, c).ToArray();
            output.Liquid2MolarFlows = predicted.GetRange(2*c, c).ToArray();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (output.VaporMolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.VaporMolarFlows[i] = 0f;
                if (output.Liquid1MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid1MolarFlows[i] = 0f;
                if (output.Liquid2MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid2MolarFlows[i] = 0f;
            }

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (d.MixtureMolarFlows[i] > 0f)
                {
                    output.VaporMolarFlows[i] = output.VaporMolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid1MolarFlows[i] = output.Liquid1MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid2MolarFlows[i] = output.Liquid2MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                }
            }

            var total0 = d.MixtureMolarFlows.Sum();
            var total = output.VaporMolarFlows.Sum() + output.Liquid1MolarFlows.Sum() + output.Liquid2MolarFlows.Sum();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x / total * total0).ToArray();

            return output;

        }
     
        public static TVFlash_ConvergenceHelperTrainingDataOutput TVFlash_Evaluate(ANNModel model, TVFlash_ConvergenceHelperTrainingDataInput d)
        {

            if (d.Liquid2MolarFlows == null || d.Liquid2MolarFlows.Length == 0) d.Liquid2MolarFlows = new float[d.MixtureMolarFlows.Length];
            var row = new List<float>();
            row.Add(d.Temperature);
            row.Add(d.VaporFraction);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new TVFlash_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.Pressure = predicted[0];
            output.VaporMolarFlows = predicted.GetRange(1, c).ToArray();
            output.Liquid1MolarFlows = predicted.GetRange(c, c).ToArray();
            output.Liquid2MolarFlows = predicted.GetRange(2*c, c).ToArray();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x < 0f ? x = 0f : x).ToArray();

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (output.VaporMolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.VaporMolarFlows[i] = 0f;
                if (output.Liquid1MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid1MolarFlows[i] = 0f;
                if (output.Liquid2MolarFlows[i] > 0 && d.MixtureMolarFlows[i] == 0f) output.Liquid2MolarFlows[i] = 0f;
            }

            for (int i = 0; i < d.MixtureMolarFlows.Count(); i++)
            {
                if (d.MixtureMolarFlows[i] > 0f)
                {
                    output.VaporMolarFlows[i] = output.VaporMolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid1MolarFlows[i] = output.Liquid1MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                    output.Liquid2MolarFlows[i] = output.Liquid2MolarFlows[i] *
                        (output.VaporMolarFlows[i] + output.Liquid1MolarFlows[i] + +output.Liquid2MolarFlows[i]) /
                        d.MixtureMolarFlows[i];
                }
            }

            var total0 = d.MixtureMolarFlows.Sum();
            var total = output.VaporMolarFlows.Sum() + output.Liquid1MolarFlows.Sum() + output.Liquid2MolarFlows.Sum();

            output.VaporMolarFlows = output.VaporMolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid1MolarFlows = output.Liquid1MolarFlows.Select(x => x / total * total0).ToArray();
            output.Liquid2MolarFlows = output.Liquid2MolarFlows.Select(x => x / total * total0).ToArray();

            return output;

        }

        public static GibbsIsothermic_ConvergenceHelperTrainingDataOutput GI_Evaluate(ANNModel model, GibbsIsothermic_ConvergenceHelperTrainingDataInput d)
        {

            var row = new List<float>();
            row.Add(d.Temperature);
            row.Add(d.Pressure);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new GibbsIsothermic_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.MixtureMolarFlows2 = predicted.ToArray();
            output.MixtureMolarFlows2 = output.MixtureMolarFlows2.Select(x => x < 0f ? x = 0f : x).ToArray();

            return output;

        }

        public static GibbsAdiabatic_ConvergenceHelperTrainingDataOutput GA_Evaluate(ANNModel model, GibbsAdiabatic_ConvergenceHelperTrainingDataInput d)
        {

            var row = new List<float>();
            row.Add(d.Temperature);
            row.Add(d.Pressure);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new GibbsAdiabatic_ConvergenceHelperTrainingDataOutput();

            var c = d.MixtureMolarFlows.Count();

            output.Temperature2 = predicted[0];
            output.MixtureMolarFlows2 = predicted.GetRange(1, c).ToArray();
            output.MixtureMolarFlows2 = output.MixtureMolarFlows2.Select(x => x < 0f ? x = 0f : x).ToArray();

            return output;

        }

        public static EquilibriumIsothermic_ConvergenceHelperTrainingDataOutput EI_Evaluate(ANNModel model, EquilibriumIsothermic_ConvergenceHelperTrainingDataInput d)
        {

            var row = new List<float>();
            row.Add(d.Temperature);
            row.Add(d.Pressure);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new EquilibriumIsothermic_ConvergenceHelperTrainingDataOutput();

            output.ReactionExtents = predicted.ToArray();

            return output;

        }

        public static EquilibriumAdiabatic_ConvergenceHelperTrainingDataOutput EA_Evaluate(ANNModel model, EquilibriumAdiabatic_ConvergenceHelperTrainingDataInput d)
        {

            var row = new List<float>();
            row.Add(d.Temperature);
            row.Add(d.Pressure);
            row.AddRange(d.MixtureMolarFlows);

            var predicted = model.Predict(row);

            var output = new EquilibriumAdiabatic_ConvergenceHelperTrainingDataOutput();

            output.Temperature2 = predicted[0];
            output.ReactionExtents = predicted.GetRange(1, predicted.Count() - 1).ToArray();

            return output;

        }

    }
}
