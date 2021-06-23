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
using System.Windows.Shapes;

namespace MainProject._09_RoutedEvents
{
    [PetzoldExampleProject(chapterNumber: 9, page: 186)]
    public class ShadowTheStylus : Window
    {
        //Определения констант
        static readonly SolidColorBrush brushStylus = Brushes.Blue;
        static readonly SolidColorBrush brushShadow = Brushes.LightBlue;
        static readonly double widthStroke = 96 / 2.54;
        static readonly Vector vectShadow = new Vector(widthStroke / 4, widthStroke / 4);

        // Дополниительные поля для операций перемещения стилуса
        Canvas canv;
        Polyline polyStylus, polyShadow;
        bool isDrawing;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShadowTheStylus());
        }

        public ShadowTheStylus()
        {
            Title = "Shadow the Stylus";

            //Создание панели Canvas для содержимого окна
            canv = new Canvas();
            Content = canv;
        }

        protected override void OnStylusDown(StylusDownEventArgs e)
        {
            base.OnStylusDown(e);
            Point ptStylus = e.GetPosition(canv);

            //Создание основного объекта Polyline
            //с закруглёнными концами отрезков
            polyStylus = new Polyline();
            polyStylus.Stroke = brushStylus;
            polyStylus.StrokeThickness = widthStroke;
            polyStylus.StrokeStartLineCap = PenLineCap.Round;
            polyStylus.StrokeEndLineCap = PenLineCap.Round;
            polyStylus.StrokeLineJoin = PenLineJoin.Round;
            polyStylus.Points = new PointCollection();
            polyStylus.Points.Add(ptStylus);

            //Создание основного объекта Polyline
            //с закруглёнными концами отрезков
            polyShadow = new Polyline();
            polyShadow.Stroke = brushShadow;
            polyShadow.StrokeThickness = widthStroke;
            polyShadow.StrokeStartLineCap = PenLineCap.Round;
            polyShadow.StrokeEndLineCap = PenLineCap.Round;
            polyShadow.StrokeLineJoin = PenLineJoin.Round;
            polyShadow.Points = new PointCollection();
            polyShadow.Points.Add(ptStylus + vectShadow);

            //Тень вставляется перед ломаными переднего плана
            canv.Children.Insert(canv.Children.Count / 2, polyShadow);

            //Основная ломаная добавляется последней
            canv.Children.Add(polyStylus);

            CaptureStylus();
            isDrawing = true;
            e.Handled = true;
        }

        protected override void OnStylusMove(StylusEventArgs e)
        {
            base.OnStylusMove(e);
            if (isDrawing)
            {
                Point ptStylus = e.GetPosition(canv);
                polyStylus.Points.Add(ptStylus);
                polyStylus.Points.Add(ptStylus + vectShadow);
                e.Handled = true;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            //Рисование завершается клавишей Escape
            if (isDrawing && e.Text.IndexOf('\x1B') != -1)
            {
                ReleaseStylusCapture();
                e.Handled = true;
            }
        }

        protected override void OnLostStylusCapture(StylusEventArgs e)
        {
            base.OnLostStylusCapture(e);

            //Аномальное завершение рисования: удаление ломаных
            if (isDrawing)
            {
                canv.Children.Remove(polyStylus);
                canv.Children.Remove(polyShadow);
                isDrawing = false;
            }
        }
    }
}
