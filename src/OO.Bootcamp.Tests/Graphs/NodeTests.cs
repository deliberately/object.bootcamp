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
}