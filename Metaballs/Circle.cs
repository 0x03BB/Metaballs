using System;
using System.Numerics;
using static Metaballs.Shared;

namespace Metaballs
{
    public struct Circle
    {
        private static readonly Random random = new Random();

        public Vector2 CenterPoint;
        public readonly float Radius;
        public readonly float RadiusSquared;
        public Vector2 Velocity;

        public Circle(float x, float y, float radius, float speed, float angle)
        {
            CenterPoint = new Vector2(x, y);
            Radius = radius;
            RadiusSquared = radius * radius;
            Velocity = new Vector2(speed * (float)Math.Cos(angle), speed * (float)Math.Sin(angle));
        }

        public static Circle GenerateCircle()
        {
            var radius = 40f + 40f * (float)random.NextDouble();
            return new Circle(
                radius + (CANVAS_X - radius * 2f) * (float)random.NextDouble(),
                radius + (CANVAS_Y - radius * 2f) * (float)random.NextDouble(),
                radius,
                0.6f + 0.5f * (float)random.NextDouble(),
                2f * (float)Math.PI * (float)random.NextDouble());
        }
    }
}
