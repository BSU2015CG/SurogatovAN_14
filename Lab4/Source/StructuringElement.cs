using System.Drawing;

namespace CG_Lab4
{
    public interface StructuringElement
    {
        void SetCenter(Point position);
        Point[] GetStructuringPoints();
    }
}
