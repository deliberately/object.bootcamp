namespace OO.Bootcamp
{
    // Understands the relationship between an amount and units of measurement
    public class Quantity
    {
        private readonly int value;
        private readonly ImperialMeasure unitOfMeasurement;

        public Quantity(int value, ImperialMeasure unitOfMeasurement)
        {
            this.value = value;
            this.unitOfMeasurement = unitOfMeasurement;
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as Quantity);
        }

        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(this, null)) { return false; }

            return this.Resolve() == other.Resolve();
        }

        private int Resolve()
        {
            return unitOfMeasurement.InBaseUnits(value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() ^ unitOfMeasurement.GetHashCode();
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

        public Quantity Amount(int value)
        {
            return new Quantity(value, this);
        }

        public int InBaseUnits(int value)
        {
            if (unit == null) return value*factor;
            return unit.InBaseUnits(value*factor);
        }
    }
}