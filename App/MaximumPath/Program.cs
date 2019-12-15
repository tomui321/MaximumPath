using System;
using System.Linq;
using DataContracts;
using MaximumPath.Config;
using MaximumPath.DataAccess;
using MaximumPath.PathFinders;
using MaximumPath.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace MaximumPath
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler;
            
            var fileConfig = new InputFileConfig
            {
                Filename = "pyramid.txt"
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMaximumPathFinder, SmartMaximumPathFinder>()
                .AddSingleton(fileConfig)
                .AddSingleton<IInputProvider, InputProviderFromFile>()
                .AddSingleton<IPyramidValidator, PyramidValidator>()
                .AddSingleton<IOutputWriter, ConsoleOutputWriter>()
                .BuildServiceProvider();

            var inputProvider = serviceProvider.GetService<IInputProvider>();
            var pyramidValidator = serviceProvider.GetService<IPyramidValidator>();
            var pathFinder = serviceProvider.GetService<IMaximumPathFinder>();
            var outputWriter = serviceProvider.GetService<IOutputWriter>();

            var result = Process(inputProvider, pyramidValidator, pathFinder);
            WriteOutput(outputWriter, result);

            Console.ReadLine();
        }

        public static Result Process(IInputProvider inputProvider, IPyramidValidator pyramidValidator, IMaximumPathFinder pathFinder)
        {
            var pyramid = inputProvider.GetPyramid();

            if (!pyramidValidator.IsValid(pyramid))
            {
                throw new ArgumentException("Invalid pyramid provided!");
            }

            var path = pathFinder.FindPath(pyramid);

            return new Result
            {
                MaximumPossibleSum = path?.Sum() ?? 0,
                Path = path
            };
        }

        public static void WriteOutput(IOutputWriter outputWriter, Result result)
        {
            outputWriter.WriteMaximumPossibleSum(result);
            outputWriter.WritePath(result);
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
