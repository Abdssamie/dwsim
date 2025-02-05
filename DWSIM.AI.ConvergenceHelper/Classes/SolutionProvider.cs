using DWSIM.ExtensionMethods;
using DWSIM.Interfaces;

namespace DWSIM.AI.ConvergenceAssistant.Classes
{
    public class SolutionProvider
    {

        public static ConvergenceHelperResponse GetSolutionEstimate(ConvergenceHelperRequest request)
        {

            var model = Manager.GetModel(request);

            if (model == null) {return null;}

            var result = new ConvergenceHelperResponse();

            switch (request.RequestType)
            { 
                case ConvergenceHelperRequestType.PTFlash:

                    var input = new Training.Data.PTFlash_ConvergenceHelperTrainingDataInput();
                    input.Temperature = (float)request.Temperature;
                    input.Pressure = (float)request.Pressure;
                    input.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output = ModelEvaluator.PTFlash_Evaluate(model, input);

                    result.VaporMolarFlows = output.VaporMolarFlows.ToDouble();
                    result.Liquid1MolarFlows = output.Liquid1MolarFlows.ToDouble();
                    result.Liquid2MolarFlows = output.Liquid2MolarFlows.ToDouble();

                    break;

                case ConvergenceHelperRequestType.PVFlash:

                    var input2 = new Training.Data.PVFlash_ConvergenceHelperTrainingDataInput();
                    input2.VaporFraction = (float)request.VaporMolarFraction;
                    input2.Pressure = (float)request.Pressure;
                    input2.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output2 = ModelEvaluator.PVFlash_Evaluate(model, input2);

                    result.Temperature = output2.Temperature;
                    result.VaporMolarFlows = output2.VaporMolarFlows.ToDouble();
                    result.Liquid1MolarFlows = output2.Liquid1MolarFlows.ToDouble();
                    result.Liquid2MolarFlows = output2.Liquid2MolarFlows.ToDouble();

                    break;

                case ConvergenceHelperRequestType.TVFlash:

                    var input3 = new Training.Data.TVFlash_ConvergenceHelperTrainingDataInput();
                    input3.VaporFraction = (float)request.VaporMolarFraction;
                    input3.Temperature = (float)request.Temperature;
                    input3.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output3 = ModelEvaluator.TVFlash_Evaluate(model, input3);

                    result.Pressure = output3.Pressure;
                    result.VaporMolarFlows = output3.VaporMolarFlows.ToDouble();
                    result.Liquid1MolarFlows = output3.Liquid1MolarFlows.ToDouble();
                    result.Liquid2MolarFlows = output3.Liquid2MolarFlows.ToDouble();

                    break;

                case ConvergenceHelperRequestType.PHFlash:

                    var input4 = new Training.Data.PHFlash_ConvergenceHelperTrainingDataInput();
                    input4.MassEnthalpy = (float)request.MassEnthalpy;
                    input4.Pressure = (float)request.Pressure;
                    input4.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output4 = ModelEvaluator.PHFlash_Evaluate(model, input4);

                    result.Temperature = output4.Temperature;
                    result.VaporMolarFlows = output4.VaporMolarFlows.ToDouble();
                    result.Liquid1MolarFlows = output4.Liquid1MolarFlows.ToDouble();
                    result.Liquid2MolarFlows = output4.Liquid2MolarFlows.ToDouble();

                    break;

                case ConvergenceHelperRequestType.PSFlash:

                    var input5 = new Training.Data.PSFlash_ConvergenceHelperTrainingDataInput();
                    input5.MassEntropy = (float)request.MassEntropy;
                    input5.Pressure = (float)request.Pressure;
                    input5.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output5 = ModelEvaluator.PSFlash_Evaluate(model, input5);

                    result.Temperature = output5.Temperature;
                    result.VaporMolarFlows = output5.VaporMolarFlows.ToDouble();
                    result.Liquid1MolarFlows = output5.Liquid1MolarFlows.ToDouble();
                    result.Liquid2MolarFlows = output5.Liquid2MolarFlows.ToDouble();

                    break;

                case ConvergenceHelperRequestType.GibbsReactorIsothermic:

                    var input6 = new Training.Data.GibbsIsothermic_ConvergenceHelperTrainingDataInput();
                    input6.Temperature = (float)request.Temperature;
                    input6.Pressure = (float)request.Pressure;
                    input6.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output6 = ModelEvaluator.GI_Evaluate(model, input6);

                    result.MixtureMolarFlows2 = output6.MixtureMolarFlows2.ToDouble();

                    break;

                case ConvergenceHelperRequestType.GibbsReactorAdiabatic:

                    var input7 = new Training.Data.GibbsAdiabatic_ConvergenceHelperTrainingDataInput();
                    input7.Temperature = (float)request.Temperature;
                    input7.Pressure = (float)request.Pressure;
                    input7.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output7 = ModelEvaluator.GA_Evaluate(model, input7);

                    result.Temperature2 = output7.Temperature2;
                    result.MixtureMolarFlows2 = output7.MixtureMolarFlows2.ToDouble();

                    break;

                case ConvergenceHelperRequestType.EquilibriumReactorIsothermic:

                    var input8 = new Training.Data.EquilibriumIsothermic_ConvergenceHelperTrainingDataInput();
                    input8.Temperature = (float)request.Temperature;
                    input8.Pressure = (float)request.Pressure;
                    input8.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output8 = ModelEvaluator.EI_Evaluate(model, input8);

                    result.ReactionExtents = output8.ReactionExtents.ToDouble();

                    break;

                case ConvergenceHelperRequestType.EquilibriumReactorAdiabatic:

                    var input9 = new Training.Data.EquilibriumAdiabatic_ConvergenceHelperTrainingDataInput();
                    input9.Temperature = (float)request.Temperature;
                    input9.Pressure = (float)request.Pressure;
                    input9.MixtureMolarFlows = request.MixtureMolarFlows.ToSingle();

                    var output9 = ModelEvaluator.EA_Evaluate(model, input9);

                    result.Temperature2 = output9.Temperature2;
                    result.ReactionExtents = output9.ReactionExtents.ToDouble();

                    break;

            }

            return result;

        }


    }
}
