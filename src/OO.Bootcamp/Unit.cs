namespace OO.Bootcamp
{
    public class Unit
    {
        protected readonly double factor;
        private readonly int offset;
        protected readonly Unit unit;
        private readonly object unitType;
        private readonly string abbreviation;

        protected Unit(int factor, Unit unit, object unitType, string abbreviation) : this(factor, 0, unit, unitType, abbreviation){}

        protected Unit(double factor, int offset, Unit unit, object unitType, string abbreviation)
        {
            this.factor = factor;
            this.offset = offset;
            this.unit = unit;
            this.unitType = unitType;
            this.abbreviation = abbreviation;
        }

        private double InBaseUnits(double value)
        {
            return unit?.InBaseUnits(value * factor) ?? value * factor;
        }

        public double Convert(double value, Unit desiredUnit)
        {
            return InBaseUnits(value - offset) / desiredUnit.InBaseUnits(1) + desiredUnit.offset;
        }

        public bool IsIncompatibleWith(Unit other)
        {
            return !this.unitType.Equals(other.unitType);
        }

        public override string ToString()
        {
            return abbreviation;
        }
    }

    public class IntervalUnit : Unit
    {
        private static readonly object Temperature = new object();
        public static readonly IntervalUnit Celsius = new IntervalUnit(1, 0, Celsius, Temperature, "°C");
        public static readonly IntervalUnit Fahrenheit = new IntervalUnit(5.0/9.0, 32, Celsius, Temperature, "°F");

        protected IntervalUnit(double factor, int offset, Unit unit, object unitType, string abbreviation) : base(factor, offset, unit, unitType, abbreviation)
        {
        }

        public IntervalQuantity Amount(double value)
        {
            return new IntervalQuantity(value, this);
        }

    }

    public class RatioUnit : Unit
    {
        protected RatioUnit(int factor, Unit unit, object unitType, string abbreviation) : base(factor, unit, unitType, abbreviation)
        {
        }

        private static readonly object Volume = new object();
        public static readonly RatioUnit Teaspoon = new RatioUnit(1, Teaspoon, Volume, "tsp");
        public static readonly RatioUnit Tablespoon = new RatioUnit(3, Teaspoon, Volume, "tbsp");
        public static readonly RatioUnit Ounce = new RatioUnit(2, Tablespoon, Volume, "oz");
        public static readonly RatioUnit Cup = new RatioUnit(8, Ounce, Volume, "cup");
        public static readonly RatioUnit Pint = new RatioUnit(2, Cup, Volume, "pt");
        public static readonly RatioUnit Quart = new RatioUnit(2, Pint, Volume, "qrt");
        public static readonly RatioUnit Gallon = new RatioUnit(4, Quart, Volume, "gallon");
        private static readonly object Distance = new object();
        public static readonly RatioUnit Inch = new RatioUnit(1, Inch, Distance, "\"");
        public static readonly RatioUnit Feet = new RatioUnit(12, Inch, Distance, "'");
        public static readonly RatioUnit Yard = new RatioUnit(3, Feet, Distance, "yds");
        public static readonly RatioUnit Mile = new RatioUnit(1760, Yard, Distance, "mile");

        public RatioQuantity Amount(double value)
        {
            return new RatioQuantity(value, this);
        }
    }

}