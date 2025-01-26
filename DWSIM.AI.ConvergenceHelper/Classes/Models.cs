using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.AI.ConvergenceHelper.Training.Data
{

    public class PTFlash_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class PTFlash_ConvergenceHelperTrainingDataOutput
    {
        [VectorType(50)] public float[] VaporMolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid1MolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid2MolarFlows { get; set; }
        [VectorType(50)] public float[] SolidMolarFlows { get; set; }
        [VectorType(50)] public float[] KValuesVL1 { get; set; }
        [VectorType(50)] public float[] KValuesVL2 { get; set; }
    }

    public class PVFlash_ConvergenceHelperTrainingDataInput
    {
        public float VaporFraction { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class PVFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature { get; set; }
        [VectorType(50)] public float[] VaporMolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid1MolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid2MolarFlows { get; set; }
        [VectorType(50)] public float[] SolidMolarFlows { get; set; }
        [VectorType(50)] public float[] KValuesVL1 { get; set; }
        [VectorType(50)] public float[] KValuesVL2 { get; set; }
    }

    public class TVFlash_ConvergenceHelperTrainingDataInput
    {
        public float VaporFraction { get; set; }
        public float Temperature { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class TVFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Pressure { get; set; }
        [VectorType(50)] public float[] VaporMolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid1MolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid2MolarFlows { get; set; }
        [VectorType(50)] public float[] SolidMolarFlows { get; set; }
        [VectorType(50)] public float[] KValuesVL1 { get; set; }
        [VectorType(50)] public float[] KValuesVL2 { get; set; }
    }

    public class PHFlash_ConvergenceHelperTrainingDataInput
    {
        public float MassEnthalpy { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class PHFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature { get; set; }
        [VectorType(50)] public float[] VaporMolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid1MolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid2MolarFlows { get; set; }
        [VectorType(50)] public float[] SolidMolarFlows { get; set; }
        [VectorType(50)] public float[] KValuesVL1 { get; set; }
        [VectorType(50)] public float[] KValuesVL2 { get; set; }
    }

    public class PSFlash_ConvergenceHelperTrainingDataInput
    {
        public float MassEntropy { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class PSFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature { get; set; }
        [VectorType(50)] public float[] VaporMolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid1MolarFlows { get; set; }
        [VectorType(50)] public float[] Liquid2MolarFlows { get; set; }
        [VectorType(50)] public float[] SolidMolarFlows { get; set; }
        [VectorType(50)] public float[] KValuesVL1 { get; set; }
        [VectorType(50)] public float[] KValuesVL2 { get; set; }
    }

    public class GibbsIsothermic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class GibbsIsothermic_ConvergenceHelperTrainingDataOutput
    {
        [VectorType(50)] public float[] MixtureMolarFlows2 { get; set; }
    }

    public class GibbsAdiabatic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class GibbsAdiabatic_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature2 { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows2 { get; set; }
    }

    public class EquilibriumIsothermic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class EquilibriumIsothermic_ConvergenceHelperTrainingDataOutput
    {
        [VectorType(50)] public float[] ReactionExtents { get; set; }
    }

    public class EquilibriumAdiabatic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        [VectorType(50)] public float[] MixtureMolarFlows { get; set; }
    }

    public class Equilibrium_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature2 { get; set; }
        [VectorType(50)] public float[] ReactionExtents { get; set; }
    }
}
