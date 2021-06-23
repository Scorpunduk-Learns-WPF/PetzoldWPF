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


namespace CheckSomeExamples
{
    public class SelectColor : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SelectColor());
        }

        public SelectColor()
        {
            Title = "Select Color";
            SizeToContent = SizeToContent.WidthAndHeight;

            //Создание объекта StackPanel как содержимого окна
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            //Создание фиктивной кнопки для проверки передачи фокуса
            Button btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            // Создание элемента ColorGrid
            ColorGrid clrgrid = new ColorGrid();
            clrgrid.Margin = new Thickness(24);
            clrgrid.HorizontalAlignment = HorizontalAlignment.Center;
            clrgrid.VerticalAlignment = VerticalAlignment.Center;
            clrgrid.SelectedColorChanged += ColorGridOnSelectedColorChanged;
            stack.Children.Add(clrgrid);

            // Создание другой фиктивной кнопки
            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }

        void ColorGridOnSelectedColorChanged(object sender, EventArgs args)
        {
            ColorGrid clrgrid = sender as ColorGrid;
            Background = new SolidColorBrush(clrgrid.SelectedColor);
        }
    }
}
