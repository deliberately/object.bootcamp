using System;
using NUnit.Framework;

namespace OO.Bootcamp.Tests
{
    // Understands the probability of an event occurring
    [TestFixture]
    public class ChanceTests
    {
        private readonly Chance oneInTwo = new Chance(0.5);
        private readonly Chance oneInFour = new Chance(0.25);
        private readonly Chance oneInEight = new Chance(0.125);
        private readonly Chance threeInFour = new Chance(0.75);

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

        [Test]
        public void ShouldProvideAbilityToSubtractProbabilities()
        {
            Assert.AreEqual(Chance.Certain - oneInTwo, oneInTwo);
            Assert.AreEqual(Chance.Certain - oneInFour, threeInFour);
        }

        [Test]
        public void ShouldDetermineChanceOfOneOrAnotherEventOccuring()
        {
            Assert.AreEqual(oneInFour.Or(oneInFour), new Chance(0.4375));
        }
    }

}