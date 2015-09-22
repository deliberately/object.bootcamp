namespace OO.Bootcamp
{
    public class Unit
    {
        private static readonly object ImperialMeasure = new object();

        public static readonly Unit Teaspoon = new Unit(1, Teaspoon, ImperialMeasure);
        public static readonly Unit Tablespoon = new Unit(3, Teaspoon, ImperialMeasure);
        public static readonly Unit Ounce = new Unit(2, Tablespoon, ImperialMeasure);
        public static readonly Unit Cup = new Unit(8, Ounce, ImperialMeasure);
        public static readonly Unit Pint = new Unit(2, Cup, ImperialMeasure);
        public static readonly Unit Quart = new Unit(2, Pint, ImperialMeasure);
        public static readonly Unit Gallon = new Unit(4, Quart, ImperialMeasure);

        private static readonly object Distance = new object();
        public static readonly Unit Inch = new Unit(1, Inch, Distance);
        public static readonly Unit Feet = new Unit(12, Inch, Distance);
        public static readonly Unit Yard = new Unit(3, Feet, Distance);
        public static readonly Unit Mile = new Unit(1760, Yard, Distance);


        protected readonly int factor;
        protected readonly Unit unit;
        private readonly object unitType;

        protected Unit(int factor, Unit unit, object unitType)
        {
            this.factor = factor;
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
            return InBaseUnits(value) / desiredUnit.InBaseUnits(1);
        }

        public bool IsDifferentUnitType(Unit other)
        {
            return !this.unitType.Equals(other.unitType);
        }
    }

}