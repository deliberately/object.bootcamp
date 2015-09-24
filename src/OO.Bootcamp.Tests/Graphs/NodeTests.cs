using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace OO.Bootcamp.Tests.Graphs
{
    [TestFixture]
    public class NodeTests
    {
        private Node a = new Node();
        private Node b = new Node();
        private Node c = new Node();
        private Node d = new Node();
        private Node e = new Node();
        private Node f = new Node();
        private Node g = new Node();

        [SetUp]
        public void Setup()
        {
            b.LinkTo(a);
            b.LinkTo(c).LinkTo(d).LinkTo(e).LinkTo(b).LinkTo(f);
            c.LinkTo(e);
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

        [Test]
        public void ShouldDetermineHopCount()
        {
            Assert.That(b.HopsTo(b), Is.EqualTo(0));
            Assert.That(b.HopsTo(a), Is.EqualTo(1));
            Assert.That(c.HopsTo(e), Is.EqualTo(1));
            Assert.That(c.HopsTo(f), Is.EqualTo(3));
        }

        [Test]
        public void ShouldFailWhenAttemptingToReachAnUnreachableNode()
        {
            Assert.Throws<NoPathExistsException>(() => a.HopsTo(b));
        }
    }

    public class NoPathExistsException : Exception
    {
        
    }
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

    public static class ListExtensions
    {
        public static List<T> With<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> Copy<T>(this List<T> list)
        {
            return new List<T>(list);
        }
    }
}