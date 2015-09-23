using System;

namespace OO.Bootcamp
{
    // Understands the relationship between an amount and units of measurement
    public class IntervalQuantity : IComparable<IntervalQuantity>
    {
        private const double DoubleComparisonTolerance = 0.000001;
        protected readonly double amount;
        protected readonly Unit unit;

        public IntervalQuantity(double amount, Unit unit)
        {
            this.amount = amount;
            this.unit = unit;
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as IntervalQuantity);
        }

        public bool Equals(IntervalQuantity other)
        {
            if (ReferenceEquals(other, null) || unit.IsIncompatibleWith(other.unit)) { return false; }

            var convertedQuantity = other.In(unit);
            return Math.Abs(amount - convertedQuantity.amount) < DoubleComparisonTolerance && unit == convertedQuantity.unit;
        }

        public override int GetHashCode()
        {
            return amount.GetHashCode() ^ unit.GetHashCode();
        }

        public IntervalQuantity In(Unit desiredUnit)
        {
            EnsureSameUnitTypes(unit, desiredUnit);
            return new IntervalQuantity(unit.Convert(amount, desiredUnit), desiredUnit);
        }

        public int CompareTo(IntervalQuantity other)
        {
            Ensure.NotNull(other);
            return amount.CompareTo(other.In(unit).amount);
        }

        protected static void EnsureSameUnitTypes(Unit leftUnit, Unit rightUnit)
        {
            if(leftUnit.IsIncompatibleWith(rightUnit)) throw new IncompatibleUnitsException();
        }

        public override string ToString()
        {
            return $"{amount} {unit}";
        }
    }
    public class IncompatibleUnitsException : Exception
    {
        
    }
}