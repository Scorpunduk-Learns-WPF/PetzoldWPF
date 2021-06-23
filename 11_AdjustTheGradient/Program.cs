using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media; // for the brushes

using System.Reflection; // for propertyinfo

namespace _11_AdjustTheGradient
{
    // page 46
    class AdjustTheGradiate : Window
    {
        LinearGradientBrush brush;

        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new AdjustTheGradiate());
        }

        public AdjustTheGradiate()
        {
            Title = "Adjust the Gradient";
            SizeChanged += WindowOnSizeChanged;

            brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 0);
            brush.MappingMode = BrushMappingMode.Absolute;
            Background = brush;
        }

        void WindowOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;

            Point ptCenter = new Point(width / 2, height / 2);
            Vector vectDiag = new Vector(width, -height);
            Vector vectPerp = new Vector(vectDiag.Y, -vectDiag.X);

            vectPerp.Normalize();
            vectPerp *= width * height / vectDiag.Length;
            brush.StartPoint = ptCenter + vectPerp;
            brush.EndPoint = ptCenter - vectPerp;
        }

    }
}
