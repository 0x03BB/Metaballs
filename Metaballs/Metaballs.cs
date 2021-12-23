using System;
using static Metaballs.Shared;

namespace Metaballs
{
    public class Metaballs
    {
        public const int CIRCLE_COUNT = 9;
        public readonly Circle[] circles = new Circle[CIRCLE_COUNT];
        public readonly float[,] Samples;

        public Metaballs()
        {
            Samples = new float[GRID_X_SAMPLE, GRID_X_SAMPLE];
            for (int i = 0; i < CIRCLE_COUNT; i++)
            {
                circles[i] = Circle.GenerateCircle();
            }
        }

        public void Update(long dt)
        {
            for (var i = 0; i < CIRCLE_COUNT; ++i)
            {
                ref var circle = ref circles[i];
                if (circle.CenterPoint.X + circle.Radius > CANVAS_X)
                {
                    circle.Velocity.X = -Math.Abs(circle.Velocity.X);
                }
                else if (circle.CenterPoint.X - circle.Radius < 0)
                {
                    circle.Velocity.X = Math.Abs(circle.Velocity.X);
                }

                if (circle.CenterPoint.Y + circles[i].Radius > CANVAS_Y)
                {
                    circle.Velocity.Y = -Math.Abs(circle.Velocity.Y);
                }
                else if (circle.CenterPoint.Y - circles[i].Radius < 0)
                {
                    circle.Velocity.Y = Math.Abs(circle.Velocity.Y);
                }

                circle.CenterPoint.X += circle.Velocity.X * (dt / 100000f);
                circle.CenterPoint.Y += circle.Velocity.Y * (dt / 100000f);
            }
        }

        public void Sample()
        {
            for (var x = 0; x < GRID_X_SAMPLE; x++)
            {
                for (var y = 0; y < GRID_Y_SAMPLE; y++)
                {
                    var sample = 0f;
                    for (var i = 0; i < CIRCLE_COUNT; i++)
                    {
                        ref var circle = ref circles[i];
                        var xi = x * RESOLUTION - circle.CenterPoint.X;
                        var yi = y * RESOLUTION - circle.CenterPoint.Y;
                        sample += circle.RadiusSquared / (xi * xi + yi * yi);
                    }
                    Samples[x, y] = sample;
                }
            }
        }
    }
}
