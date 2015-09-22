namespace OO.Bootcamp
{
    public class Unit
    {
        protected readonly int factor;
        protected readonly ImperialMeasure unit;

        protected Unit(int factor, ImperialMeasure unit)
        {
            this.factor = factor;
            this.unit = unit;
        }
    }

    public class ImperialMeasure : Unit
    {
        public static readonly ImperialMeasure Teaspoon = new ImperialMeasure(1, Teaspoon);
        public static readonly ImperialMeasure Tablespoon = new ImperialMeasure(3, Teaspoon);
        public static readonly ImperialMeasure Ounce = new ImperialMeasure(2, Tablespoon);
        public static readonly ImperialMeasure Cup = new ImperialMeasure(8, Ounce);
        public static readonly ImperialMeasure Pint = new ImperialMeasure(2, Cup);
        public static readonly ImperialMeasure Quart = new ImperialMeasure(2, Pint);
        public static readonly ImperialMeasure Gallon = new ImperialMeasure(4, Quart);

        private ImperialMeasure(int factor, ImperialMeasure unit) : base(factor, unit)
        {
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