using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;

namespace CG_Lab3
{
    class DDA : LineDrawingAlg
    {
        public override void DrawLine(Canvas canvas, SPoint start, SPoint end)
        {
            int deltaX = end.X - start.X;
            int deltaY = end.Y - start.Y;

            int l = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY));

            float dx = (float)deltaX / l;
            float dy = (float)deltaY / l;

            float x = start.X;
            float y = start.Y;

            var rect = CreateRectangle(Colors.Red);
            Canvas.SetLeft(rect, x * Form1.gridStep);
            Canvas.SetTop(rect, y * Form1.gridStep);
            canvas.Children.Add(rect);

            for (int i = 0; i < l; ++i)
            {
                x += dx;
                y += dy;
                int a = x > 0 ? (int)(x + 0.5) : (int)(x - 0.5);
                int b = y > 0 ? (int)(y + 0.5) : (int)(y - 0.5);

                rect = CreateRectangle(Colors.Red);
                Canvas.SetLeft(rect, a * Form1.gridStep);
                Canvas.SetTop(rect, b * Form1.gridStep);
                canvas.Children.Add(rect);
            }
        }
    }
}
