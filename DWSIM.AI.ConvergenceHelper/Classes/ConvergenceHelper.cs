using System;
using System.Collections.Generic;
using DWSIM.Interfaces;
using Microsoft.ML.Data;

namespace DWSIM.AI.ConvergenceHelper.Classes
{
    public class ConvergenceHelper : IConvergenceHelper
    {
        public IConvergenceHelperResponse GetEstimates(ConvergenceHelperRequestType RequestType, string ModelName, double[] MixtureMolarFlows, double? Temperature, double? Pressure, double? VaporMolarFraction, double? MassEnthalpy, double? MassEntropy)
        {
            return new ConvergenceHelperResponse();
        }

        public void StoreResults(ConvergenceHelperRequestType RequestType, string ModelName, double[] MixtureMolarFlows, double? Temperature, double? Pressure, double? VaporMolarFraction, double? MassEnthalpy, double? MassEntropy, double[] VaporMolarFlows, double[] Liquid1MolarFlows, double[] Liquid2MolarFlows, double[] SolidMolarFlows, double[] KValuesVL1, double[] KValuesVL2)
        {
            throw new NotImplementedException();
        }
    }

    public class ConvergenceHelperTrainingData : IConvergenceHelperTrainingData
    {
        public ConvergenceHelperRequestType RequestType { get; set; }
        public string ModelName { get; set; }
        public List<IReaction> Reactions { get; set; }
        public string Hash { get; set; }
        public int NumberOfCompounds { get; set; }
        public string[] CompoundNames { get; set; }
        public string Temperature { get; set; }
        public string Temperature2 { get; set; }
        public string Pressure { get; set; }
        public string MassEnthalpy { get; set; }
        public string MassEntropy { get; set; }
        public string VaporMolarFraction { get; set; }
        public string[] MixtureMolarFlows { get; set; }
        public string[] MixtureMolarFlows2 { get; set; }
        public string[] VaporMolarFlows { get; set; }
        public string[] Liquid1MolarFlows { get; set; }
        public string[] Liquid2MolarFlows { get; set; }
        public string[] SolidMolarFlows { get; set; }
        public string[] KValuesVL1 { get; set; }
        public string[] KValuesVL2 { get; set; }
        public string[] ReactionExtents { get; set; }

        public string GetBase64StringHash()
        {
            var options = new Newtonsoft.Json.JsonSerializerSettings();
            var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsondata);
            return Convert.ToBase64String(plainTextBytes);
        }
    }

    public class ConvergenceHelperRequest : IConvergenceHelperRequest
    {
        public ConvergenceHelperRequestType RequestType { get; set; }
        public string ModelName { get; set; } = "";
        public int NumberOfCompounds { get; set; }
        public string[] CompoundNames { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? MassEnthalpy { get; set; }
        public double? MassEntropy { get; set; }
        public double? VaporMolarFraction { get; set; }
        public double[] MixtureMolarFlows { get; set; }
    }

    public class ConvergenceHelperResponse : IConvergenceHelperResponse
    {
        public ConvergenceHelperRequestType RequestType { get; set; } = ConvergenceHelperRequestType.PVFlash;
        public IConvergenceHelperMetaData MetaData { get; set; } = new ConvergenceHelperMetaData();
        public string ModelName { get; set; } = "";
        public bool IsValid { get; set; }
        public string Reason { get; set; } = "";
        public Exception InnerException { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? MassEnthalpy { get; set; }
        public double? MassEntropy { get; set; }
        public double? VaporMolarFraction { get; set; }
        public double[] MixtureMolarFlows { get; set; }
        public double[] VaporMolarFlows { get; set; }
        public double[] Liquid1MolarFlows { get; set; }
        public double[] Liquid2MolarFlows { get; set; }
        public double[] SolidMolarFlows { get; set; }
        public double[] KValuesVL1 { get; set; }
        public double[] KValuesVL2 { get; set; }
    }

    public class ConvergenceHelperMetaData : IConvergenceHelperMetaData
    {
        public string ModelName { get; set; } = "";
        public int NumberOfCompounds { get; set; }
        public int NumberOfSamples { get; set; }
        public string[] CompoundNames { get; set; }
        public Tuple<double, double> TemperatureRange { get; set; }
        public Tuple<double, double> PressureRange { get; set; }
        public Tuple<double, double> MassEnthalpyRange { get; set; }
        public Tuple<double, double> MassEntropyRange { get; set; }
        public Tuple<double, double> VaporMolarFractionRange { get; set; }
        public Tuple<double[], double[]> MolarCompositionRange { get; set; }
    }
}
