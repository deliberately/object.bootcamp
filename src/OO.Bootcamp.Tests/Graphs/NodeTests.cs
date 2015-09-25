using NUnit.Framework;
using OO.Bootcamp.Graphs;

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
            b.LinkTo(a, 6);
            b.LinkTo(c, 5).LinkTo(d, 2).LinkTo(e, 3).LinkTo(b, 4).LinkTo(f, 7);
            c.LinkTo(e, 8);
            c.LinkTo(d, 1);
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
            Assert.That(b.HopsTo(f), Is.EqualTo(1));
        }

        [Test]
        public void ShouldFailWhenAttemptingToReachAnUnreachableNode()
        {
            Assert.Throws<NoPathExistsException>(() => a.HopsTo(b));
            Assert.Throws<NoPathExistsException>(() => a.WeightedPathTo(b));
        }

        [Test]
        public void ShouldCalculateWeightOfPath()
        {
            Assert.That(b.WeightedPathTo(b), Is.EqualTo(0));
            Assert.That(b.WeightedPathTo(a), Is.EqualTo(6));
            Assert.That(b.WeightedPathTo(e), Is.EqualTo(9));
            Assert.That(b.WeightedPathTo(f), Is.EqualTo(7));
            Assert.That(c.WeightedPathTo(f), Is.EqualTo(15));
        }

        [Test]
        public void ShouldCalculatePathToANode()
        {
            Assert.That(b.PathTo(b).Cost, Is.EqualTo(0));
            Assert.That(b.PathTo(a).Cost, Is.EqualTo(6));
            Assert.That(b.PathTo(e).Cost, Is.EqualTo(9));
            Assert.That(b.PathTo(f).Cost, Is.EqualTo(7));
            Assert.That(c.PathTo(f).Cost, Is.EqualTo(15));
        }
    }
}