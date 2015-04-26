using System;
using System.Drawing;

namespace CG_Lab4
{
    public abstract class ImageFilter
    {
        public abstract Bitmap Process(Bitmap bmp);

        public byte[,] GetMatrix(Bitmap bmp)
        {
            Color c;
            byte[,] matrix = new byte[bmp.Width, bmp.Height];
            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    c = bmp.GetPixel(i, j);
                    matrix[i, j] = (byte)(.299*c.R + .587*c.G + .114*c.B);
                }
            return matrix;
        }

        public Bitmap GetBitmapFromMatrix(byte[,] matrix)
        {
            Bitmap ans = new Bitmap(matrix.GetLength(0), matrix.GetLength(1));
            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    ans.SetPixel(i, j, Color.FromArgb(matrix[i, j], matrix[i, j], matrix[i, j]));
            return ans;
        }

        public Point[] GetNearlyPoints(Point center, int size = 1)
        {
            Point[] ans = new Point[(size * 2 + 1) * (size * 2 + 1) - 1];
            int count = 0;
            for (int i = center.X - size; i <= center.X + size; ++i)
                for (int j = center.Y - size; j <= center.Y + size; ++j)
                    if  (i != center.X && j != center.Y)
                        ans[count++] = new Point(i, j);
            return ans;
        }
    }

    public class Median : ImageFilter
    {
        public override Bitmap Process(Bitmap bmp)
        {
            var matrix = GetMatrix(bmp);
            byte[,] medianMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    var points = GetNearlyPoints(new Point(i, j));
                    medianMatrix[i, j] = matrix[i, j];

                    int sum = 0;
                    bool isValidPoint = true;
                    foreach (var pt in points)
                        if (!Morphology.IsValidPosition(matrix.GetLength(0), matrix.GetLength(1), pt))
                        {
                            isValidPoint = false;
                            break;
                        }
                        else
                            sum += matrix[pt.X, pt.Y];

                    if (isValidPoint)
                        medianMatrix[i, j] = (byte) (sum / 8);
                }
            return GetBitmapFromMatrix(medianMatrix);
        }
    }

    public class Min : ImageFilter
    {
        public override Bitmap Process(Bitmap bmp)
        {
            var matrix = GetMatrix(bmp);
            byte[,] medianMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    var points = GetNearlyPoints(new Point(i, j));
                    medianMatrix[i, j] = matrix[i, j];

                    byte min = Byte.MaxValue;
                    bool isValidPoint = true;
                    foreach (var pt in points)
                        if (!Morphology.IsValidPosition(matrix.GetLength(0), matrix.GetLength(1), pt))
                        {
                            isValidPoint = false;
                            break;
                        }
                        else
                            if (matrix[pt.X, pt.Y] < min)
                                min = matrix[pt.X, pt.Y];

                    if (isValidPoint)
                        medianMatrix[i, j] = min;
                }
            return GetBitmapFromMatrix(medianMatrix);
        }
    }

    public class Max : ImageFilter
    {
        public override Bitmap Process(Bitmap bmp)
        {
            var matrix = GetMatrix(bmp);
            byte[,] medianMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    var points = GetNearlyPoints(new Point(i, j));
                    medianMatrix[i, j] = matrix[i, j];

                    byte max = Byte.MinValue;
                    bool isValidPoint = true;
                    foreach (var pt in points)
                        if (!Morphology.IsValidPosition(matrix.GetLength(0), matrix.GetLength(1), pt))
                        {
                            isValidPoint = false;
                            break;
                        }
                        else
                            if (matrix[pt.X, pt.Y] > max)
                                max = matrix[pt.X, pt.Y];

                    if (isValidPoint)
                        medianMatrix[i, j] = max;
                }
            return GetBitmapFromMatrix(medianMatrix);
        }
    }
}
