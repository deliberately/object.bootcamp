using System;

namespace OO.Bootcamp
{
    // Understands the chance of something happening
    public class Chance
    {
        private readonly double fraction;
        public static readonly Chance Always = new Chance(1);

        public Chance(double fraction)
        {
            this.fraction = fraction;
        }

        public Chance Not => new Chance(Always.fraction - fraction);

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as Chance);
        }

        public bool Equals(Chance other)
        {
            if (ReferenceEquals(other, null)) { return false; }

            return Math.Abs(this.fraction - other.fraction) < 0.00001;
        }

        public override int GetHashCode()
        {
            return fraction.GetHashCode();
        }

        public Chance And(Chance other)
        {
            return new Chance(this.fraction * other.fraction);
        }

        public override string ToString()
        {
            return fraction.ToString();
        }
    }
}