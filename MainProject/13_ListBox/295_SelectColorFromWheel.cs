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
    [PetzoldExampleProject(chapterNumber: 13, page: 295)]
    public class SelectColorFromWheel : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromWheel());
        }

        public SelectColorFromWheel()
        {
            Title = "Select Color from Wheel";
            SizeToContent = SizeToContent.WidthAndHeight;

            //Создание объекта StackPanel как содержимого окна
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;
            //Создание фиктивной кнопки для проверки передчаи фокуса
            Button btn = new Button();
            btn.Content = "Do nothing button \nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            //Создание элемента ColorWheel
            ColorWheel clrwheel = new ColorWheel();
            clrwheel.Margin = new Thickness(24);
            clrwheel.HorizontalAlignment = HorizontalAlignment.Center;
            clrwheel.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrwheel);

            //Привязка свойства Background окна к выделенному цвету
            clrwheel.SetBinding(ColorWheel.SelectedValueProperty, "Background");
            clrwheel.DataContext = this;

            //Создание другой фиктивной кнопки Button
            btn = new Button();
            btn.Content = "Do not nothing \nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
