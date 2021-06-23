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

namespace MainProject._02_Brushes
{
    [PetzoldExampleProject(chapterNumber: 2, page: 47)]
    public class FollowTheRainbow : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new FollowTheRainbow());
        }

        public FollowTheRainbow()
        {
            Title = "Follow the Rainbow";

            RadialGradientBrush brush = new RadialGradientBrush();
            //LinearGradientBrush brush = new LinearGradientBrush();
            //brush.StartPoint = new Point(0, 0);
            //brush.EndPoint = new Point(1, 0);
            Background = brush;

            brush.GradientOrigin = new Point(0.75, 0.75);

            brush.GradientStops.Add(new GradientStop(Colors.Red, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Orange, 0.17));
            brush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.33));
            brush.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, 0.67));
            brush.GradientStops.Add(new GradientStop(Colors.Indigo, 0.84));
            brush.GradientStops.Add(new GradientStop(Colors.Violet, 1));
        }

    }
}
