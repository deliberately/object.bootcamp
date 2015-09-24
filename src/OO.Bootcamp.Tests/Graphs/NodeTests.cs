using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace OO.Bootcamp.Tests.Graphs
{
    [TestFixture]
    public class NodeTests
    {
        private Node a = new Node("a");
        private Node b = new Node("b");
        private Node c = new Node("c");
        private Node d = new Node("d");
        private Node e = new Node("e");
        private Node f = new Node("f");
        private Node g = new Node("g");

        [SetUp]
        public void Setup()
        {
            b.LinkTo(a);
            b.LinkTo(c).LinkTo(d).LinkTo(e).LinkTo(b).LinkTo(f);
            c.LinkTo(d);
        }

        [Test]
        public void ShouldBeAbleToReachItself()
        {
            Assert.That(a.CanReach(a));
            Assert.False(a.CanReach(null));
        }

        [Test]
        public void ShouldDetermineWhetherItCanReachOtherNodes()
        {
            Assert.That(c.CanReach(d));
            Assert.That(c.CanReach(f));
        }
    }

    public class Node
    {
        private readonly List<Node> neighbours = new List<Node>();

        public Node(string name)
        {
            
        }

        public bool CanReach(Node destination)
        {
            return CanReach(destination, new List<Node>());
        }

        private bool CanReach(Node destination, List<Node> visitedNodes)
        {
            if (this.Equals(destination)) return true;
            if (visitedNodes.Contains((this))) return false;
            return neighbours.Any(n => n.CanReach(destination, visitedNodes.With(this)));
        }

        public Node LinkTo(Node neighbour)
        {
            neighbours.Add(neighbour);
            return neighbour;
        }
    }

    public static class ListExtensions
    {
        public static List<T> With<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }
    }
}