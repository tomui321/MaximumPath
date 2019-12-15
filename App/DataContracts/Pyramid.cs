using System.Collections.Generic;

namespace DataContracts
{
    public class Pyramid
    {
        public List<Level> Levels { get; set; }

        public class Level
        {
            public long[] Nodes { get; set; }
        }
    }
}