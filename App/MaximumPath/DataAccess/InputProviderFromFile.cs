using System.IO;
using System.Linq;
using DataContracts;
using MaximumPath.Config;

namespace MaximumPath.DataAccess
{
    public class InputProviderFromFile : IInputProvider
    {
        public InputProviderFromFile(InputFileConfig fileConfig)
        {
            FileConfig = fileConfig;
        }

        private InputFileConfig FileConfig { get; set; }

        public Pyramid GetPyramid()
        {
            var content = File.ReadAllLines(FileConfig.Filename);

            if (content == null)
                return null;

            // Parse file content
            var levels = content.Select(line =>
                new Pyramid.Level
                {
                    Nodes = line
                        .Split(" ")
                        .Select(long.Parse)
                        .ToArray()
                }
            ).ToList();

            return new Pyramid
            {
                Levels = levels
            };
        }
    }
}