using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;

namespace CG_Lab3
{
    public abstract class LineDrawingAlg
    {
        public abstract void DrawLine(Canvas g, SPoint start, SPoint finish);

        public static System.Windows.Shapes.Rectangle CreateRectangle(Color color)
        {
            System.Windows.Shapes.Rectangle rect;
            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(color);
            rect.Width = Form1.gridStep;
            rect.Height = Form1.gridStep;

            return rect;
        }

        public static Line CreateDefaultLine(int x1, int y1, int x2, int y2, Brush color)
        {
            Line line = new Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = color;
            return line;
        }

        public struct SPoint
        {
            private int x;
            private int y;

            public int X
            {
                get { return x; }
                set { x = (value - value % Form1.gridStep) / Form1.gridStep; }
            }

            public int Y
            {
                get { return y; }
                set { y = (value - value % Form1.gridStep) / Form1.gridStep; }
            }

            public SPoint(int x, int y)
            {
                this.x = (x - x % Form1.gridStep) / Form1.gridStep;
                this.y = (y - y % Form1.gridStep) / Form1.gridStep;
            }
        }
    }
}
