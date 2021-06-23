using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MainProject._07_Canvas
{
    public class Tile : Canvas
    {
        const int SIZE = 64;
        const int BORD = 6;
        TextBlock txtblk;

        public Tile()
        {
            Width = SIZE;
            Height = SIZE;

            // Граница на левой и верхней сторонах
            Polygon poly = new Polygon();
            poly.Points = new PointCollection(new Point[]
            {
                new Point(0,0),
                new Point(SIZE, 0),
                new Point(SIZE - BORD, BORD),
                new Point(BORD, BORD),
                new Point(BORD, SIZE - BORD),
                new Point(0, SIZE)
            });
            poly.Fill = SystemColors.ControlLightLightBrush;
            Children.Add(poly);

            //Граница на правой и нижней сторонах
            poly = new Polygon();
            poly.Points = new PointCollection(new Point[]
            {
                new Point(SIZE, SIZE),
                new Point(SIZE, 0),
                new Point(SIZE-BORD, BORD),
                new Point(SIZE-BORD, SIZE-BORD),
                new Point(BORD, SIZE-BORD),
                new Point(0, SIZE)
            });
            poly.Fill = SystemColors.ControlDarkBrush;
            Children.Add(poly);

            // Поле для вывода отцентриованного текста
            Border bord = new Border();
            bord.Width = SIZE - 2 * BORD;
            bord.Height = SIZE - 2 * BORD;
            bord.Background = SystemColors.ControlBrush;
            Children.Add(bord);
            SetLeft(bord, BORD);
            SetTop(bord, BORD);

            //Вывод текста
            txtblk = new TextBlock();
            txtblk.FontSize = 32;
            txtblk.Foreground = SystemColors.ControlBrush;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            txtblk.VerticalAlignment = VerticalAlignment.Center;
            bord.Child = txtblk;
        }

        //Открытое свойство для назначения текста
        public string Text
        {
            get { return txtblk.Text; }
            set { txtblk.Text = value; }
        }
    }

    public class Empty : FrameworkElement
    {

    }
}
