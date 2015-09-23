namespace OO.Bootcamp
{
    public class Unit
    {
        private static readonly object Volume = new object();
        public static readonly Unit Teaspoon = new Unit(1, Teaspoon, Volume);
        public static readonly Unit Tablespoon = new Unit(3, Teaspoon, Volume);
        public static readonly Unit Ounce = new Unit(2, Tablespoon, Volume);
        public static readonly Unit Cup = new Unit(8, Ounce, Volume);
        public static readonly Unit Pint = new Unit(2, Cup, Volume);
        public static readonly Unit Quart = new Unit(2, Pint, Volume);
        public static readonly Unit Gallon = new Unit(4, Quart, Volume);

        private static readonly object Distance = new object();
        public static readonly Unit Inch = new Unit(1, Inch, Distance);
        public static readonly Unit Feet = new Unit(12, Inch, Distance);
        public static readonly Unit Yard = new Unit(3, Feet, Distance);
        public static readonly Unit Mile = new Unit(1760, Yard, Distance);

        private static readonly object Temperature = new object();
        public static readonly Unit Celsius = new Unit(1, Celsius, Temperature);
        public static readonly Unit Fahrenheit = new Unit(5.0/9.0, 32, Celsius, Temperature);

        protected readonly double factor;
        private readonly int offset;
        protected readonly Unit unit;
        private readonly object unitType;

        protected Unit(int factor, Unit unit, object unitType) : this(factor, 0, unit, unitType){}

        private Unit(double factor, int offset, Unit unit, object unitType)
        {
            this.factor = factor;
            this.offset = offset;
            this.unit = unit;
            this.unitType = unitType;
        }

        public Quantity Amount(double value)
        {
            return new Quantity(value, this);
        }

        private double InBaseUnits(double value)
        {
            return unit?.InBaseUnits(value * factor) ?? value * factor;
        }

        public double Convert(double value, Unit desiredUnit)
        {
            return InBaseUnits(value - offset) / desiredUnit.InBaseUnits(1) + desiredUnit.offset;
        }

        public bool IsDifferentUnitType(Unit other)
        {
            return !this.unitType.Equals(other.unitType);
        }
    }

}