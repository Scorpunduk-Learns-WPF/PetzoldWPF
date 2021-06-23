using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;


namespace _34_DesignButton
{
    // page 106
    class DesignButton : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DesignButton());
        }

        public DesignButton()
        {
            Title = "Design Button";

            //Создание объекта Button как содержимого окна
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;

            //Создание объекта StackPanel как содержимого Button
            StackPanel stack = new StackPanel();
            btn.Content = stack;

            //Добавление объекта Polyline к StackPanel
            stack.Children.Add(ZigZag(10));

            //Добавление объекта Image
            Uri uri = new Uri("C:/#CSprojects/L/PetzoldWPF/34_DesignButton/Images/petzoldWPF.jpg");
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Margin = new Thickness(0, 10, 0, 0);
            img.Source = bitmap;
            img.Stretch = Stretch.None;
            stack.Children.Add(img);

            //Добавление объекта label
            Label lbl = new Label();
            lbl.Content = "_Read books!";
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            stack.Children.Add(lbl);

            //Добавление объекта PolyLine
            stack.Children.Add(ZigZag(0));
        }


        Polyline ZigZag(int offset)
        {
            Polyline poly = new Polyline();
            poly.Stroke = SystemColors.ControlTextBrush;
            poly.Points = new PointCollection();
            for (int x=0; x <= 100; x += 10)
            {
                poly.Points.Add(new Point(x, (x + offset) % 20));
            }
            return poly;
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The button has been clicked", Title);
        }

    }
}
