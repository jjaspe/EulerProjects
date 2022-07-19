using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Topgram;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _2DSandbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Color[] colors;
        History history;
        int index = 0;
        private int increase = 0;
        PieceBuilder builder = new PieceBuilder();

        public MainPage()
        {
            this.InitializeComponent();
            colors = new Color[]
            {
                Colors.Red,
                Colors.Blue,
                Colors.Pink,
                Colors.Orange,
                Colors.Green,
                Colors.Black,
                Colors.Pink
            };
        }

        void LoadHistory(int? countFilter = null)
        {
            history = FileLoader
                            .LoadHistory("Topgram_Pieces.txt");
            if(countFilter != null)
            {
                history.Steps = history.Steps.Where(n => n.Count == countFilter.Value).ToList();
            }
        }

        void LoadSolution()
        {
            history = FileLoader
                            .LoadHistory("Topgram_Solution.txt");
        }

        void CanvasControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {            
            try
            {
                var center = new Vector2((float)sender.Size.Width / 2, (float)sender.Size.Height / 2);
                var rombus = builder.BuilRombus(4, "rombus");
                for (int i = 0; i < 4; i++)
                {
                    var vertices2 = GetPolygonFromPiece(rombus.rotatedPieces[i], center);
                    var g2 = CanvasGeometry.CreatePolygon(sender, vertices2);
                    args.DrawingSession.DrawGeometry(g2, colors[i]);
                }
                if(index+increase < history.Steps.Count)
                {
                    index += increase;
                }
                else
                {
                    index = history.Steps.Count - 1;
                }
                var pieces = history.Steps[index];
                for (int i = 0; i < pieces.Count; i++)
                {
                    var vertices = GetPolygonFromPiece(pieces[i], center);
                    var geometry = CanvasGeometry.CreatePolygon(sender, vertices);
                    args.DrawingSession.DrawGeometry(geometry, colors[i]);
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);               
            }
        }

        Vector2[] GetPolygonFromPiece(RotatedPiece piece, Vector2 center)
        {
            var vectors = piece.Corners.Select(n => new Vector2(n.X * 100 + center.X,
                -n.Y * 100 + center.Y))
                .ToArray();
            return vectors;

        }

        private void CanvasAnimatedControl_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            CanvasCommandList cl = new CanvasCommandList(sender);
            using (CanvasDrawingSession clds = cl.CreateDrawingSession())
            {
                clds.DrawLine(new Vector2(0,(float)sender.Size.Height/2), new Vector2((float)sender.Width, (float)sender.Size.Height / 2)
                    , Colors.Black);
                clds.DrawLine(new Vector2((float)sender.Size.Width / 2, 0), new Vector2((float)sender.Width/2, (float)sender.Size.Height)
                    , Colors.Black);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.index++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.increase = 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.increase = 0;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.index = 0;
            this.increase = 0;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadHistory();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSolution();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.increase = this.increase * 2;
        }
    }
}
