using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace CG_Lab5
{
    /// <summary>
    /// Interaction logic for CG_Lab5.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int gridStep = 32;
        public static int gridSize = 5000;

        private float zmLevel = 1.1f;
        private double maxZoom = 2;
        private double curZoom = 1;

        private double prevTransformX = 0;
        private double prevTransformY = 0;

        TransformGroup group = new TransformGroup();

        private bool isDragging;
        private Point clickPosition;

        private int linesCount = 0;
        private Segment[] lines;
        private Segment rectangle;

        private LineClippingAlgorithm lca;

        public static int x0 = ((gridSize / 2) / gridStep + 1) * gridStep;
        public static int y0 = ((gridSize / 2) / gridStep + 1) * gridStep;

        public MainWindow()
        {
            InitializeComponent();

            canvas.Width = MainGrid.Width;
            canvas.Height = MainGrid.Height;

            canvas.MouseDown += coordinateSystem_MouseDown;
            canvas.MouseUp += coordinateSystem_MouseUp;
            canvas.MouseLeave += coordinateSystem_MouseLeave;
            canvas.MouseMove += Control_MouseMove;
            canvas.MouseWheel += MouseWheelEvent;

            group.Children.Add(new MatrixTransform());
            group.Children.Add(new TranslateTransform());

            DrawGrid();
            initPosition();
        }

        public static Line CreateDefaultLine(double x1, double y1, double x2, double y2, Brush color)
        {
            Line line = new Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = color;
            return line;
        }

        public static Line CreateLine(double x1, double y1, double x2, double y2, Brush color)
        {
            return CreateDefaultLine(x0 + x1*gridStep, y0 - y1*gridStep, x0 + x2*gridStep, y0 - y2*gridStep, color);
        }
        
        public static Line CreateLine(Segment line, Brush color)
        {
            return CreateLine(line.X1, line.Y1, line.X2, line.Y2, color);
        }

        private void initPosition()
        {
            var transform = group.Children[1] as TranslateTransform;

            prevTransformX -= gridSize / 2;
            prevTransformY -= gridSize / 2;
            transform.X = prevTransformX;
            transform.Y = prevTransformY;

            group.Children[1] = transform;

            canvas.RenderTransform = group;
            clickPosition.X = 0;
            clickPosition.Y = 0;
        }

        private void DrawBackground()
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.LightYellow);
            rect.Fill = new SolidColorBrush(Colors.LightYellow);
            rect.Width = gridSize;
            rect.Height = gridSize;
            canvas.Children.Add(rect);
        }

        private void DrawGrid()
        {
            canvas.Children.Clear();
            DrawBackground();

            bool drawOY = false;
            bool drawOX = false;
            for (int i = 0; i < gridSize; i += gridStep)
            {
                if (!drawOY && i > gridSize / 2)
                {
                    for (int j = 0; j < gridSize; j += gridStep)
                    {
                        int numRow = (gridSize / 2 - j) / gridStep;
                        if ((double)gridSize / 2 - j > 0)
                            ++numRow;
                        TextBlock tb = new TextBlock();
                        tb.Text = numRow.ToString();
                        canvas.Children.Add(tb);
                        tb.Margin = new Thickness(i, j, i + 20, j + 20);
                    }
                    drawOY = true;
                }
                canvas.Children.Add(CreateDefaultLine(i, 0, i, gridSize, Brushes.DarkSlateGray));
            }

            for (int i = 0; i < gridSize; i += gridStep)
            {
                if (!drawOX && i > gridSize / 2)
                {
                    for (int j = 0; j < gridSize; j += gridStep)
                    {
                        int numRow = (gridSize / 2 - j) / gridStep;
                        if ((double)gridSize / 2 - j > 0)
                            ++numRow;
                        numRow *= -1;
                        TextBlock tb = new TextBlock();
                        tb.Text = numRow.ToString();
                        canvas.Children.Add(tb);
                        tb.Margin = new Thickness(j, i, j + 20, i + 20);
                    }
                    drawOX = true;
                }
                canvas.Children.Add(CreateDefaultLine(0, i, gridSize, i, Brushes.DarkSlateGray));
            }

            canvas.Children.Add(CreateDefaultLine(0, ((gridSize / 2) / gridStep + 1) * gridStep,
                                                            gridSize, ((gridSize / 2) / gridStep + 1) * gridStep, Brushes.Black));
            canvas.Children.Add(CreateDefaultLine(((gridSize / 2) / gridStep + 1) * gridStep,
                                                            gridSize, ((gridSize / 2) / gridStep + 1) * gridStep, 0, Brushes.Black));
        }

        private void coordinateSystem_MouseLeave(object sender, MouseEventArgs e)
        {

            isDragging = false;
            canvas.ReleaseMouseCapture();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && canvas != null)
            {
                var transform = group.Children[1] as TranslateTransform;

                Point currentPosition = e.GetPosition(canvas.Parent as UIElement);

                prevTransformX += currentPosition.X - clickPosition.X;
                prevTransformY += currentPosition.Y - clickPosition.Y;
                transform.X = prevTransformX;
                transform.Y = prevTransformY;

                group.Children[1] = transform;

                canvas.RenderTransform = group;
                clickPosition.X = currentPosition.X;
                clickPosition.Y = currentPosition.Y;
            }
        }


        private void MouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            isDragging = false;

            var position = e.GetPosition(canvas);

            var matrix = (group.Children[0] as MatrixTransform).Matrix;
            var scale = e.Delta >= 0 ? zmLevel : (1.0 / zmLevel);

            if (curZoom * scale > maxZoom)
                scale = 1;
            if (curZoom * scale < 1 / maxZoom)
                scale = 1;

            curZoom *= scale;

            matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);

            group.Children[0] = new MatrixTransform(matrix);
            canvas.RenderTransform = group;
        }

        private void coordinateSystem_MouseDown(object sender, MouseButtonEventArgs mEventArgs)
        {
            if (mEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                clickPosition = mEventArgs.GetPosition(canvas.Parent as UIElement);
                canvas.CaptureMouse();
            }
        }

        private void coordinateSystem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            canvas.ReleaseMouseCapture();
        }

        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                DrawGrid();

                StreamReader sr = new StreamReader(ofd.FileName);
                linesCount = Int32.Parse(sr.ReadLine());
                lines = new Segment[linesCount];
                string[] line;

                for (int i = 0; i < linesCount; ++i)
                {
                    line = sr.ReadLine().Split(' ');
                    lines[i] = new Segment(Int32.Parse(line[0]), Int32.Parse(line[1]), Int32.Parse(line[2]), Int32.Parse(line[3]));
                    canvas.Children.Add(CreateLine(lines[i], Brushes.Blue));
                }

                line = sr.ReadLine().Split(' ');
                rectangle = new Segment(Int32.Parse(line[0]), Int32.Parse(line[1]), Int32.Parse(line[2]), Int32.Parse(line[3]));

                DrawRectangle(rectangle);

                cutBtn.IsEnabled = true;
            }
        }

        public void DrawRectangle(Segment segment)
        {
            Rectangle r;
            r = new Rectangle();
            r.Stroke = new SolidColorBrush(Colors.Red);
            r.StrokeThickness = 5;

            r.Width = Math.Abs(rectangle.X2 - rectangle.X1) * gridStep;
            r.Height = Math.Abs(rectangle.Y2 - rectangle.Y1) * gridStep;

            Canvas.SetLeft(r, x0 + rectangle.X1 * gridStep);
            Canvas.SetTop(r, y0 - rectangle.Y2 * gridStep);
            canvas.Children.Add(r);
        }

        private void cutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                lca = new MidPointAlgorithm();
            if (comboBox1.SelectedIndex == 1)
                lca = new CohenSutherlandAlgorithm();

            if (lca != null)
            {
                var newLines = lca.CutSegments(lines, rectangle);

                DrawGrid();
                DrawRectangle(rectangle);

                foreach (var line in newLines)
                {
                    canvas.Children.Add(CreateLine(line, Brushes.Blue));
                }
            }
        }
    }
}
