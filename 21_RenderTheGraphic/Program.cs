using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents; // new Italic()
using System.ComponentModel;

namespace _21_RenderTheGraphic
{
    // page 71
    class RenderTheGraphic : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new RenderTheGraphic());
        }

        public RenderTheGraphic()
        {
            Title = "Render the Graphic";
            SimpleEllipse ellips = new SimpleEllipse();
            Content = ellips;
        }
    }

    class SimpleEllipse : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(
                Brushes.Blue, 
                new Pen(Brushes.Red, 2), 
                new Point(RenderSize.Width / 2, RenderSize.Height / 2), 
                RenderSize.Width / 2, 
                RenderSize.Height / 2);
        }
    }
}
