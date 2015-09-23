namespace OO.Bootcamp
{
    public class RatioQuantity : IntervalQuantity
    {
        public RatioQuantity(double amount, Unit unit) : base(amount, unit)
        {
        }

        public RatioQuantity In(Unit desiredUnit)
        {
            EnsureSameUnitTypes(unit, desiredUnit);
            return new RatioQuantity(unit.Convert(amount, desiredUnit), desiredUnit);
        }

        public static RatioQuantity operator +(RatioQuantity left, RatioQuantity right)
        {
            EnsureSameUnitTypes(left.unit, right.unit);
            return new RatioQuantity(left.amount + right.In(left.unit).amount, left.unit);
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
            return new RatioQuantity(left.amount * factor, left.unit);
        }

        public static RatioQuantity operator /(RatioQuantity left, double factor)
        {
            return new RatioQuantity(left.amount / factor, left.unit);
        }
    }
}