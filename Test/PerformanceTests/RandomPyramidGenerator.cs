using System.Collections.Generic;
using System.Linq;
using DataContracts;

namespace PerformanceTests
{
    public class RandomPyramidGenerator : IInputProvider
    {
        private PerformanceTestsConfig Config { get; }

        public RandomPyramidGenerator(PerformanceTestsConfig config)
        {
            this.Config = config;
        }

        public Pyramid GetPyramid()
        {
            var levels = new List<Pyramid.Level>();

            for (var i = 0; i < Config.NumberOfLevels; i++)
            {
                var nodes = i % 2 == 0 ?
                    GenerateEvenNumbersSequence(i + 1) :
                    GenerateOddNumbersSequence(i + 1);
                
                levels.Add(new Pyramid.Level
                {
                    Nodes = nodes.Select(x => (long)x).ToArray()
                });
            }

            var pyramid = new Pyramid
            {
                Levels = levels
            };

            return pyramid;
        }

        private IEnumerable<int> GenerateOddNumbersSequence(int numberOfItems)
        {
            return Enumerable.Repeat(3, numberOfItems);
        }

        private IEnumerable<int> GenerateEvenNumbersSequence(int numberOfItems)
        {
            return Enumerable.Repeat(2, numberOfItems);
        }
    }
}