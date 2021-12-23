using System.Numerics;

namespace Metaballs
{
    public struct Line
    {
        public Vector2 point0;
        public Vector2 point1;

        public Line(Vector2 point0, Vector2 point1)
        {
            this.point0 = point0;
            this.point1 = point1;
        }
    }
}
