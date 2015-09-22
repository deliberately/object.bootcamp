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
        }
    }
}