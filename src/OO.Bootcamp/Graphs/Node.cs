using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OO.Bootcamp.Graphs
{
    // Understands its neighbours
    public class Node
    {
        private readonly List<Edge> edges = new List<Edge>();

        public Node LinkTo(Node neighbour, int cost)
        {
            edges.Add(new Edge(neighbour, cost));
            return neighbour;
        }

        public bool CanReach(Node destination)
        {
            return !NoPath.Possible.Equals(PathTo(destination, new List<Node>(), ActualPath.CostStrategy));
        }

        public double CostTo(Node destination)
        {
            return PathTo(destination).Cost;
        }

        public int HopsTo(Node destination)
        {
            return PathTo(destination, ActualPath.HopCountStrategy).HopCount;
        }

        public Path PathTo(Node destination)
        {
            return PathTo(destination, ActualPath.CostStrategy);
        }

        private Path PathTo(Node destination, IComparer<Path> sortingStrategy)
        {
            var result =  PathTo(destination, new List<Node>(), sortingStrategy);
            if (result.Equals(NoPath.Possible))
            {
                throw new NoPathExistsException();
            }
            return result;
        }

        private Path PathTo(Node destination, List<Node> visitedNodes, IComparer<Path> sortingStrategy)
        {
            if (Equals(destination)) return new ActualPath();
            if (visitedNodes.Contains((this))) return NoPath.Possible;
            visitedNodes.Add(this);
            var validPaths = edges.Select(edge => edge.PathTo(destination, visitedNodes.Copy())).ToList();
            return validPaths.Any() ? validPaths.Minimum(sortingStrategy) : NoPath.Possible;
        }

        public class Edge
        {
            private readonly Node node;
            private readonly int cost;

            public Edge(Node node, int cost)
            {
                this.node = node;
                this.cost = cost;
            }

            internal Path PathTo(Node destination, List<Node> visitedNodes)
            {
                return node.PathTo(destination, visitedNodes, ActualPath.CostStrategy).Add(this);
            }

            public static double TotalCost(List<Edge> edges)
            {
                return edges.Aggregate<Edge, double>(0, (current, edge) => current + edge.cost);
            }
        }
    }

    public interface Path : IComparable<Path>
    {
        Path Add(Node.Edge edge);
        double Cost { get; }
        int HopCount { get; }
    }

    public class ActualPath : Path
    {
        private readonly List<Node.Edge> edges = new List<Node.Edge>();

        public Path Add(Node.Edge edge)
        {
            edges.Add(edge);
            return this;
        }

        public double Cost { get { return Node.Edge.TotalCost(edges); } }
        public int HopCount { get { return edges.Count; } }
        public static IComparer<Path> CostStrategy => new CostStrategyComparison();
        public static IComparer<Path> HopCountStrategy => new HopCountStrategyComparison();

        public int CompareTo(Path other)
        {
            return Cost.CompareTo(other.Cost);
        }

        private class CostStrategyComparison : IComparer<Path>
        {
            public int Compare(Path left, Path right)
            {
                return left.Cost.CompareTo(right.Cost);
            }
        }

        private class HopCountStrategyComparison : IComparer<Path>
        {
            public int Compare(Path left, Path right)
            {
                return left.HopCount.CompareTo(right.HopCount);
            }
        }
    }

    public class NoPath : Path
    {
        public static NoPath Possible = new NoPath();

        private NoPath() {}

        public Path Add(Node.Edge edge)
        {
            return this;
        }

        public double Cost { get {return double.PositiveInfinity;} }
        public int HopCount { get { return int.MaxValue; } }
        public int CompareTo(Path other) { return 1;}
    }


}