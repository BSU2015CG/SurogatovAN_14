using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;

namespace CG_Lab3
{
    public class Bresenham : LineDrawingAlg
    {
        public override void DrawLine(Canvas canvas, SPoint start, SPoint end)
        {
            int octant;
            int dx = end.X - start.X;
            int dy = end.Y - start.Y;

            if (Math.Abs(dy) > Math.Abs(dx))
            {
                if (dx > 0)
                    octant = dy > 0 ? 1 : 6;
                else
                    octant = dy > 0 ? 2 : 5;
            }
            else
            {
                if (dx > 0)
                    octant = dy > 0 ? 0 : 7;
                else
                    octant = dy > 0 ? 3 : 4;
            }

            Point endT = switchToZeroOctant(octant, new Point(dx, dy));

            if (octant == 2)
                octant = 6;
            else if (octant == 6)
                octant = 2;

            dx = endT.X;
            dy = endT.Y;

            int D = 2 * dy - dx;

            for (int x = 1, y = 0; x < endT.X; x++)
            {
                if (D > 0)
                {
                    y++;
                    D += 2 * dy - 2 * dx;
                }
                else
                    D += 2 * dy;

                Point p = switchToZeroOctant(octant, new Point(x, y));
                int tmpx = p.X + start.X;
                int tmpy = p.Y + start.Y;
                var rect = CreateRectangle(Colors.Green);
                Canvas.SetLeft(rect, tmpx * Form1.gridStep);
                Canvas.SetTop(rect, tmpy * Form1.gridStep);
                canvas.Children.Add(rect);
            }
            var r = CreateRectangle(Colors.Green);
            Canvas.SetLeft(r, start.X * Form1.gridStep);
            Canvas.SetTop(r, start.Y * Form1.gridStep);
            canvas.Children.Add(r);
            r = CreateRectangle(Colors.Green);
            Canvas.SetLeft(r, end.X * Form1.gridStep);
            Canvas.SetTop(r, end.Y * Form1.gridStep);
            canvas.Children.Add(r);
        }


        Point switchToZeroOctant(int octant, Point p)
        {
            switch (octant)
            {
                case 0:
                    return new Point(p.X, p.Y);
                case 1:
                    return new Point(p.Y, p.X);
                case 2:
                    return new Point(p.Y, -p.X);
                case 3:
                    return new Point(-p.X, p.Y);
                case 4:
                    return new Point(-p.X, -p.Y);
                case 5:
                    return new Point(-p.Y, -p.X);
                case 6:
                    return new Point(-p.Y, p.X);
                case 7:
                    return new Point(p.X, -p.Y);
                default:
                    return p;
            }
        }
    }
}