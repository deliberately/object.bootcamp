using System;
using System.Collections.Generic;
using System.Linq;

namespace OO.Bootcamp.Graphs
{
    // Understands its neighbours
    public class Node
    {
        private static readonly Func<int, int> HopCountStrategy = cost => 1;
        private static readonly Func<int, int> EdgeCostStrategy = cost => cost;
        private const double NotFound = double.PositiveInfinity;

        private readonly List<Edge> edges = new List<Edge>();
        private double result;

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
            return CalculateCostTo(destination, HopCountStrategy);
        }

        public int WeightedPathTo(Node destination)
        {
            return CalculateCostTo(destination, EdgeCostStrategy);
        }

        private int CalculateCostTo(Node destination, Func<int, int> hopCountStrategy)
        {
            var result = CalculateCostTo(destination, new List<Node>(), hopCountStrategy);
            if (result.Equals(NotFound))
            {
                throw new NoPathExistsException();
            }
            return Convert.ToInt32(result);
        }

        private double CalculateCostTo(Node destination, List<Node> visitedNodes, Func<int, int> costStrategy)
        {
            if (Equals(destination)) return 0;
            if (visitedNodes.Contains((this))) return NotFound;
            visitedNodes.Add(this);
            var validHops = edges.Select(edge => edge.HopsTo(destination, visitedNodes.Copy(), costStrategy)).ToList();
            return validHops.Any() ? validHops.Min() : NotFound;
        }

        private class Edge
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
        }
    }


}