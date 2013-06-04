using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CbhLib.math
{
    public class _2d
    {
        public static double Distance(PointF a, PointF b)
        {
            double dxsquared = ((a.X - b.X) * (a.X - b.X));
            double dysquared = ((a.Y - b.Y) * (a.Y - b.Y));
            double d = Math.Sqrt(dxsquared + dysquared);
            return d;
        }

        public static double Distance(PointF a, Point b)
        {
            return Distance(a, new PointF(b.X, b.Y));
        }

        public static double Rotation(PointF a, PointF b)
        {

            double dx = b.X - a.X;
            double dy = b.Y - a.Y;
            double rads = Math.Atan(dy / dx);

            if (dy > 0)
            {
                if (dx >= 0)
                {

                }
                else
                {
                    rads += Math.PI;
                }
            }
            else
            {
                if (dx >= 0)
                {
                    rads += Math.PI * 2;
                }
                else
                {
                    rads += Math.PI;
                }
            }

            return rads;
        }

        public static float ToFloatDegrees(double r)
        {
            return (float)(r * 180 / Math.PI);
        }
        public static float ToFloatRadians(double d)
        {
            return (float)(d * Math.PI / 180);
        }

        public static double ToDegrees(double r)
        {
            return r * 180 / Math.PI;
        }
        public static double ToRadians(double d)
        {
            return d * Math.PI / 180;
        }

        /// <summary>
        /// Calculates factors for two intersecting lines or segments
        /// http://paulbourke.net/geometry/lineline2d/
        /// http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/
        /// </summary>
        /// <param name="aStart">start of line a (measurement start)</param>
        /// <param name="aEnd">end of line a</param>
        /// <param name="bStart">start of line b (measurement start)</param>
        /// <param name="bEnd">end of line b</param>
        /// <returns></returns>
        public static PointF GetIntersectionFactors(PointF aStart, PointF aEnd, PointF bStart, PointF bEnd)
        {
            double D = ((aEnd.Y - aStart.Y) * (bStart.X - bEnd.X)) - ((aEnd.X - aStart.X) * (bStart.Y - bEnd.Y));
            // Make sure there is not a division by zero - this also indicates that
            // the lines are parallel.  
            // If n_a and n_b were both equal to zero the lines would be on top of each 
            // other (coincidental).  This check is not done because it is not 
            // necessary for this implementation (the parallel check accounts for this).
            if (D == 0)
                return new PointF(0, 0);
            double Ua = (((aEnd.X - aStart.X) * (bEnd.Y - aStart.Y)) - ((aEnd.Y - aStart.Y) * (bEnd.X - aStart.X))) / D;
            double Ub = (((bStart.X - bEnd.X) * (bEnd.Y - aStart.Y)) - ((bStart.Y - bEnd.Y) * (bEnd.X - aStart.X))) / D;
            return new PointF((float)Ua, (float)Ub);
        }

        /// <summary>
        /// This is based off an explanation and expanded math presented by Paul Bourke:
        /// 
        /// It takes two lines as inputs and returns true if they intersect, false if they 
        /// don't.
        /// If they do, ptIntersection returns the point where the two lines intersect.  
        /// </summary>
        /// <param name="L1">The first line</param>
        /// <param name="L2">The second line</param>
        /// <param name="ptIntersection">The point where both lines intersect (if they do).</param>
        /// <returns></returns>
        /// <remarks>See http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/</remarks>
        public static bool DoLinesIntersect(PointF aStart, PointF aEnd, PointF bStart, PointF bEnd, ref PointF ptIntersection)
        {
            // Calculate the intermediate fractional point at which the lines potentially intersect.
            PointF Factors = GetIntersectionFactors(aStart, aEnd, bStart, bEnd);
            double Ua = Factors.X;
            double Ub = Factors.Y;

            // The fractional point will be between 0 and 1 inclusive if the lines
            // intersect.  If the fractional calculation is larger than 1 or smaller
            // than 0 the lines would need to be longer to intersect.
            if (Ua >= 0d &&
                Ua <= 1d &&
                Ub >= 0d &&
                Ub <= 1d)
            {
                ptIntersection.X = (float)(bEnd.X + (Ua * (bStart.X - bEnd.X)));
                ptIntersection.Y = (float)(bEnd.Y + (Ua * (bStart.Y - bEnd.Y)));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates interpolation factors from the beginning of each of two intersecting lines or segments
        /// </summary>
        /// <param name="aStart">start of line a (measurement start)</param>
        /// <param name="aEnd">end of line a</param>
        /// <param name="bStart">start of line b (measurement start)</param>
        /// <param name="bEnd">end of line b</param>
        /// <param name="factorA"></param>
        /// <param name="factorB"></param>
        public static void CalculatePivotDistFactors(PointF aStart, PointF aEnd, PointF bStart, PointF bEnd, out double factorA, out double factorB)
        {
            PointF Intersection = new PointF();
            bool intersect = DoLinesIntersect(aStart, aEnd, bStart, bEnd, ref Intersection);

            if (!intersect)
            {
                factorA = 0;
                factorB = 0;
                return;
            }

            double pivotLengthA = Distance(Intersection, aStart);
            double pivotLengthB = Distance(Intersection, bStart);
            double totalLengthA = Distance(aStart, aEnd);
            double totalLengthB = Distance(bStart, bEnd);

            factorA = pivotLengthA / totalLengthA;
            factorB = pivotLengthB / totalLengthB;
        }

        /// <summary>
        /// projects a point from 2D world coordinates to 2D screen coordinates
        /// </summary>
        /// <param name="p">World Coordinates</param>
        /// <param name="zoom"></param>
        /// <param name="offset"></param>
        /// <returns>Screen Coordinates</returns>
        public static PointF Project(PointF p, double zoom, PointF offset)
        {
            return new PointF(p.X * (float)zoom + offset.X,
                                p.Y * (float)zoom + offset.Y);

        }
        
        /// <summary>
        /// Unprojects a point from 2D screen coordinates to 2D world coordinates
        /// </summary>
        /// <param name="p">Screen coordinates</param>
        /// <param name="zoom"></param>
        /// <param name="offset"></param>
        /// <returns>World coordinates</returns>
        public static PointF Unproject(PointF p, double zoom, PointF offset)
        {
            return new PointF((float)((p.X - offset.X) / zoom),
                                (float)((p.Y - offset.Y) / zoom));
        }

        /// <summary>
        /// Calculates interpolation factor for a point along a line. 
        /// Assumes the three points are co-linear.
        /// </summary>
        /// <param name="a">start of the line (measurement start)</param>
        /// <param name="b">point for which a factor is needed (measurement end)</param>
        /// <param name="c">end of the line</param>
        /// <returns>the length of B-A in terms of the length of C-A</returns>
        public static double GetInterpolationFactor(PointF a, PointF b, PointF c)
        {
            double totalLength = Distance(c, a);
            double subLength = Distance(b, a);
            return subLength / totalLength;
        }

        public static PointF GetNearestPointOnSegment(PointF a, PointF b, PointF p)
        {
            PointF ap = new PointF(p.X - a.X, p.Y - a.Y);
            PointF ab = new PointF(b.X - a.X, b.Y - a.Y);

            double ab2 = ab.X * ab.X + ab.Y * ab.Y;
            double ap_ab = ap.X * ab.X + ap.Y * ab.Y;
            double t = ap_ab / ab2;
            if (t < 0)
                t = 0;
            else if (t > 1)
                t = 1;
            PointF near = new PointF((float)(a.X + (ab.X * t)), (float)(a.Y + (ab.Y * t)));
            return near;
        }
    }
}
