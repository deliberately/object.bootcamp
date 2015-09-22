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

        public static Quantity operator +(Quantity left, Quantity right)
        {
            return new Quantity(left.amount + right.In(left.unitOfMeasurement).amount, left.unitOfMeasurement);
        }

        public static Quantity operator -(Quantity left, Quantity right)
        {
            return left + (right * -1);
        }

        public static Quantity operator -(Quantity original)
        {
            return original * -1;
        }

        public static Quantity operator *(Quantity left, double factor)
        {
            return new Quantity(left.amount * factor, left.unitOfMeasurement);
        }

        public static Quantity operator /(Quantity left, double factor)
        {
            return new Quantity(left.amount / factor, left.unitOfMeasurement);
        }

        public override string ToString()
        {
            return $"Quantity: {amount} {unitOfMeasurement}";
        }
    }
}