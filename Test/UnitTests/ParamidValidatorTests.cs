using System.Collections.Generic;
using DataContracts;
using MaximumPath.Validators;
using NUnit.Framework;

namespace UnitTests
{
    public class ParamidValidatorTests
    {
        private readonly PyramidValidator validator = new PyramidValidator();

        [Test]
        public void IsValid_NullPyramidSupplied_ReturnsFalse()
        {
            var pyramid = (Pyramid) null;

            var result = validator.IsValid(pyramid);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_PyramidWithoutLevelsSupplied_ReturnsFalse()
        {
            var pyramid = new Pyramid
            {
                Levels = null
            };

            var result = validator.IsValid(pyramid);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_PyramidWithEmptyLevelsSupplied_ReturnsFalse()
        {
            var pyramid = new Pyramid
            {
                Levels = new List<Pyramid.Level>()
            };

            var result = validator.IsValid(pyramid);

            Assert.IsFalse(result);
        }


        [Test]
        public void IsValid_PyramidWithoutNodesSupplied_ReturnsFalse()
        {
            var pyramid = new Pyramid
            {
                Levels = new List<Pyramid.Level>
                {
                    new Pyramid.Level
                    {
                        Nodes = null
                    }
                }
            };

            var result = validator.IsValid(pyramid);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_PyramidWithEmptyNodesSupplied_ReturnsFalse()
        {
            var pyramid = new Pyramid
            {
                Levels = new List<Pyramid.Level>
                {
                    new Pyramid.Level
                    {
                        Nodes = new long[0]
                    }
                }
            };

            var result = validator.IsValid(pyramid);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_ImpossibleToFindPath_ReturnsTrue()
        {
            var pyramid = new Pyramid
            {
                Levels = new List<Pyramid.Level>
                {
                    new Pyramid.Level
                    {
                        Nodes = new long[]{3}
                    },
                    new Pyramid.Level
                    {
                        Nodes = new long[]{5, 7}
                    }
                }
            };

            var result = validator.IsValid(pyramid);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_FindsPath_ReturnsTrue()
        {
            var pyramid = new Pyramid
            {
                Levels = new List<Pyramid.Level>
                {
                    new Pyramid.Level
                    {
                        Nodes = new long[]{3}
                    },
                    new Pyramid.Level
                    {
                        Nodes = new long[]{4, 6}
                    }
                }
            };

            var result = validator.IsValid(pyramid);

            Assert.IsTrue(result);
        }
    }
}