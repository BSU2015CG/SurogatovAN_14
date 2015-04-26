using System.Drawing;

namespace CG_Lab4
{
    class Square : StructuringElement
    {
        private int size;
        private Point center;

        public Square(int size)
        {
            this.size = size;
            center = new Point(0, 0);
        }

        public void SetCenter(Point position)
        {
            center = position;
        }

        public Point[] GetStructuringPoints()
        {
            Point[] ans = new Point[(size * 2 + 1) * (size * 2 + 1)];
            int count = 0;
            for (int i = center.X - size; i <= center.X + size; ++i)
                for (int j = center.Y - size; j <= center.Y + size; ++j)
                    ans[count++] = new Point(i, j);
            return ans;
        }
    }
}
