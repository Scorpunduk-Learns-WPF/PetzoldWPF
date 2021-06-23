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

namespace MainProject.OneChildElements
{
    [PetzoldExampleProject(chapterNumber: 11, page: 216)]
    public class EncloseElementInEllipse : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new EncloseElementInEllipse());
        }

        public EncloseElementInEllipse()
        {
            Title = "Enclose Element in Ellipse";
            EllipseWithChild ellipse = new EllipseWithChild();
            ellipse.Fill = Brushes.ForestGreen;
            ellipse.Stroke = new Pen(Brushes.Magenta, 48);
            Content = ellipse;
            TextBlock text = new TextBlock();
            text.FontFamily = new FontFamily("Times Nes Roman");
            text.FontSize = 48;
            text.Text = "Text inside ellipse";

            //Свойству Child объекта EllipseWithChild
            // задаётся объект TextBlock
            ellipse.Child = text;
        }
    }
}
