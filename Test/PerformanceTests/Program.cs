using System;
using System.Diagnostics;
using DataContracts;
using MaximumPath.PathFinders;
using Microsoft.Extensions.DependencyInjection;

namespace PerformanceTests
{
    public class PerformanceTestsConfig
    {
        public int NumberOfLevels { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var performanceTestsConfig = new PerformanceTestsConfig
            {
                NumberOfLevels = 5000
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMaximumPathFinder, SmartMaximumPathFinder>()
                .AddSingleton<IInputProvider, RandomPyramidGenerator>()
                .AddSingleton(performanceTestsConfig)
                .BuildServiceProvider();

            var inputProvider = serviceProvider.GetService<IInputProvider>();
            var pathFinder = serviceProvider.GetService<IMaximumPathFinder>();

            Console.Write($"Running benchmark...");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var watch = Stopwatch.StartNew();
            var pyramid = inputProvider.GetPyramid();
            watch.Stop();
            Console.WriteLine($"Random pyramid generated in {watch.ElapsedMilliseconds} ms.");

            watch.Reset();
            watch.Start();
            var result = pathFinder.FindPath(pyramid);
            watch.Stop();
            
            Console.WriteLine($"Maximum path found in {watch.ElapsedMilliseconds} ms.");
            Console.ReadLine();
        }
    }
}
