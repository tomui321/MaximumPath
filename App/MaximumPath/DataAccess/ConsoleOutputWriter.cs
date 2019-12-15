using System;
using DataContracts;

namespace MaximumPath.DataAccess
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WritePath(Result result)
        {
            if (result != null && result.Path != null)
            {
                Console.WriteLine($"Path: {string.Join(", ", result.Path)}");
            }
            else
            {
                Console.WriteLine("There is no valid path to the bottom!");
            }
        }

        public void WriteMaximumPossibleSum(Result result)
        {
            if (result != null && result.Path != null)
            {
                Console.WriteLine($"Max sum: {result.MaximumPossibleSum}");
            }
        }
    }
}
