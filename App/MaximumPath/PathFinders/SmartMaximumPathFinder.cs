using System.Collections.Generic;
using System.Linq;
using DataContracts;
using MoreLinq;

namespace MaximumPath.PathFinders
{
    public class SmartMaximumPathFinder : IMaximumPathFinder
    {
        public List<long> FindPath(Pyramid pyramid)
        {
            var internalPyramidModel = MapPyramidToInternalModel(pyramid);
            FindMaximumTheoreticalSumOnNodes(internalPyramidModel);

            return ReadMaximumPath(internalPyramidModel);
        }

        private InternalPyramidModel MapPyramidToInternalModel(Pyramid pyramid)
        {
            var model = new InternalPyramidModel
            {
                Levels = pyramid.Levels.Select(level =>
                    new InternalPyramidModel.Level
                    {
                        Nodes = level.Nodes.Select(node =>
                            new InternalPyramidModel.Node
                            {
                                NodeValue = node,
                                TheoreticalSumOfLevelsBelow = null,
                                Children = null
                            }).ToArray()
                    }).ToArray()
            };

            // Assign children
            for (int levelIndex = 0; levelIndex < model.Levels.Length - 1; levelIndex++)
            {
                var currentLevel = model.Levels[levelIndex];
                var levelBelow = model.Levels[levelIndex + 1];

                for (int nodeIndex = 0; nodeIndex < currentLevel.Nodes.Length; nodeIndex++)
                {
                    currentLevel.Nodes[nodeIndex].Children = new[]
                    {
                        levelBelow.Nodes[nodeIndex],
                        levelBelow.Nodes[nodeIndex + 1]
                    };
                }
            }

            return model;
        }

        private void FindMaximumTheoreticalSumOnNodes(InternalPyramidModel internalPyramidModel)
        {
            for (var i = internalPyramidModel.Levels.Length - 1; i >= 0; i--)
            {
                foreach (var currentNode in internalPyramidModel.Levels[i].Nodes)
                {
                    if (currentNode.IsBottomOfThePyramid())
                    {
                        currentNode.TheoreticalSumOfLevelsBelow = currentNode.NodeValue;
                        continue;
                    }

                    var suitableChildren = GetSuitableChildNodes(currentNode).ToArray();

                    if (!suitableChildren.Any())
                    {
                        currentNode.MarkPathImpossible();
                    }
                    else
                    {
                        var bestPathValue = suitableChildren.Max(x => x.TheoreticalSumOfLevelsBelow.Value);
                        currentNode.TheoreticalSumOfLevelsBelow = currentNode.NodeValue + bestPathValue;
                    }
                }
            }
        }

        private IEnumerable<InternalPyramidModel.Node> GetSuitableChildNodes(InternalPyramidModel.Node currentNode)
        {
            return currentNode.NodeValue.IsEven() ?
                currentNode.Children.Where(child => child.IsPathPossible && child.NodeValue.IsOdd()) :
                currentNode.Children.Where(child => child.IsPathPossible && child.NodeValue.IsEven());
        }

        private List<long> ReadMaximumPath(InternalPyramidModel pyramid)
        {
            if (pyramid.Levels.Any(x => x.Nodes.All(node => !node.IsPathPossible)))
            {
                return null;
            }

            var path = new List<long>(pyramid.Levels.Length);
            ProceedToTheNextNodeNextNode(pyramid.Levels.First().Nodes.Single(), path);

            return path;
        }

        private void ProceedToTheNextNodeNextNode(InternalPyramidModel.Node currentNode, List<long> path)
        {
            if (currentNode.IsBottomOfThePyramid())
            {
                path.Add(currentNode.NodeValue);
                return;
            }

            path.Add(currentNode.NodeValue);

            var suitableChildren = GetSuitableChildNodes(currentNode).ToArray();
            var bestChild = suitableChildren.MaxBy(x => x.TheoreticalSumOfLevelsBelow).First();
            ProceedToTheNextNodeNextNode(bestChild, path);
        }

        private class InternalPyramidModel
        {
            public Level[] Levels { get; set; }

            public class Level
            {
                public Node[] Nodes { get; set; }
            }

            public class Node
            {
                public long NodeValue { get; set; }
                public Node[] Children { get; set; }

                public long? TheoreticalSumOfLevelsBelow { get; set; }
                public bool IsPathPossible => TheoreticalSumOfLevelsBelow != null;

                public void MarkPathImpossible()
                {
                    TheoreticalSumOfLevelsBelow = null;
                }
                public bool IsBottomOfThePyramid()
                {
                    return Children == null;
                }
            }
        }
    }
}