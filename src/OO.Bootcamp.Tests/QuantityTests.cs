using System;
using NUnit.Framework;

namespace OO.Bootcamp.Tests
{
    // Understands the correct behaviour of Quantity
    [TestFixture]
    public class QuantityTests
    {
        private readonly Quantity oneTeaspoon = ImperialMeasure.Teaspoon.Amount(1);
        private readonly Quantity oneTablespoon = ImperialMeasure.Tablespoon.Amount(1);
        private readonly Quantity oneOunce = ImperialMeasure.Ounce.Amount(1);
        private readonly Quantity oneCup = ImperialMeasure.Cup.Amount(1);
        private readonly Quantity onePint = ImperialMeasure.Pint.Amount(1);
        private readonly Quantity oneQuart = ImperialMeasure.Quart.Amount(1);
        private readonly Quantity oneGallon = ImperialMeasure.Gallon.Amount(1);

        [Test]
        public void ShouldValidateEquality()
        {
            Assert.AreEqual(oneTeaspoon, oneTeaspoon);
            Assert.False(oneTeaspoon.Equals(null));
            Assert.False(oneTeaspoon.Equals(new object()));
        }

        [Test]
        public void ShouldEnsureHashCodeIsEqualForTwoDifferentObjects()
        {
            Assert.AreEqual(oneTeaspoon.GetHashCode(), oneTeaspoon.GetHashCode());
        }

        [Test]
        public void ShouldDetermineEqualityBetweenDifferentUnits()
        {
            Assert.AreEqual(ImperialMeasure.Teaspoon.Amount(3), oneTablespoon);
            Assert.AreEqual(ImperialMeasure.Tablespoon.Amount(2), oneOunce);
            Assert.AreEqual(ImperialMeasure.Ounce.Amount(8), oneCup);
            Assert.AreEqual(ImperialMeasure.Cup.Amount(2), onePint);
            Assert.AreEqual(ImperialMeasure.Pint.Amount(2), oneQuart);
            Assert.AreEqual(ImperialMeasure.Quart.Amount(4), oneGallon);
            Assert.AreEqual(ImperialMeasure.Quart.Amount(0.25), ImperialMeasure.Ounce.Amount(8));
            Assert.AreEqual(ImperialMeasure.Tablespoon.Amount(2/3.0), ImperialMeasure.Teaspoon.Amount(2));
        }

        [Test]
        public void ShouldCompareDifferentQuantities()
        {
            Assert.That(ImperialMeasure.Teaspoon.Amount(1), Is.LessThan(ImperialMeasure.Teaspoon.Amount(2)));
            Assert.That(ImperialMeasure.Teaspoon.Amount(1), Is.LessThan(ImperialMeasure.Tablespoon.Amount(1)));
            Assert.That(ImperialMeasure.Pint.Amount(0.5), Is.GreaterThan(ImperialMeasure.Ounce.Amount(7)));
            Assert.Throws<ArgumentException>(() => ImperialMeasure.Teaspoon.Amount(1).CompareTo(null));
        }

        [Test]
        public void ShouldConvertToDifferentUnits()
        {
            Assert.AreEqual(ImperialMeasure.Tablespoon.Amount(12).In(ImperialMeasure.Cup), ImperialMeasure.Cup.Amount(0.75));
        }
    }
}