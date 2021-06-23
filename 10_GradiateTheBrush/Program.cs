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

// page 42
namespace _10_GradiateTheBrush
{
    class GradiateTheBrush : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new GradiateTheBrush());
        }

        public GradiateTheBrush()
        {
            Title = "Gradiate The Brush";
            LinearGradientBrush brush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
            Background = brush;
        }
    }
}
