using System.Collections.Generic;

namespace DataContracts
{
    public class Result
    {
        public long MaximumPossibleSum { get; set; }
        public List<long> Path { get; set; }
    }
}