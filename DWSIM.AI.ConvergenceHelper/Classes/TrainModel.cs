using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;

namespace DWSIM.AI.ConvergenceHelper
{
    public static class ModelTrainer
    {

        private static MLContext mlContext;
       
        public static void Initialize()
        {

            mlContext = new MLContext(seed: 0);

        }

        public static ITransformer Train(MLContext mlContext, List<ConvergenceHelperTrainingData> data)
        {

            IDataView dataView = mlContext.Data.LoadFromEnumerable(data);

            var sdcaEstimator = mlContext.Regression.Trainers.OnlineGradientDescent();

            TrainTestData dataSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            var model = sdcaEstimator.Fit(dataSplit.TrainSet);

            Evaluate(mlContext, model, dataSplit.TestSet);

            return model;
        }

        public static void Evaluate(MLContext mlContext, ITransformer model, List<ConvergenceHelperTrainingData> data)
        {

            IDataView dataView = mlContext.Data.LoadFromEnumerable(data);

            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
         
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }

        public static void Evaluate(MLContext mlContext, ITransformer model, IDataView data)
        {

            var predictions = model.Transform(data);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }

        public static void TestSinglePrediction(MLContext mlContext, ITransformer model)
        {
            //var predictionFunction = mlContext.Model.CreatePredictionEngine<ConvergenceHelperTrainingData, TaxiTripFarePrediction>(model);
                      
            //var prediction = predictionFunction.Predict(taxiTripSample);
           
            //Console.WriteLine($"**********************************************************************");
            //Console.WriteLine($"Predicted fare: {prediction.FareAmount:0.####}, actual fare: 15.5");
            //Console.WriteLine($"**********************************************************************");

        }
    }
}