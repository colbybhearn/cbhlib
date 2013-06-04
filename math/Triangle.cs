using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CbhLib.math
{
    public class Triangle
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        public static void GetAnglesFromSides(double a, double b, double c, out double A, out double B, out double C)
        {
            A = GetAngleFromSides(a, b, c);
            B = GetAngleFromSides(b, a, c);
            C = GetAngleFromSides(c, a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <param name="s3"></param>
        /// <returns>Angle opposite of side s3 in Radians</returns>
        public static double GetAngleFromSides(double s1, double s2, double s3)
        {
            double cosS1 = ((s2 * s2) + (s3 * s3) - (s1 * s1)) / (2 * s2 * s3);
            return System.Math.Acos(cosS1);
        }
    }


}
