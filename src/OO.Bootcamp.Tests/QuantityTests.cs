using System;
using NUnit.Framework;

namespace OO.Bootcamp.Tests
{
    // Understands the correct behaviour of IntervalQuantity
    [TestFixture]
    public class QuantityTests
    {
        private readonly RatioQuantity oneTeaspoon = RatioUnit.Teaspoon.Amount(1);
        private readonly RatioQuantity oneTablespoon = RatioUnit.Tablespoon.Amount(1);
        private readonly RatioQuantity oneOunce = RatioUnit.Ounce.Amount(1);
        private readonly RatioQuantity oneCup = RatioUnit.Cup.Amount(1);
        private readonly RatioQuantity onePint = RatioUnit.Pint.Amount(1);
        private readonly RatioQuantity oneQuart = RatioUnit.Quart.Amount(1);
        private readonly RatioQuantity oneGallon = RatioUnit.Gallon.Amount(1);

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
            Assert.AreEqual(RatioUnit.Teaspoon.Amount(1).GetHashCode(), oneTeaspoon.GetHashCode());
        }

        [Test]
        public void ShouldDetermineEqualityBetweenDifferentUnits()
        {
            Assert.AreEqual(RatioUnit.Teaspoon.Amount(3), oneTablespoon);
            Assert.AreEqual(RatioUnit.Tablespoon.Amount(2), oneOunce);
            Assert.AreEqual(RatioUnit.Ounce.Amount(8), oneCup);
            Assert.AreEqual(RatioUnit.Cup.Amount(2), onePint);
            Assert.AreEqual(RatioUnit.Pint.Amount(2), oneQuart);
            Assert.AreEqual(RatioUnit.Quart.Amount(4), oneGallon);
            Assert.AreEqual(RatioUnit.Quart.Amount(0.25), RatioUnit.Ounce.Amount(8));
            Assert.AreEqual(RatioUnit.Tablespoon.Amount(2/3.0), RatioUnit.Teaspoon.Amount(2));
        }

        [Test]
        public void ShouldDetermineEqualityForDistanceUnits()
        {
            Assert.That(RatioUnit.Inch.Amount(12), Is.EqualTo(RatioUnit.Feet.Amount(1)));
            Assert.That(RatioUnit.Feet.Amount(12), Is.EqualTo(RatioUnit.Yard.Amount(4)));
            Assert.That(RatioUnit.Yard.Amount(3520), Is.EqualTo(RatioUnit.Mile.Amount(2)));
        }

        [Test]
        public void ShouldDeterminEqualityForDifferentTemperatures()
        {
            Assert.That(IntervalUnit.Celsius.Amount(-40), Is.EqualTo(IntervalUnit.Fahrenheit.Amount(-40)));
            Assert.That(IntervalUnit.Celsius.Amount(0), Is.EqualTo(IntervalUnit.Fahrenheit.Amount(32)));
            Assert.That(IntervalUnit.Celsius.Amount(10), Is.EqualTo(IntervalUnit.Fahrenheit.Amount(50)));
            Assert.That(IntervalUnit.Celsius.Amount(100), Is.EqualTo(IntervalUnit.Fahrenheit.Amount(212)));
        }

        [Test]
        public void ShouldDetermineInequalityBetweenDifferentUnitTypes()
        {
            Assert.False(oneTeaspoon.Equals(RatioUnit.Inch.Amount(1)));
        }

        [Test]
        public void ShouldPreventAdditionOfQuantitiesWithDifferentUnitTypes()
        {
            Assert.That(() => oneTeaspoon + RatioUnit.Feet.Amount(1), Throws.TypeOf<IncompatibleUnitsException>());
        }

        [Test]
        public void ShouldCompareDifferentQuantities()
        {
            Assert.That(RatioUnit.Teaspoon.Amount(1), Is.LessThan(RatioUnit.Teaspoon.Amount(2)));
            Assert.That(RatioUnit.Teaspoon.Amount(1), Is.LessThan(RatioUnit.Tablespoon.Amount(1)));
            Assert.That(RatioUnit.Pint.Amount(0.5), Is.GreaterThan(RatioUnit.Ounce.Amount(7)));
            Assert.Throws<ArgumentException>(() => RatioUnit.Teaspoon.Amount(1).CompareTo(null));
        }

        [Test]
        public void ShouldConvertToDifferentUnits()
        {
            Assert.AreEqual(RatioUnit.Tablespoon.Amount(12).In(RatioUnit.Cup), RatioUnit.Cup.Amount(0.75));
        }

        [Test]
        public void ShouldAddQuantitiesTogether()
        {
            Assert.AreEqual(oneTeaspoon + oneTeaspoon, RatioUnit.Teaspoon.Amount(2));
            Assert.AreEqual(oneTeaspoon + oneTablespoon, RatioUnit.Ounce.Amount(2/3.0));
        }

        [Test]
        public void ShouldSubtractQuantitiesFromOneAnother()
        {
            Assert.AreEqual(oneTablespoon - oneTeaspoon, RatioUnit.Teaspoon.Amount(2));
            Assert.AreEqual(RatioUnit.Pint.Amount(0.25) - oneOunce, RatioUnit.Ounce.Amount(3));
            Assert.AreEqual(RatioUnit.Teaspoon.Amount(2) - RatioUnit.Tablespoon.Amount(2), RatioUnit.Teaspoon.Amount(-4));
            Assert.AreEqual(-oneTablespoon, RatioUnit.Tablespoon.Amount(-1));
        }

        [Test]
        public void ShouldMultiplyQuantities()
        {
            Assert.AreEqual(oneTablespoon * 2, RatioUnit.Tablespoon.Amount(2));
            Assert.AreEqual(RatioUnit.Cup.Amount(3) * (2/3.0), onePint);
        }

        [Test]
        public void ShouldDivideQuantities()
        {
            Assert.AreEqual(oneTablespoon / 1, oneTablespoon);
            Assert.AreEqual(oneTablespoon / 3, oneTeaspoon);
        }

        [Test]
        public void ShouldPreventConversionToUnitOfADifferentType()
        {
            Assert.Throws<IncompatibleUnitsException>(() => oneTeaspoon.In(IntervalUnit.Celsius));
        }

        [Test]
        public void ShouldPrintAmountAndUnitOfAQuantity()
        {
            Assert.That(oneTeaspoon.ToString(), Is.EqualTo("1 tsp"));
            Assert.That(oneTablespoon.ToString(), Is.EqualTo("1 tbsp"));
        }
    }
}