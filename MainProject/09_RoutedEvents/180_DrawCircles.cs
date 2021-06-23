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
    [PetzoldExampleProject(chapterNumber: 9, page: 180)]
    public class DrawCircles : Window
    {
        Canvas canv;

        //Поля, относящиеся к рисованию
        bool isDrawing;
        Ellipse ellipse;
        Point ptCenter;

        //Поля, относящиеся к перетаскиванию
        bool isDragging;
        FrameworkElement elDragging;
        Point ptMouseStart, ptElementStart;
        
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DrawCircles());
        }

        public DrawCircles()
        {
            Title = "Draw Circles";
            Content = canv = new Canvas();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (isDragging)
            {
                return;
            }

            //Создание нового объекта Ellipse
            //и его добавление на панель Canvas
            ptCenter = e.GetPosition(canv);
            ellipse = new Ellipse();
            ellipse.Stroke = SystemColors.WindowTextBrush;
            ellipse.StrokeThickness = 1;
            ellipse.Width = 0;
            ellipse.Height = 0;
            canv.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, ptCenter.X);
            Canvas.SetTop(ellipse, ptCenter.Y);

            //Захват мыши и подготовка к будущим событиям
            CaptureMouse();
            isDrawing = true;
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (isDrawing)
            {
                return;
            }

            //Получение элемента, на котором был сделан щелчок,
            // и подготовка к будущим событиям
            ptMouseStart = e.GetPosition(canv);
            elDragging = canv.InputHitTest(ptMouseStart) as FrameworkElement;

            if(elDragging != null)
            {
                ptElementStart = new Point(Canvas.GetLeft(elDragging), Canvas.GetTop(elDragging));
                isDragging = true;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if(e.ChangedButton == MouseButton.Middle)
            {
                Shape shape = canv.InputHitTest(e.GetPosition(canv)) as Shape;
                if(shape != null)
                {
                    shape.Fill = (shape.Fill == Brushes.Red ? Brushes.Transparent : Brushes.Red);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point ptMouse = e.GetPosition(canv);
            //Перемещение и изменение размеров эллипса
            if (isDrawing)
            {
                double dRadius = Math.Sqrt(
                    Math.Pow(ptCenter.X - ptMouse.X, 2) +
                    Math.Pow(ptCenter.Y - ptMouse.Y, 2));
                Canvas.SetLeft(ellipse, ptCenter.X - dRadius);
                Canvas.SetTop(ellipse, ptCenter.Y - dRadius);
                ellipse.Width = 2 * dRadius;
                ellipse.Height = 2 * dRadius;
            }

            //Перемещение эллипса
            else if (isDragging)
            {
                Canvas.SetLeft(
                    elDragging,
                    ptElementStart.X + ptMouse.X - ptMouseStart.X);
                Canvas.SetTop(
                    elDragging,
                    ptElementStart.Y + ptMouse.Y - ptMouseStart.Y);                    
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            //Завершение операции рисования 
            if (isDrawing && e.ChangedButton == MouseButton.Left)
            {
                ellipse.Stroke = Brushes.Blue;
                ellipse.StrokeThickness = Math.Min(24, ellipse.Width / 2);
                ellipse.Fill = Brushes.Red;
                isDrawing = false;
                ReleaseMouseCapture();
            }
            //Выход из режима захвата
            else if (isDragging && e.ChangedButton == MouseButton.Right)
            {
                isDragging = false;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            //Нажатие Escape прерывает рисовние и перетаскивание
            if(e.Text.IndexOf('\x1B') != -1)
            {
                if (isDrawing)
                {
                    ReleaseMouseCapture();
                }
                else if (isDragging)
                {
                    Canvas.SetLeft(elDragging, ptElementStart.X);
                    Canvas.SetTop(elDragging, ptElementStart.Y);
                    isDragging = false;
                }
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            // Аномальное завершение рисования: удаление эллипса
            if (isDrawing)
            {
                canv.Children.Remove(ellipse);
                isDrawing = false;
            }
        }
    } 
}
