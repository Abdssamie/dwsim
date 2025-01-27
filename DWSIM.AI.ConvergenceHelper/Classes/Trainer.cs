using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;
using DWSIM.AI.ConvergenceHelper.Training.Data;

namespace DWSIM.AI.ConvergenceHelper.Training.Trainer
{
    public static class ModelTrainer
    {

        private static MLContext mlContext;
       
        public static void Initialize()
        {
            mlContext = new MLContext(seed: 0);
        }

        public static ITransformer PTFlash_Train(List<PTFlash_ConvergenceHelperTrainingDataInput> data)
        {

            IDataView dataView = mlContext.Data.LoadFromEnumerable(data);

            var sdcaEstimator = mlContext.Regression.Trainers.OnlineGradientDescent();

            TrainTestData dataSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            var model = sdcaEstimator.Fit(dataSplit.TrainSet);

            PTFlash_Evaluate(model, dataSplit.TestSet);

            return model;
        }

        public static void PTFlash_Evaluate(ITransformer model, List<PTFlash_ConvergenceHelperTrainingDataInput> data)
        {

            IDataView dataView = mlContext.Data.LoadFromEnumerable(data);

            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
         
        }

        public static void PTFlash_Evaluate(ITransformer model, IDataView data)
        {

            var predictions = model.Transform(data);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

        }

        public static void PTFlash_SinglePrediction(ITransformer model, PTFlash_ConvergenceHelperTrainingDataInput sample)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<PTFlash_ConvergenceHelperTrainingDataInput, PTFlash_ConvergenceHelperTrainingDataOutput>(model);

            var output = new PTFlash_ConvergenceHelperTrainingDataOutput();

            predictionFunction.Predict(sample, ref output);

        }
    }
}