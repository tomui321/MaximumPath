using System.Linq;
using DataContracts;

namespace MaximumPath.Validators
{
    public class PyramidValidator : IPyramidValidator
    {
        public bool IsValid(Pyramid pyramid)
        {
            if (pyramid == null)
                return false;

            if (pyramid.Levels == null || !pyramid.Levels.Any())
                return false;

            if (pyramid.Levels.Any(x => x.Nodes == null) || pyramid.Levels.Any(x => !x.Nodes.Any()))
                return false;

            return true;
        }
    }
}