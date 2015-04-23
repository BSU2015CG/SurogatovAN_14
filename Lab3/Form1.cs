using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using RadioButton = System.Windows.Forms.RadioButton;


namespace CG_Lab3
{
    public partial class Form1 : Form
    {
        public static int gridStep = 16;
        private int gridSize = 1000;

        private double prevTransformX = 0;
        private double prevTransformY = 0;

        private float zmLevel = 1.1f;
        private double maxZoom = 2;
        private double curZoom = 1;

        private LineDrawingAlg ldg = new DDA();
        private int startX = -1;
        private int startY = -1;

        Canvas canvas = new Canvas();

        Stopwatch stopWatch = new Stopwatch();

        TransformGroup group = new TransformGroup();

        private bool isDragging;
        private Point clickPosition;

        public Form1()
        {
            InitializeComponent();

            canvas.Width = elementHost1.Width;
            canvas.Height = elementHost1.Height;

            canvas.MouseDown += coordinateSystem_MouseDown;
            canvas.MouseUp += coordinateSystem_MouseUp;
            canvas.MouseLeave += coordinateSystem_MouseLeave;
            canvas.MouseMove += Control_MouseMove;
            canvas.MouseWheel += MouseWheelEvent;
            MouseWheel += MouseWheelSetFocusEvent;


            elementHost1.Child = canvas;

            group.Children.Add(new MatrixTransform());
            group.Children.Add(new TranslateTransform());

            DrawGrid();
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
                canvas.Children.Add(LineDrawingAlg.CreateDefaultLine(i, 0, i, gridSize, Brushes.DarkSlateGray));
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
                canvas.Children.Add(LineDrawingAlg.CreateDefaultLine(0, i, gridSize, i, Brushes.DarkSlateGray));
            }

            canvas.Children.Add(LineDrawingAlg.CreateDefaultLine(0, ((gridSize / 2) / gridStep + 1) * gridStep,
                                                            gridSize, ((gridSize / 2) / gridStep + 1) * gridStep, Brushes.Black));
            canvas.Children.Add(LineDrawingAlg.CreateDefaultLine(((gridSize / 2) / gridStep + 1) * gridStep,
                                                            gridSize, ((gridSize / 2) / gridStep + 1) * gridStep, 0, Brushes.Black));
        }

        private void MouseWheelSetFocusEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            elementHost1.Focus();
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

        private void clearBtn_Click(object sender, System.EventArgs e)
        {
            DrawGrid();
        }

        private void coordinateSystem_MouseDown(object sender, MouseButtonEventArgs mEventArgs)
        {
            if (mEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                clickPosition = mEventArgs.GetPosition(canvas.Parent as UIElement);
                canvas.CaptureMouse();
            }
            else
            {
                Point e = mEventArgs.GetPosition(canvas);

                if (startX == -1 && startY == -1)
                {
                    startX = (int)e.X;
                    startY = (int)e.Y;
                }
                else
                {
                    stopWatch.Start();
                    ldg.DrawLine(canvas, new LineDrawingAlg.SPoint(startX, startY), new LineDrawingAlg.SPoint((int)e.X, (int)e.Y));
                    stopWatch.Stop();
                    tbTime.Text = stopWatch.ElapsedMilliseconds.ToString();

                    startX = -1;
                    startY = -1;
                }
            }
        }

        private void coordinateSystem_MouseUp(object sender, MouseButtonEventArgs e)
        {

            isDragging = false;
            canvas.ReleaseMouseCapture();
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

        private void rb_CheckedChanged(object sender, System.EventArgs e)
        {
            if (sender as RadioButton == rbDDA && rbDDA.Checked)
                ldg = new DDA();
            if (sender as RadioButton == rbBresenham && rbBresenham.Checked)
                ldg = new Bresenham();
            if (sender as RadioButton == rbStepByStep && rbStepByStep.Checked)
                ldg = new StepByStep();
        }
    }
}
