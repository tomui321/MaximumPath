using System;
using System.Text;
using DataContracts;

namespace IntegrationTests
{
    public class TestOutputWriter : IOutputWriter
    {
        public StringBuilder OutputBuilder = new StringBuilder();

        public void WritePath(Result result)
        {
            if (result.Path != null)
            {
                OutputBuilder.Append($"Path: {string.Join(", ", result.Path)}");
            }
            else
            {
                OutputBuilder.Append("There is no valid path to the bottom!");
            }
        }

        public void WriteMaximumPossibleSum(Result result)
        {
            if (result.Path != null)
            {
                OutputBuilder.Append($"Max sum: {result.MaximumPossibleSum}");
                OutputBuilder.AppendLine();
            }
        }
    }
}
