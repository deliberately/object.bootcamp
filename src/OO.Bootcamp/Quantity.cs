using System;

namespace OO.Bootcamp
{
    // Understands the relationship between an amount and units of measurement
    public class IntervalQuantity : IComparable<IntervalQuantity>
    {
        private const double DoubleComparisonTolerance = 0.000001;
        protected readonly double amount;
        protected readonly Unit unitOfMeasurement;

        public IntervalQuantity(double amount, Unit unitOfMeasurement)
        {
            this.amount = amount;
            this.unitOfMeasurement = unitOfMeasurement;
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as IntervalQuantity);
        }

        public bool Equals(IntervalQuantity other)
        {
            if (ReferenceEquals(other, null) || this.unitOfMeasurement.IsIncompatibleWith(other.unitOfMeasurement)) { return false; }

            var convertedQuantity = other.In(this.unitOfMeasurement);
            return Math.Abs(this.amount - convertedQuantity.amount) < DoubleComparisonTolerance && this.unitOfMeasurement == convertedQuantity.unitOfMeasurement;
        }

        public override int GetHashCode()
        {
            return amount.GetHashCode() ^ unitOfMeasurement.GetHashCode();
        }

        public IntervalQuantity In(Unit desiredUnit)
        {
            EnsureSameUnitTypes(unitOfMeasurement, desiredUnit);
            return new IntervalQuantity(unitOfMeasurement.Convert(amount, desiredUnit), desiredUnit);
        }

        public int CompareTo(IntervalQuantity other)
        {
            Ensure.NotNull(other);
            return amount.CompareTo(other.In(this.unitOfMeasurement).amount);
        }

        protected static void EnsureSameUnitTypes(Unit leftUnit, Unit rightUnit)
        {
            if(leftUnit.IsIncompatibleWith(rightUnit)) throw new IncompatibleUnitsException();
        }

        

        public override string ToString()
        {
            return $"{amount} {unitOfMeasurement}";
        }
    }

    public class RatioQuantity : IntervalQuantity
    {
        public RatioQuantity(double amount, Unit unitOfMeasurement) : base(amount, unitOfMeasurement)
        {
        }

        public RatioQuantity In(Unit desiredUnit)
        {
            EnsureSameUnitTypes(unitOfMeasurement, desiredUnit);
            return new RatioQuantity(unitOfMeasurement.Convert(amount, desiredUnit), desiredUnit);
        }

        public static RatioQuantity operator +(RatioQuantity left, RatioQuantity right)
        {
            EnsureSameUnitTypes(left.unitOfMeasurement, right.unitOfMeasurement);
            return new RatioQuantity(left.amount + right.In(left.unitOfMeasurement).amount, left.unitOfMeasurement);
        }

        public static RatioQuantity operator -(RatioQuantity left, RatioQuantity right)
        {
            return left + (right * -1);
        }

        public static RatioQuantity operator -(RatioQuantity original)
        {
            return original * -1;
        }

        public static RatioQuantity operator *(RatioQuantity left, double factor)
        {
            return new RatioQuantity(left.amount * factor, left.unitOfMeasurement);
        }

        public static RatioQuantity operator /(RatioQuantity left, double factor)
        {
            return new RatioQuantity(left.amount / factor, left.unitOfMeasurement);
        }
    }

    public class IncompatibleUnitsException : Exception
    {
        
    }
}