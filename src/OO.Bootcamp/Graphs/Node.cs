using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OO.Bootcamp.Graphs
{
    // Understands its neighbours
    public class Node
    {
        private static readonly Func<int, int> HopCountStrategy = cost => 1;
        private const double NotFound = double.PositiveInfinity;

        private readonly List<Edge> edges = new List<Edge>();

        public Node LinkTo(Node neighbour, int cost)
        {
            edges.Add(new Edge(neighbour, cost));
            return neighbour;
        }

        public bool CanReach(Node destination)
        {
            return !double.IsPositiveInfinity(CalculateCostTo(destination, new List<Node>(), HopCountStrategy));
        }

        public int HopsTo(Node destination)
        {
            var result = CalculateCostTo(destination, new List<Node>(), HopCountStrategy);
            if (result.Equals(NotFound))
            {
                throw new NoPathExistsException();
            }

            return Convert.ToInt32(result);
        }

        public double WeightedPathTo(Node destination)
        {
            return PathTo(destination).Cost;
        }

        public Path PathTo(Node destination)
        {
            var result = PathTo(destination, new List<Node>());
            if (result.Equals(NoPath.Possible))
            {
                throw new NoPathExistsException();
            }
            return result;
        }

        private double CalculateCostTo(Node destination, List<Node> visitedNodes, Func<int, int> costStrategy)
        {
            if (Equals(destination)) return 0;
            if (visitedNodes.Contains((this))) return NotFound;
            visitedNodes.Add(this);
            var validHops = edges.Select(edge => edge.HopsTo(destination, visitedNodes.Copy(), costStrategy)).ToList();
            return validHops.Any() ? validHops.Min() : NotFound;
        }


        private Path PathTo(Node destination, List<Node> visitedNodes)
        {
            if (Equals(destination)) return new ActualPath();
            if (visitedNodes.Contains((this))) return NoPath.Possible;
            visitedNodes.Add(this);
            var validPaths = edges.Select(edge => edge.PathTo(destination, visitedNodes.Copy())).ToList();
            return validPaths.Any() ? validPaths.Min() : NoPath.Possible;
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

            internal double HopsTo(Node destination, List<Node> visitedNodes, Func<int, int> costStrategy)
            {
                return node.CalculateCostTo(destination, visitedNodes, costStrategy) + costStrategy(cost);
            }

            internal Path PathTo(Node destination, List<Node> visitedNodes)
            {
                return node.PathTo(destination, visitedNodes).Add(this);
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

        public int CompareTo(Path other)
        {
            return Cost.CompareTo(other.Cost);
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