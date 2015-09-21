using System;

namespace OO.Bootcamp
{
    // Understands the chance of something happening
    public class Chance
    {
        private readonly double fraction;
        public static readonly Chance Certain = new Chance(1);

        public Chance(double fraction)
        {
            this.fraction = fraction;
        }

        public Chance Not => new Chance(Certain.fraction - fraction);

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

        public static Chance operator -(Chance one, Chance other)
        {
            return new Chance(one.fraction - other.fraction);
        }

        public override string ToString()
        {
            return fraction.ToString();
        }
                            
        public Chance Or(Chance other)
        {
            /** Implementaion of De Morgan's Law
             * <see>https://en.wikipedia.org/wiki/De_Morgan%27s_laws</see> 
             **/
            return this.Not.And(other.Not).Not; 
        }
    }
}