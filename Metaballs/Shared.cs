using System.Runtime.CompilerServices;

namespace Metaballs
{
    public static class Shared
    {
        public const int CANVAS_X = 1000;
        public const int CANVAS_Y = 1000;
        public const int RESOLUTION = 5;
        public const float HALF_RESOLUTION = RESOLUTION / 2;
        public const int GRID_X = CANVAS_X / RESOLUTION;
        public const int GRID_Y = CANVAS_Y / RESOLUTION;
        public const int GRID_X_SAMPLE = GRID_X + 1;
        public const int GRID_Y_SAMPLE = GRID_Y + 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static byte ByteCast(bool b) => *(byte*)&b;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int IntCast(bool b) => *(int*)&b;
    }
}
