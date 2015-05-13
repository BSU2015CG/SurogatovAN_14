
using System.Collections.Generic;

namespace CG_Lab5
{
    public abstract class LineClippingAlgorithm
    {
        public abstract Segment[] CutSegments(Segment[] lines, Segment rectangle);

        public uint GetCode(double x, double y, Segment rectangle)
        {
            uint code = 0;

            if (x < rectangle.X1)
                code += 8;
            if (x > rectangle.X2)
                code += 4;
            if (y < rectangle.Y1)
                code += 2;
            if (y > rectangle.Y2)
                code += 1;

            return code;
        }
    }

    public class MidPointAlgorithm : LineClippingAlgorithm
    {
        double epsilon = 0.001;
        List<Segment> VisibleSegments = new List<Segment>();

        public override Segment[] CutSegments(Segment[] lines, Segment rectangle)
        {
            VisibleSegments.Clear();

            for (int i = 0; i < lines.Length; i++)
                CutLine(lines[i], rectangle);

            return VisibleSegments.ToArray();
        }

        public void CutLine(Segment line, Segment rectangle)
        {
            uint code1, code2;

            code1 = GetCode(line.X1, line.Y1, rectangle);
            code2 = GetCode(line.X2, line.Y2, rectangle);

            if (code1 == 0 && code2 == 0)
            {
                VisibleSegments.Add(new Segment(line));
            }
            else if ((code1 & code2) > 0 || line.GetLength() < epsilon)
                return;
            else
            {
                double centerX, centerY;

                centerX = (line.X1 + line.X2) / 2;
                centerY = (line.Y1 + line.Y2) / 2;

                CutLine(new Segment(line.X1, line.Y1, centerX, centerY), rectangle);
                CutLine(new Segment(centerX, centerY, line.X2, line.Y2), rectangle);
            }
        }
    }

    public class CohenSutherlandAlgorithm : LineClippingAlgorithm
    {
        public override Segment[] CutSegments(Segment[] lines, Segment rectangle)
        {
            List<Segment> result = new List<Segment>();

            for (int i = 0; i < lines.Length; i++)
            {
                Segment cutted = CutSegment(lines[i], rectangle);

                if (cutted != null)
                    result.Add(cutted);
            }

            return result.ToArray();
        }

        public Segment CutSegment(Segment line, Segment rectangle)
        {
            uint code1, code2;

            code1 = GetCode(line.X1, line.Y1, rectangle);
            code2 = GetCode(line.X2, line.Y2, rectangle);

            if (code1 == 0 && code2 == 0)
                return new Segment(line);
            if ((code1 & code2) > 0)
                return null;
            Segment newSegment = GetIntersection(line, rectangle);
            return CutSegment(newSegment, rectangle);
        }

        private Segment GetIntersection(Segment line, Segment rectangle)
        {
            uint code1, code2, code;
            double outX, outY, inX, inY, k;

            code1 = GetCode(line.X1, line.Y1, rectangle);
            code2 = GetCode(line.X2, line.Y2, rectangle);

            if (code1 != 0)
            {
                code = code1;
                outX = line.X1;
                outY = line.Y1;
                inX = line.X2;
                inY = line.Y2;
            }
            else
            {
                code = code2;
                outX = line.X2;
                outY = line.Y2;
                inX = line.X1;
                inY = line.Y1;
            }

            if (outX - inX != 0)
                k = (outY - inY) / (outX - inX);
            else
                k = 0;

            if (code == 9 || code == 1)
            {
                if (k != 0)
                    outX = outX + (1 / k) * (rectangle.Y2 - outY);
                outY = rectangle.Y2;
            }
            else if (code == 5 || code == 4)
            {
                outY = outY + k * (rectangle.X2 - outX);
                outX = rectangle.X2;
            }
            else if (code == 6 || code == 2)
            {
                if (k != 0)
                    outX = outX + (1 / k) * (rectangle.Y1 - outY);
                outY = rectangle.Y1;
            }
            else if (code == 10 || code == 8)
            {
                outY = outY + k * (rectangle.X1 - outX);
                outX = rectangle.X1;
            }

            return new Segment(outX, outY, inX, inY);
        }
    }
}
