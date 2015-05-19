using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using CG_Lab6.Graphics_Lab6;
using Microsoft.Win32;

namespace CG_Lab6
{
    public partial class MainWindow : Window
    {
        private enum Projection { OXY, OXZ, OYZ };

        public static int gridStep = 32;
        public static int gridSize = 5000;

        private float zmLevel = 1.1f;
        private double maxZoom = 2;
        private double curZoom = 1;

        private double prevTransformX = 0;
        private double prevTransformY = 0;

        public static int x0 = ((gridSize / 2) / gridStep + 1) * gridStep;
        public static int y0 = ((gridSize / 2) / gridStep + 1) * gridStep;

        TransformGroup group = new TransformGroup();

        private bool isDragging;
        private Point clickPosition;

        private int pointsCount;
        private Segment[] originalSegments;
        private Segment[] transformMatrix;
        private double[,] resultMatrix;

        private double[,] MatrixProjectionOxy = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 1 } };
        private double[,] MatrixProjectionOxz = { { 1, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
        private double[,] MatrixProjectionOyz = { { 0, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
        private double[,] currentMatrixProjection;

        private Projection currentProjection = Projection.OXY;


        private CheckBox oldCB;

        public MainWindow()
        {
            InitializeComponent();

            oldCB = cbOXY;
            cbOXY.IsEnabled = false;
            currentMatrixProjection = MatrixProjectionOxy;

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
            return CreateDefaultLine(x0 + x1 * gridStep, y0 - y1 * gridStep, x0 + x2 * gridStep, y0 - y2 * gridStep, color);
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

        private void openFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                DrawGrid();

                StreamReader sr = new StreamReader(ofd.FileName);
                pointsCount = Int32.Parse(sr.ReadLine());
                originalSegments = new Segment[pointsCount];

                double x1, y1, z1, x2, y2, z2;

                for (int i = 0; i < pointsCount; ++i)
                {
                    String[] elements = sr.ReadLine().Split(new char[] { ' ' });

                    x1 = Double.Parse(elements[0]);
                    y1 = Double.Parse(elements[1]);
                    z1 = Double.Parse(elements[2]);
                    x2 = Double.Parse(elements[3]);
                    y2 = Double.Parse(elements[4]);
                    z2 = Double.Parse(elements[5]);

                    originalSegments[i] = new Segment(x1, y1, z1, x2, y2, z2);
                }

                DrawSegments(originalSegments, currentProjection);
            }
        }

        private void DrawSegments(Segment[] segments, Projection projection)
        {
            DrawGrid();
            if (segments == null || segments.Length == 0)
                return;

            for (int i = 0; i < segments.Length; i++)
            {
                if (projection == Projection.OYZ)
                {
                    canvas.Children.Add(CreateLine(segments[i].PointA.Z, segments[i].PointA.Y,
                                               segments[i].PointB.Z, segments[i].PointB.Y, Brushes.Blue));
                }
                else if (projection == Projection.OXZ)
                {
                    canvas.Children.Add(CreateLine(segments[i].PointA.X, segments[i].PointA.Z,
                                               segments[i].PointB.X, segments[i].PointB.Z, Brushes.Blue));
                }
                else if (projection == Projection.OXY)
                {
                   canvas.Children.Add(CreateLine(segments[i].PointA.X, segments[i].PointA.Y, 
                                                  segments[i].PointB.X, segments[i].PointB.Y, Brushes.Blue));
                }
            }
        }


        private void ProcessObject()
        {
            resultMatrix = GetResultMatrix();
            transformMatrix = ProcessSegments(originalSegments, resultMatrix);

            DrawSegments(ProcessSegments(transformMatrix, currentMatrixProjection), currentProjection);
        }

        private Segment[] ProcessSegments(Segment[] segments, double[,] matrix)
        {
            if (segments == null || segments.Length == 0)
                return null;

            Segment[] result = new Segment[segments.Length];

            for (int i = 0; i < segments.Length; i++)
            {
                double[,] newPointA = MatrixMultiply(matrix, segments[i].PointA.GetVector4D());
                double[,] newPointB = MatrixMultiply(matrix, segments[i].PointB.GetVector4D());
                result[i] = new Segment(newPointA[0, 0], newPointA[1, 0], newPointA[2, 0], newPointB[0, 0], newPointB[1, 0], newPointB[2, 0]);
            }

            return result;
        }

        double[,] GetResultMatrix()
        {
            return MatrixMultiply(GetScalingMatrix(), MatrixMultiply(GetShiftingMatrix(), GetRotationMatrix()));
        }

        double[,] MatrixMultiply(double[,] matrixA, double[,] matrixB)
        {
            if (matrixA.GetLength(1) != matrixB.GetLength(0))
                return null;

            int rows = matrixA.GetLength(0);
            int columns = matrixB.GetLength(1);
            int middle = matrixA.GetLength(1);
            double[,] result = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double element = 0;
                    for (int k = 0; k < middle; k++)
                        element += matrixA[i, k] * matrixB[k, j];
                    result[i, j] = element;
                }
            }

            return result;
        }

        double[,] GetScalingMatrix()
        {
            double[,] result = new double[4, 4];

            if (xScaleSlider == null || yScaleSlider == null || zScaleSlider == null)
                return result;
            double a = xScaleSlider.Value / 10.0;
            double b = yScaleSlider.Value / 10.0;
            double c = zScaleSlider.Value / 10.0;

            result[0, 0] = a;
            result[1, 1] = b;
            result[2, 2] = c;
            result[3, 3] = 1;

            return result;
        }

        double[,] GetRotationMatrix()
        {
            double[,] rotationX = new double[4, 4];
            double[,] rotationY = new double[4, 4];
            double[,] rotationZ = new double[4, 4];

            if (xTranslationSlider == null || yTranslationSlider == null || zTranslationSlider == null)
                return rotationX;

            double angleX = (2 * Math.PI * xRotationSlider.Value) / 360.0;
            double angleY = (2 * Math.PI * yRotationSlider.Value) / 360.0;
            double angleZ = (2 * Math.PI * zRotationSlider.Value) / 360.0;

            rotationX[0, 0] = 1;
            rotationX[1, 1] = rotationX[2, 2] = Math.Cos(angleX);
            rotationX[1, 2] = -Math.Sin(angleX);
            rotationX[2, 1] = -rotationX[1, 2];

            rotationY[1, 1] = 1;
            rotationY[0, 0] = rotationY[2, 2] = Math.Cos(angleY);
            rotationY[2, 0] = -Math.Sin(angleY);
            rotationY[0, 2] = -rotationY[2, 0];

            rotationZ[2, 2] = 1;
            rotationZ[0, 0] = rotationZ[1, 1] = Math.Cos(angleZ);
            rotationZ[0, 1] = -Math.Sin(angleZ);
            rotationZ[1, 0] = -rotationZ[0, 1];

            rotationX[3, 3] = rotationY[3, 3] = rotationZ[3, 3] = 1;

            return MatrixMultiply(rotationX, MatrixMultiply(rotationY, rotationZ));
        }

        double[,] GetShiftingMatrix()
        {
            double[,] result = new double[4, 4];

            if (xTranslationSlider == null || yTranslationSlider == null || zTranslationSlider == null)
                return result;
            double tx = xTranslationSlider.Value / 10.0;
            double ty = yTranslationSlider.Value / 10.0;
            double tz = zTranslationSlider.Value / 10.0;

            result[0, 0] = 1;
            result[1, 1] = 1;
            result[2, 2] = 1;
            result[3, 3] = 1;
            result[0, 3] = tx;
            result[1, 3] = ty;
            result[2, 3] = tz;

            return result;
        }

        private void PrintMatrix(double[,] matrix)
        {
            if (matrix == null || matrixTB == null)
                return;

            String strMatrix = "";

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    strMatrix += String.Format("{0:0.##} ", matrix[i, j]).PadRight(5);
                }
                strMatrix += Environment.NewLine + Environment.NewLine;
            }

            matrixTB.Text = strMatrix;
        }


        private void cb_Checked(object sender, RoutedEventArgs e)
        {
            oldCB.IsChecked = false;
            cbOXY.IsEnabled = true;
            cbOXZ.IsEnabled = true;
            cbOYZ.IsEnabled = true;

            if ((bool)cbOXY.IsChecked)
            {
                currentProjection = Projection.OXY;
                currentMatrixProjection = MatrixProjectionOxy;
            }
            if ((bool)cbOXZ.IsChecked)
            {
                currentProjection = Projection.OXZ;
                currentMatrixProjection = MatrixProjectionOxz;
            }
            if ((bool)cbOYZ.IsChecked)
            {
                currentProjection = Projection.OYZ;
                currentMatrixProjection = MatrixProjectionOyz;
            }
            oldCB = sender as CheckBox;
            oldCB.IsEnabled = false;

            ProcessObject();
        }

        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProcessObject();
            PrintMatrix(resultMatrix);
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            xScaleSlider.Value = 10;
            yScaleSlider.Value = 10;
            zScaleSlider.Value = 10;

            xRotationSlider.Value = 0;
            yRotationSlider.Value = 0;
            zRotationSlider.Value = 0;

            xTranslationSlider.Value = 0;
            yTranslationSlider.Value = 0;
            zTranslationSlider.Value = 0;

            oldCB.IsEnabled = true;
            oldCB.IsChecked = false;
            oldCB = cbOXY;
            cbOXY.IsEnabled = false;
            cbOXY.IsChecked = true;
            currentMatrixProjection = MatrixProjectionOxy;
            currentProjection = Projection.OXY;

            ProcessObject();
        }
    }
}
