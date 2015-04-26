using System.Drawing;

namespace CG_Lab4
{
    public abstract class Morphology
    {
        public abstract byte[,] Process(byte[,] matrix, StructuringElement structElement);
        public static bool IsValidPosition(int width, int height, Point position)
        {
            if (position.X >= width || position.X < 0 ||
                position.Y >= height || position.Y < 0)
                return false;
            return true;
        }
    }

    public class Erosion : Morphology
    {
        public override byte[,] Process(byte[,] matrix, StructuringElement structElement)
        {
            byte[,] erosionMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    erosionMatrix[i, j] = matrix[i, j];

                    if (matrix[i, j] == 1)
                    {
                        structElement.SetCenter(new Point(i, j));
                        var points = structElement.GetStructuringPoints();

                        bool isNeedErosion = false;
                        foreach (Point pt in points)
                            if (!IsValidPosition(matrix.GetLength(0), matrix.GetLength(1), pt))
                                break;
                            else if (matrix[pt.X, pt.Y] == 0)
                            {
                                isNeedErosion = true;
                                break;
                            }
                        if (isNeedErosion)
                            erosionMatrix[i, j] = 0;
                    }
                }
            return erosionMatrix;
        }
    }

    public class Dilation : Morphology
    {
        public override byte[,] Process(byte[,] matrix, StructuringElement structElement)
        {
            byte[,] dilationMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    dilationMatrix[i, j] = matrix[i, j];

                    if (matrix[i, j] == 1)
                    {
                        structElement.SetCenter(new Point(i, j));
                        var points = structElement.GetStructuringPoints();

                        bool isValidPoint = true;
                        foreach (Point pt in points)
                            if (!IsValidPosition(matrix.GetLength(0), matrix.GetLength(1), pt))
                            {
                                isValidPoint = false;
                                break;
                            }

                        if (isValidPoint)
                        {
                            foreach (Point pt in points)
                                dilationMatrix[pt.X, pt.Y] = 1;
                        }
                    }
                }
            return dilationMatrix;
        }
    }

    public class Opening : Morphology
    {
        public override byte[,] Process(byte[,] matrix, StructuringElement structElement)
        {
            var erosion = new Erosion();
            var dilation = new Dilation(); ;
            return dilation.Process(erosion.Process(matrix, structElement), structElement);
        }
    }

    public class Closing : Morphology
    {
        public override byte[,] Process(byte[,] matrix, StructuringElement structElement)
        {
            var erosion = new Erosion();
            var dilation = new Dilation(); ;
            return erosion.Process(dilation.Process(matrix, structElement), structElement);
        }
    }
}
