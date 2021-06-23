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

namespace MainProject._13_ListBox
{
    [PetzoldExampleProject(chapterNumber: 13, page: 292)]
    public class SelectColorFromGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromGrid());
        }

        public SelectColorFromGrid()
        {
            Title = "Select Color from Grid";
            SizeToContent = SizeToContent.WidthAndHeight;

            //Создание объекта StackPanel как содержимого окна
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            //Фиктивная кнопка для проверки передачи фокуса
            Button btn = new Button();
            btn.Content = "Do nothing button \nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            //Создание элементаа ColorGridBox
            ColorGridBox clrgrid = new ColorGridBox();
            clrgrid.Margin = new Thickness(24);
            clrgrid.HorizontalAlignment = HorizontalAlignment.Center;
            clrgrid.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrgrid);

            //Привязка свойства Background окна
            // к выделенному значению ColorGridBox
            clrgrid.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrgrid.DataContext = this;

            //Создание другой фиктивной кнопки
            btn = new Button();
            btn.Content = "Do nothing button \nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
