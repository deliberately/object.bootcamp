using System;
using NUnit.Framework;

namespace OO.Bootcamp.Tests
{
    // Understands the correct behaviour of Probability
    [TestFixture]
    public class ProbabilityTests
    {
        private readonly Probability oneInTwo = new Probability(0.5);
        private readonly Probability oneInFour = new Probability(0.25);
        private readonly Probability oneInEight = new Probability(0.125);
        private readonly Probability threeInFour = new Probability(0.75);

        [Test]
        public void Should_understand_equal_probabilities()
        {
            Assert.That(oneInTwo, Is.EqualTo(oneInTwo));
            Assert.That(oneInTwo, Is.Not.EqualTo(oneInFour));
            Assert.AreNotEqual(oneInTwo, null);
            Assert.AreNotEqual(null, oneInFour);
        }

        [Test]
        public void ShouldCalculateTheProbabilityOfTwoEventsOccurring()
        {
            Assert.AreEqual(oneInTwo.And(oneInTwo), oneInFour);
            Assert.AreEqual(oneInTwo.And(oneInFour), oneInEight);
        }

        [Test]
        public void ShouldDetermineTheInverseOfAProbability()
        {
            Assert.AreEqual(oneInTwo.Not, oneInTwo);
            Assert.AreEqual(oneInFour.Not, threeInFour);
        }    
    }

}