using System;

namespace OO.Bootcamp
{
    // Understands the chance of something happening
    public class Chance
    {
        private const double DoubleTolerance = 0.00001;
        private readonly double fraction;
        public static readonly Chance Certain = new Chance(1);

        public Chance(double fraction)
        {
            if(fraction > 1 || fraction < 0) throw new ArgumentException("fraction");
            this.fraction = fraction;
        }

        public Chance Not => Certain - this;

        public Chance And(Chance other)
        {
            return new Chance(this.fraction * other.fraction);
        }

        public static Chance operator -(Chance left, Chance right)
        {
            return new Chance(left.fraction - right.fraction);
        }
                            
        public Chance Or(Chance other)
        {
            /** Implementaion of De Morgan's Law
             ** <see>https://en.wikipedia.org/wiki/De_Morgan%27s_laws</see> 
             **/
            return this.Not.And(other.Not).Not; 
        }

        public override string ToString()
        {
            return fraction.ToString();
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || Equals(other as Chance);
        }

        public bool Equals(Chance other)
        {
            if (ReferenceEquals(other, null)) { return false; }

            return Math.Abs(this.fraction - other.fraction) < DoubleTolerance;
        }

        public override int GetHashCode()
        {
            return fraction.GetHashCode();
        }
    }
}