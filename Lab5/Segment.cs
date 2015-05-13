using System;

namespace CG_Lab5
{
    public class Segment
    {
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }

        public Segment() : this(0, 0, 0, 0) { }

        public Segment(double x1, double y1, double x2, double y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Segment(Segment segment) : this(segment.X1, segment.Y1, segment.X2, segment.Y2) { }

        public double GetLength()
        {
            return Math.Sqrt((X1 - X2)*(X1 - X2) + (Y1 - Y2)*(Y1 - Y2));
        }
    }
}
