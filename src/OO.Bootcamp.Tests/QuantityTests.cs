﻿using System;
using NUnit.Framework;

namespace OO.Bootcamp.Tests
{
    // Understands the correct behaviour of Quantity
    [TestFixture]
    public class QuantityTests
    {
        private readonly Quantity oneTeaspoon = Unit.Teaspoon.Amount(1);
        private readonly Quantity oneTablespoon = Unit.Tablespoon.Amount(1);
        private readonly Quantity oneOunce = Unit.Ounce.Amount(1);
        private readonly Quantity oneCup = Unit.Cup.Amount(1);
        private readonly Quantity onePint = Unit.Pint.Amount(1);
        private readonly Quantity oneQuart = Unit.Quart.Amount(1);
        private readonly Quantity oneGallon = Unit.Gallon.Amount(1);

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
            Assert.AreEqual(Unit.Teaspoon.Amount(1).GetHashCode(), oneTeaspoon.GetHashCode());
        }

        [Test]
        public void ShouldDetermineEqualityBetweenDifferentUnits()
        {
            Assert.AreEqual(Unit.Teaspoon.Amount(3), oneTablespoon);
            Assert.AreEqual(Unit.Tablespoon.Amount(2), oneOunce);
            Assert.AreEqual(Unit.Ounce.Amount(8), oneCup);
            Assert.AreEqual(Unit.Cup.Amount(2), onePint);
            Assert.AreEqual(Unit.Pint.Amount(2), oneQuart);
            Assert.AreEqual(Unit.Quart.Amount(4), oneGallon);
            Assert.AreEqual(Unit.Quart.Amount(0.25), Unit.Ounce.Amount(8));
            Assert.AreEqual(Unit.Tablespoon.Amount(2/3.0), Unit.Teaspoon.Amount(2));
        }

        [Test]
        public void ShouldDetermineEqualityForUnitUnits()
        {
            Assert.That(Unit.Inch.Amount(12), Is.EqualTo(Unit.Feet.Amount(1)));
            Assert.That(Unit.Feet.Amount(12), Is.EqualTo(Unit.Yard.Amount(4)));
            Assert.That(Unit.Yard.Amount(3520), Is.EqualTo(Unit.Mile.Amount(2)));
        }

        [Test]
        public void ShouldPreventAdditionOfQuantitiesWithDifferentUnitTypes()
        {
            Assert.That(() => oneTeaspoon + Unit.Feet.Amount(1), Throws.TypeOf<DifferentUnitTypeException>());
        }

        [Test]
        public void ShouldCompareDifferentQuantities()
        {
            Assert.That(Unit.Teaspoon.Amount(1), Is.LessThan(Unit.Teaspoon.Amount(2)));
            Assert.That(Unit.Teaspoon.Amount(1), Is.LessThan(Unit.Tablespoon.Amount(1)));
            Assert.That(Unit.Pint.Amount(0.5), Is.GreaterThan(Unit.Ounce.Amount(7)));
            Assert.Throws<ArgumentException>(() => Unit.Teaspoon.Amount(1).CompareTo(null));
        }

        [Test]
        public void ShouldConvertToDifferentUnits()
        {
            Assert.AreEqual(Unit.Tablespoon.Amount(12).In(Unit.Cup), Unit.Cup.Amount(0.75));
        }

        [Test]
        public void ShouldAddQuantitiesTogether()
        {
            Assert.AreEqual(oneTeaspoon + oneTeaspoon, Unit.Teaspoon.Amount(2));
            Assert.AreEqual(oneTeaspoon + oneTablespoon, Unit.Ounce.Amount(2/3.0));
        }

        [Test]
        public void ShouldSubtractQuantitiesFromOneAnother()
        {
            Assert.AreEqual(oneTablespoon - oneTeaspoon, Unit.Teaspoon.Amount(2));
            Assert.AreEqual(Unit.Pint.Amount(0.25) - oneOunce, Unit.Ounce.Amount(3));
            Assert.AreEqual(Unit.Teaspoon.Amount(2) - Unit.Tablespoon.Amount(2), Unit.Teaspoon.Amount(-4));
            Assert.AreEqual(-oneTablespoon, Unit.Tablespoon.Amount(-1));
        }

        [Test]
        public void ShouldMultiplyQuantities()
        {
            Assert.AreEqual(oneTablespoon * 2, Unit.Tablespoon.Amount(2));
            Assert.AreEqual(Unit.Cup.Amount(3) * (2/3.0), onePint);
        }

        [Test]
        public void ShouldDivideQuantities()
        {
            Assert.AreEqual(oneTablespoon / 1, oneTablespoon);
            Assert.AreEqual(oneTablespoon / 3, oneTeaspoon);
        }
    }
}