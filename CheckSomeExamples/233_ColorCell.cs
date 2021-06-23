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

namespace CheckSomeExamples
{
    public class ColorCell : FrameworkElement
    {
        //Приватные поля
        static readonly Size sizeCell = new Size(20, 20);
        DrawingVisual visColor;
        Brush brush;

        //Зависимые свойства
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsHighlightedProperty;

        static ColorCell()
        {
            IsSelectedProperty =
                DependencyProperty.Register(
                    "IsSelected",
                    typeof(bool),
                    typeof(ColorCell),
                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
            IsHighlightedProperty =
                DependencyProperty.Register(
                    "IsHighlighted",
                    typeof(bool),
                    typeof(ColorCell),
                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        //Свойства
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public Brush Brush
        {
            get { return brush; }
        }

        //Конструктор получает аргумент Color
        public ColorCell(Color clr)
        {
            //Создание нового объекта DrawingVisual
            //и его сохранение в поле
            visColor = new DrawingVisual();
            DrawingContext dc = visColor.RenderOpen();

            //Рисование прямоугольника заданного цвета
            Rect rect = new Rect(new Point(0, 0), sizeCell);
            rect.Inflate(-4, -4);
            Pen pen = new Pen(SystemColors.ControlTextBrush, 1);
            brush = new SolidColorBrush(clr);
            dc.DrawRectangle(brush, pen, rect);
            dc.Close();

            //вызов AddVisualChild необходим для маршрутизации событий
            AddVisualChild(visColor);
            AddLogicalChild(visColor);
        }

        //Переопределение защищенных свойств и методов
        //для визуального дочернего объекта 
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return visColor;
        }

        //Переопределение защищенных методов определения размеров
        //и воспроизведение элемента
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            return sizeCell;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Rect rect = new Rect(new Point(0, 0), RenderSize);
            rect.Inflate(-1, -1);
            Pen pen = new Pen(SystemColors.HighlightBrush, 1);
            if (IsHighlighted)
            {
                dc.DrawRectangle(SystemColors.ControlLightBrush, pen, rect);
            }
        }

    }
}
