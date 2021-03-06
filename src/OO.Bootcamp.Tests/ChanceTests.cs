﻿using System.Linq;
using NUnit.Framework;

namespace OO.Bootcamp.Tests
{
    // Understands the correct behaviour of Chance
    [TestFixture]
    public class ChanceTests
    {
        private readonly Chance oneInTwo = new Chance(0.5);
        private readonly Chance oneInFour = new Chance(0.25);
        private readonly Chance oneInEight = new Chance(0.125);
        private readonly Chance threeInFour = new Chance(0.75);

        [Test]
        public void ShouldPreventInvalidChanceValues()
        {
            Assert.That(() => new Chance(1.1), Throws.ArgumentException);
            Assert.That(() => new Chance(-0.1), Throws.ArgumentException);
        }

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

        [Test]
        public void ShouldBeAbleToCompareDifferentChances()
        {
            Assert.That(oneInFour.CompareTo(threeInFour), Is.EqualTo(-1));
            Assert.That(oneInFour.CompareTo(oneInFour), Is.EqualTo(0));
            Assert.That(threeInFour.CompareTo(oneInFour), Is.EqualTo(1));
            Assert.That(new []{oneInFour, oneInTwo, threeInFour}.Max(), Is.EqualTo(threeInFour));
        }
    }

}