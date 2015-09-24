using System;
using System.Collections.Generic;
using System.Linq;

namespace OO.Bootcamp.Graphs
{
    public class Node
    {
        private readonly List<Node> neighbours = new List<Node>();
        private const double NotFound = double.PositiveInfinity;

        public bool CanReach(Node destination)
        {
            return HopsTo(destination, new List<Node>()) != NotFound;
        }

        public int HopsTo(Node destination)
        {
            var result = HopsTo(destination, new List<Node>());
            if (result.Equals(NotFound))
            {
                throw new NoPathExistsException();
            }
            return Convert.ToInt32(result);
        }

        private double HopsTo(Node destination, List<Node> visitedNodes)
        {
            if (Equals(destination)) return 0;
            if (visitedNodes.Contains((this))) return NotFound;
            visitedNodes.Add(this);
            var validHops = neighbours.Select(n => n.HopsTo(destination, visitedNodes.Copy()) + 1).ToList();
            return validHops.Any() ? validHops.Min() : NotFound;
        }

        public Node LinkTo(Node neighbour)
        {
            neighbours.Add(neighbour);
            return neighbour;
        }
    }
}