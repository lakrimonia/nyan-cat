using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Platform : IGameObject
    {
        public Vector2 Velocity { get; }
        public Point Center { get; }
        public int Length { get; }

        public Platform(Point center, int length)
        {
            Center = center;
            Length = length;
        }
    }
}
