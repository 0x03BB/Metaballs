using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Metaballs.Shared;

namespace Metaballs
{
    public class MarchingSquares
    {
        private readonly Metaballs metaballs;

        //public readonly byte[,] CellTypes;
        public readonly List<Line> Lines = new List<Line>(1000);

        public MarchingSquares(Metaballs metaballs)
        {
            this.metaballs = metaballs;

            //CellTypes = new byte[GRID_X, GRID_Y];
        }

        public void March()
        {
            metaballs.Sample();
            AddLines();
        }

        private void AddLines()
        {
            Lines.Clear();

            for (var x = 0; x < GRID_X; x++)
            {
                for (var y = 0; y < GRID_Y; y++)
                {
                    AddGridLinesLerp(x * RESOLUTION, y * RESOLUTION, metaballs.Samples[x, y], metaballs.Samples[x, y + 1], metaballs.Samples[x + 1, y], metaballs.Samples[x + 1, y + 1]);
                }
            }
        }

        private void AddGridLines(float X, float Y, float TL, float BL, float TR, float BR)
        {
            float average;

            switch (Classify(TL, BL, TR, BR))
            {
                case 0:
                case 15:
                    break;
                case 1:
                case 14:
                    Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X, Y + HALF_RESOLUTION)));
                    break;
                case 2:
                case 13:
                    Lines.Add(new Line(new Vector2(X, Y + HALF_RESOLUTION), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    break;
                case 3:
                case 12:
                    Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    break;
                case 4:
                case 11:
                    Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION)));
                    break;
                case 5:
                case 10:
                    Lines.Add(new Line(new Vector2(X, Y + HALF_RESOLUTION), new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION)));
                    break;
                case 7:
                case 8:
                    Lines.Add(new Line(new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    break;
                case 6:
                    average = Average(TL, BL, TR, BR);
                    if (average < 1)
                    {
                        Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X, Y + HALF_RESOLUTION), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X, Y + HALF_RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    }
                    break;
                case 9:
                    average = Average(TL, BL, TR, BR);
                    if (average < 1)
                    {
                        Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X, Y + HALF_RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Vector2(X + HALF_RESOLUTION, Y), new Vector2(X + RESOLUTION, Y + HALF_RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X, Y + HALF_RESOLUTION), new Vector2(X + HALF_RESOLUTION, Y + RESOLUTION)));
                    }
                    break;
            }
        }

        private void AddGridLinesLerp(float X, float Y, float TL, float BL, float TR, float BR)
        {
            float average;

            switch (Classify(TL, BL, TR, BR))
            {
                case 0:
                case 15:
                    break;
                case 1:
                case 14:
                    Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION)));
                    break;
                case 2:
                case 13:
                    Lines.Add(new Line(new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    break;
                case 3:
                case 12:
                    Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    break;
                case 4:
                case 11:
                    Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION)));
                    break;
                case 5:
                case 10:
                    Lines.Add(new Line(new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION), new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION)));
                    break;
                case 7:
                case 8:
                    Lines.Add(new Line(new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    break;
                case 6:
                    average = Average(TL, BL, TR, BR);
                    if (average < 1)
                    {
                        Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    }
                    break;
                case 9:
                    average = Average(TL, BL, TR, BR);
                    if (average < 1)
                    {
                        Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Vector2(X + Lerp(TL, TR) * RESOLUTION, Y), new Vector2(X + RESOLUTION, Y + Lerp(TR, BR) * RESOLUTION)));
                        Lines.Add(new Line(new Vector2(X, Y + Lerp(TL, BL) * RESOLUTION), new Vector2(X + Lerp(BL, BR) * RESOLUTION, Y + RESOLUTION)));
                    }
                    break;
            }
        }

        private static byte Classify(float TL, float BL, float TR, float BR)
        {
            // Bit positions:
            // *--------*
            // |0      2|
            // |        |
            // |1      3|
            // *--------*

            return (byte)(
                 ByteCast(TL >= 1) |
                (ByteCast(BL >= 1) << 1) |
                (ByteCast(TR >= 1) << 2) |
                (ByteCast(BR >= 1) << 3));
        }

        private static float Average(float TL, float BL, float TR, float BR) => (TL + BL + TR + BR) / 4;

        private static float Lerp(float a, float b) => (1 - a) / (b - a);
    }
}
