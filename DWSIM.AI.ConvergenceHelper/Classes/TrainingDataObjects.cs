namespace DWSIM.AI.ConvergenceHelper.Training.Data
{

    public class PTFlash_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }
        public float[] SolidMolarFlows { get; set; }

        public void PrepareData()
        {
            if (Liquid2MolarFlows == null) Liquid2MolarFlows = new float[0];
            if (SolidMolarFlows == null) SolidMolarFlows = new float[0];
        }

    }

    public class PTFlash_ConvergenceHelperTrainingDataOutput
    {
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }

    }

    public class PVFlash_ConvergenceHelperTrainingDataInput
    {
        public float VaporFraction { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float Temperature { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }
        public float[] SolidMolarFlows { get; set; }

        public void PrepareData()
        {
            if (Liquid2MolarFlows == null) Liquid2MolarFlows = new float[0];
            if (SolidMolarFlows == null) SolidMolarFlows = new float[0];
        }

    }

    public class PVFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }

    }

    public class TVFlash_ConvergenceHelperTrainingDataInput
    {
        public float VaporFraction { get; set; }
        public float Temperature { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float Pressure { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }
        public float[] SolidMolarFlows { get; set; }

        public void PrepareData()
        {
            if (Liquid2MolarFlows == null) Liquid2MolarFlows = new float[0];
            if (SolidMolarFlows == null) SolidMolarFlows = new float[0];
        }

    }

    public class TVFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Pressure { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }

    }

    public class PHFlash_ConvergenceHelperTrainingDataInput
    {
        public float MassEnthalpy { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float Temperature { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }
        public float[] SolidMolarFlows { get; set; }

        public void PrepareData()
        {
            if (Liquid2MolarFlows == null) Liquid2MolarFlows = new float[0];
            if (SolidMolarFlows == null) SolidMolarFlows = new float[0];
        }

    }

    public class PHPSFlash_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }

    }

    public class PSFlash_ConvergenceHelperTrainingDataInput
    {
        public float MassEntropy { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float Temperature { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }
        public float[] SolidMolarFlows { get; set; }

        public void PrepareData()
        {
            if (Liquid2MolarFlows == null) Liquid2MolarFlows = new float[0];
            if (SolidMolarFlows == null) SolidMolarFlows = new float[0];
        }

    }

    public class GibbsIsothermic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float[] MixtureMolarFlows2 { get; set; }
    }

    public class GibbsIsothermic_ConvergenceHelperTrainingDataOutput
    {
        public float[] MixtureMolarFlows2 { get; set; }
    }

    public class GibbsAdiabatic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float Temperature2 { get; set; }
        public float[] MixtureMolarFlows2 { get; set; }
    }

    public class GibbsAdiabatic_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature2 { get; set; }
        public float[] MixtureMolarFlows2 { get; set; }
    }

    public class EquilibriumIsothermic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float[] ReactionExtents { get; set; }
    }

    public class EquilibriumIsothermic_ConvergenceHelperTrainingDataOutput
    {
        public float[] ReactionExtents { get; set; }
    }

    public class EquilibriumAdiabatic_ConvergenceHelperTrainingDataInput
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float Temperature2 { get; set; }
        public float[] ReactionExtents { get; set; }
    }

    public class EquilibriumAdiabatic_ConvergenceHelperTrainingDataOutput
    {
        public float Temperature2 { get; set; }
        public float[] ReactionExtents { get; set; }
    }

}
