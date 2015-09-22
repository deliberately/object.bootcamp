using System;

namespace OO.Bootcamp
{
    // Understands the relationship between an amount and units of measurement
    public class Quantity : IComparable<Quantity>
    {
        private const double DoubleComparisonTolerance = 0.000001;
        private readonly double amount;
        private readonly ImperialMeasure unitOfMeasurement;

        public Quantity(double amount, ImperialMeasure unitOfMeasurement)
        {
            this.amount = amount;
            this.unitOfMeasurement = unitOfMeasurement;
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as Quantity);
        }

        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(other, null)) { return false; }

            var convertedQuantity = other.In(this.unitOfMeasurement);
            return Math.Abs(this.amount - convertedQuantity.amount) < DoubleComparisonTolerance && this.unitOfMeasurement == convertedQuantity.unitOfMeasurement;
        }

        public override int GetHashCode()
        {
            return amount.GetHashCode() ^ unitOfMeasurement.GetHashCode();
        }

        public Quantity In(ImperialMeasure desiredUnit)
        {
            return new Quantity(unitOfMeasurement.Convert(amount, desiredUnit), desiredUnit);
        }

        public int CompareTo(Quantity other)
        {
            Ensure.NotNull(other);
            return amount.CompareTo(other.In(this.unitOfMeasurement).amount);
        }

        public override string ToString()
        {
            return $"Quantity: {amount} {unitOfMeasurement}";
        }
    }

    public static class Ensure
    {
        public static void NotNull(object obj)
        {
            if(obj == null) {throw new ArgumentException();}
        }
    }

    public class ImperialMeasure
    {
        private readonly int factor;
        private readonly ImperialMeasure unit;
        public static readonly ImperialMeasure Teaspoon = new ImperialMeasure(1, Teaspoon);
        public static readonly ImperialMeasure Tablespoon = new ImperialMeasure(3, Teaspoon);
        public static readonly ImperialMeasure Ounce = new ImperialMeasure(2, Tablespoon);
        public static readonly ImperialMeasure Cup = new ImperialMeasure(8, Ounce);
        public static readonly ImperialMeasure Pint = new ImperialMeasure(2, Cup);
        public static readonly ImperialMeasure Quart = new ImperialMeasure(2, Pint);
        public static readonly ImperialMeasure Gallon = new ImperialMeasure(4, Quart);

        public ImperialMeasure(int factor, ImperialMeasure unit)
        {
            this.factor = factor;
            this.unit = unit;
        }

        public Quantity Amount(double value)
        {
            return new Quantity(value, this);
        }

        private double InBaseUnits(double value)
        {
            return unit?.InBaseUnits(value*factor) ?? value*factor;
        }

        public double Convert(double value, ImperialMeasure desiredUnit)
        {
            return InBaseUnits(value)/desiredUnit.InBaseUnits(1);
        }
    }
}