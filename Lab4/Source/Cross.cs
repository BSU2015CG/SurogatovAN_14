using System.Drawing;

namespace CG_Lab4
{
    class Cross : StructuringElement
    {
        private int size;
        private Point center;

        public Cross(int size)
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
            Point[] ans = new Point[size * 4 + 1];
            int count = 0;
            for (int i = center.X - size; i <= center.X + size; ++i)
                ans[count++] = new Point(i, center.Y);
            for (int i = center.Y - size; i <= center.Y + size; ++i)
                if (i != center.Y)
                    ans[count++] = new Point(center.X, i);
            return ans;
        }
    }
}
