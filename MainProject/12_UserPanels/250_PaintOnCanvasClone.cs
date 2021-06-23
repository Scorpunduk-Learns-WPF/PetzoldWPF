using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MainProject._12_UserPanels
{
    [PetzoldExampleProject(chapterNumber: 12, page: 250)]
    public class PaintOnCanvasClone : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new PaintOnCanvasClone());
        }

        public PaintOnCanvasClone()
        {
            Title = "Paint on Canvas Clone";
            Canvas canv = new Canvas();
            Content = canv;
            SolidColorBrush[] brushes =
                {Brushes.Red, Brushes.Green, Brushes.Blue };
            for (int i = 0; i < brushes.Length; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Fill = brushes[i];
                rect.Width = 200;
                rect.Height = 200;
                canv.Children.Add(rect);
                Canvas.SetLeft(rect, 100 * (i + 1));
                Canvas.SetTop(rect, 100 * (i + 1));
            }
        }
    }
}
