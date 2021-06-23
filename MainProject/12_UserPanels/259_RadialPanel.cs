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

namespace MainProject._12_UserPanels
{
    public class RadialPanel : Panel
    {
        //Зависмое свойство
        public static readonly DependencyProperty OrientationProperty;

        // Приватные поля
        bool showPieLines;
        double angleEach; // угол для каждого дочернего объекта
        Size sizeLargest; //размер наибольшего дочернего объекта

        double radius; // радиус круга
        double outerEdgeFromCenter;
        double innerEdgeFromCenter;

        // Статический конструктор для создания 
        // зависимого свойства Orientation
        static RadialPanel()
        {
            OrientationProperty =
                DependencyProperty.Register(
                    "Orientation",
                    typeof(RadialPanelOrientation),
                    typeof(RadialPanel),
                    new FrameworkPropertyMetadata(
                        RadialPanelOrientation.ByWidth,
                        FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        //Свойство Orientation
        public RadialPanelOrientation Orientation
        {
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        //Свойство wPieLines
        public bool ShowPieLines
        {
            get { return showPieLines; }
            set
            {
                if (value != showPieLines)
                {
                    InvalidateVisual();
                }
            }
        }

        //Переопределение MeasureOverride
        protected override Size MeasureOverride (Size sizeAvailable)
        {
            if(InternalChildren.Count == 0) { return new Size(0, 0); }
            angleEach = 365.0 / InternalChildren.Count;
            sizeLargest = new Size(0, 0);
            foreach(UIElement child in InternalChildren)
            {
                //Вызываем Measure для каждого дочернего объекта...
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                //... после чего проверяем свойство DesiredSize
                sizeLargest.Width = Math.Max(sizeLargest.Width, child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Height, child.DesiredSize.Height);
            }

            if(Orientation == RadialPanelOrientation.ByWidth)
            {
                //Вычисление расстояние от центра до краёв элемента
                innerEdgeFromCenter = sizeLargest.Width / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Height;

                //Вычисление радиуса круга по размерам
                // самого большого дочернего объекта
                radius = 
                    Math.Sqrt(Math.Pow(sizeLargest.Width / 2, 2) + 
                    Math.Pow(outerEdgeFromCenter, 2));
            }
            else
            {
                // Вычисление расстояния от центра до краёв элемента
                innerEdgeFromCenter = sizeLargest.Height / 2 /
                    Math.Tan(Math.PI * angleEach / 360);
                // Вычисление радиуса круга по размером
                // самого большого дочернего объекта
                radius = Math.Sqrt(
                    Math.Pow(sizeLargest.Height / 2, 2) +
                    Math.Pow(outerEdgeFromCenter, 2));
            }
            // Возвращение размера круга
            return new Size(2 * radius, 2 * radius);
        }

        //Переопределение ArrangeOverride
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            double angleChild = 0;
            Point ptCenter = new Point(sizeFinal.Width / 2, sizeFinal.Height / 2);
            double multiplier = 
                Math.Min(
                sizeFinal.Width / (2 * radius),
                sizeFinal.Height / (2 * radius));

            foreach(UIElement child in InternalChildren)
            {
                //Сброс RenderTransform
                child.RenderTransform = Transform.Identity;
                if(Orientation == RadialPanelOrientation.ByWidth)
                {
                    //Размещение дочернего объекта наверху
                    child.Arrange(
                        new Rect(ptCenter.X - multiplier * sizeLargest.Width / 2,
                        ptCenter.Y - multiplier * outerEdgeFromCenter,
                        multiplier * sizeLargest.Width,
                        multiplier * sizeLargest.Height));
                }
                else
                {
                    //Размещение дочернего объекта справа
                    child.Arrange(
                        new Rect(ptCenter.X + multiplier * innerEdgeFromCenter,
                        ptCenter.Y - multiplier * sizeLargest.Height / 2,
                        multiplier * sizeLargest.Width,
                        multiplier * sizeLargest.Height));
                }

                //Поворот дочернего объекта вокруг центра
                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform =
                    new RotateTransform(angleChild, pt.X, pt.Y);

                //увеличение угла
                angleChild += angleEach;
            }
            return sizeFinal;
        }

        // Переопределние OnRender рисует границы секторов (по желанию)
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (ShowPieLines)
            {
                Point ptCenter =
                    new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier =
                    Math.Min(
                        RenderSize.Width / (2 * radius),
                        RenderSize.Height / (2 * radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                //Вывод круга
                dc.DrawEllipse(
                    null,
                    pen,
                    ptCenter,
                    multiplier * radius,
                    multiplier * radius);

                //инициализация угла
                double angleChild = -angleEach / 2;
                if (Orientation == RadialPanelOrientation.ByWidth) { angleChild += 90; }
                //Loop through each child to draw radial lines from center
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter,
                        new Point(ptCenter.X + multiplier * radius *
                        Math.Cos(2 * Math.PI * angleChild / 360),
                        ptCenter.Y + multiplier * radius *
                        Math.Sin(2 * Math.PI * angleChild / 360)));
                    angleChild += angleEach;
                }
            }
        }
    }

    public enum RadialPanelOrientation
    {
        ByWidth,
        ByHeight
    }
}
