using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media; // for the brushes

namespace MainProject._02_Brushes
{
    [PetzoldExampleProject(chapterNumber: 2, page: 37)]
    class VaryTheBackGround : Window
    {
        SolidColorBrush brush = new SolidColorBrush(Colors.Black);

        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new VaryTheBackGround());
        }

        public VaryTheBackGround()
        {
            Title = "Vary the Background";
            Width = 384;
            Height = 384;
            Background = brush;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            double width = 
                ActualWidth 
                - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;

            double height = 
                ActualHeight 
                - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight 
                - SystemParameters.CaptionHeight;

            Point ptMouse = e.GetPosition(this);
            Point ptCenter = new Point(width / 2, height / 2);

            Vector vectMouse = ptMouse - ptCenter;
            double angle = Math.Atan2(vectMouse.Y, vectMouse.X);
            Vector vectEllipse = new Vector(width / 2 * Math.Cos(angle), height / 2 * Math.Sin(angle));

            Byte byLevel = (byte)(255 * (1 - Math.Min(1, vectMouse.Length / vectEllipse.Length)));

            Color clr = brush.Color;
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;
        }
    }
}
