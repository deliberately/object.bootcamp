using System;

namespace OO.Bootcamp
{
    public static class Ensure
    {
        public static void NotNull(object obj)
        {
            if(obj == null) {throw new ArgumentException();}
        }
    }
}