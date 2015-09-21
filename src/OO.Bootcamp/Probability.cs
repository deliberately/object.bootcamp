using System;

namespace OO.Bootcamp
{
    // Understands the chance of something happening
    public class Probability
    {
        private readonly double fraction;
        public static readonly Probability Always = new Probability(1);

        public Probability(double fraction)
        {
            this.fraction = fraction;
        }

        public Probability Not => new Probability(Always.fraction - fraction);

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as Probability);
        }

        public bool Equals(Probability other)
        {
            if (ReferenceEquals(other, null)) { return false; }

            return Math.Abs(this.fraction - other.fraction) < 0.00001;
        }

        public override int GetHashCode()
        {
            return fraction.GetHashCode();
        }

        public Probability And(Probability other)
        {
            return new Probability(this.fraction * other.fraction);
        }

        public override string ToString()
        {
            return fraction.ToString();
        }
    }
}