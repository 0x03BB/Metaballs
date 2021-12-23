using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Metaballs.Shared;

namespace Metaballs
{
    public sealed partial class MainPage : Page
    {
        private readonly Metaballs metaballs = new Metaballs();
        private readonly MarchingSquares marchingSquares;
        //private readonly StringBuilder stringBuilder = new StringBuilder(100);

        public MainPage()
        {
            InitializeComponent();
            canvas.Width = CANVAS_X;
            canvas.Height = CANVAS_Y;
            canvas.TargetElapsedTime = TimeSpan.FromTicks(100000);
            marchingSquares = new MarchingSquares(metaballs);
        }

        private void CanvasControl_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            metaballs.Update(args.Timing.ElapsedTime.Ticks);
        }

        private void CanvasControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            marchingSquares.March();

            //for (var x = 0; x < GRID_X_SAMPLE; x++)
            //{
            //    for (var y = 0; y < GRID_Y_SAMPLE; y++)
            //    {
            //        args.DrawingSession.DrawRectangle(x * RESOLUTION - 1, y * RESOLUTION - 1, 2, 2, Colors.Green);
            //        if (metaballs.Samples[x, y] >= 1)
            //        {
            //            args.DrawingSession.DrawRectangle(x * RESOLUTION - HALF_RESOLUTION, y * RESOLUTION - HALF_RESOLUTION, RESOLUTION, RESOLUTION, Colors.Green);
            //        }
            //    }
            //}

            //for (var x = 0; x < GRID_X; x++)
            //{
            //    for (var y = 0; y < GRID_Y; y++)
            //    {
            //        args.DrawingSession.DrawText(marchingSquares.CellTypes[x, y].ToString(), x * RESOLUTION + 10, y * RESOLUTION + 5, Colors.Red);
            //    }
            //}

            var lines = marchingSquares.Lines;
            //stringBuilder.AppendLine(lines.Count.ToString());
            for (var i = 0; i < lines.Count; i++)
            {
                args.DrawingSession.DrawLine(lines[i].point0, lines[i].point1, Colors.Green, 2);
            }
            //args.DrawingSession.DrawText(lines.Count.ToString(), 0, 0, Colors.White);

            //for (var i = 0; i < Metaballs.CIRCLE_COUNT; i++)
            //{
            //    args.DrawingSession.DrawCircle(metaballs.circles[i].CenterPoint, metaballs.circles[i].Radius, Colors.White, 2f);
            //}

            //stringBuilder.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Paused = !canvas.Paused;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            canvas.RemoveFromVisualTree();
            canvas = null;
        }
    }
}
