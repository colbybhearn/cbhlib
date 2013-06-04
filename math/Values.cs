using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CbhLib.math
{
    public class Values
    {
        public static double Largest(double a, double b, double c)
        {
            if (a >= b && a >= c)
                return a;
            if (b >= a && b >= c)
                return b;
            return c;
        }
    }
}
