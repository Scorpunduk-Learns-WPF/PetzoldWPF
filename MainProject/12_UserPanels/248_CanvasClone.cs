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
    public class CanvasClone : Panel
    {
        // Определение двух зависимых свойств
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty TopProperty;
        static CanvasClone()
        {
            //Регистрация зависимых свойств как присоединённых
            //Знначение по умолчанию равно 0, при любом изменении
            //размещение родителя становится недействительным
            LeftProperty = DependencyProperty.RegisterAttached(
                "Left",
                typeof(double),
                typeof(CanvasClone),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsParentArrange));
            TopProperty = DependencyProperty.RegisterAttached(
                "Top",
                typeof(double),
                typeof(CanvasClone),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsParentArrange));
        }

        //Статические методы для чтения и записи присоединённых свойств
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }
        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }
        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        //Переопределение MeasureOverride просто вызывает Measure
        //для дочерних объектов
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            foreach(UIElement child in InternalChildren)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            }

            return base.MeasureOverride(sizeAvailable);
        }

        //Переопределение ArrangeOverride размещает дочерние объекты
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(
                    new Rect(new Point(
                        GetLeft(child), 
                        GetTop(child)), 
                        child.DesiredSize)
                    );
            }
            return sizeFinal;
        }
    }
}
