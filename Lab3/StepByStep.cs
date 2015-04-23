using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace CG_Lab3
{
    public class StepByStep : LineDrawingAlg
    {
        public override void DrawLine(Canvas canvas, SPoint start, SPoint end)
        {
            if (start.X == end.X && start.Y == end.Y)
            {
                var rect = CreateRectangle(Colors.Blue);
                Canvas.SetLeft(rect, start.X * Form1.gridStep);
                Canvas.SetTop(rect, start.Y * Form1.gridStep);
                canvas.Children.Add(rect);
                return;
            }
            
            if (Math.Abs(end.X - start.X) > Math.Abs(end.Y - start.Y))
            {
                if (start.X > end.X)
                {
                    SPoint tmp = start;
                    start = end;
                    end = tmp;
                }

                float k = ((float)(end.Y - start.Y) / (end.X - start.X));
                float y = start.Y;

                for (float x = start.X; x <= end.X; ++x)
                {
                    y = k * x + (float)(end.X * start.Y - start.X * end.Y) / (end.X - start.X);

                    var rect = CreateRectangle(Colors.Blue);
                    Canvas.SetLeft(rect, x * Form1.gridStep);
                    int tmpy = (int) Math.Round(y);
                    Canvas.SetTop(rect, tmpy * Form1.gridStep);
                    canvas.Children.Add(rect);
                }
            }
            else
            {
                if (start.Y > end.Y)
                {
                    SPoint tmp = start;
                    start = end;
                    end = tmp;
                }

                float k = ((float)(start.X - end.X) / (start.Y - end.Y));
                float x = start.X;

                for (float y = start.Y; y <= end.Y; ++y )
                {
                    x = k * y + (float)(end.X * start.Y - start.X * end.Y) / (start.Y - end.Y);

                    var rect = CreateRectangle(Colors.Blue);
                    int tmpx = (int)Math.Round(x);
                    Canvas.SetLeft(rect, tmpx * Form1.gridStep);
                    Canvas.SetTop(rect, y * Form1.gridStep);
                    canvas.Children.Add(rect);
                }
            }
        }
    }
}