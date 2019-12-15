using System.Collections.Generic;

namespace DataContracts
{
    public interface IMaximumPathFinder
    {
        List<long> FindPath(Pyramid pyramid);
    }

    public interface IInputProvider
    {
        Pyramid GetPyramid();
    }

    public interface IPyramidValidator
    {
        bool IsValid(Pyramid pyramid);
    }

    public interface IOutputWriter
    {
        void WritePath(Result result);
        void WriteMaximumPossibleSum(Result result);
    }
}
