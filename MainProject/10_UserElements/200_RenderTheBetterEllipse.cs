using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MainProject._10_UserElements
{
    [PetzoldExampleProject(chapterNumber: 10, page: 200)]
    public class RenderTheBetterEllipse : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new RenderTheBetterEllipse());
        }

        public RenderTheBetterEllipse()
        {
            Title = "Render the Better Ellipse";
            BetterEllipse ellipse = new BetterEllipse();
            ellipse.Fill = Brushes.AliceBlue;
            ellipse.Stroke = new Pen(
                new LinearGradientBrush(
                    Colors.CadetBlue, 
                    Colors.Chocolate, 
                    new Point(1, 0), 
                    new Point(0, 1)), 
                24);

            Content = ellipse;
        }
    }
}
