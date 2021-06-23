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

namespace MainProject._10_UserElements
{
    [PetzoldExampleProject(chapterNumber: 10, page: 198)]
    public class BetterEllipse : FrameworkElement
    {
        //Зависимые свойства
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty StrokeProperty;
        //Открытые интерфейсы к зависимым свойствам
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public Pen Stroke
        {
            get { return (Pen)GetValue(StrokeProperty); }
            set { SetValue(FillProperty, value); }
        }

        //Статический конструктор
        static BetterEllipse()
        {
            FillProperty =
                DependencyProperty.Register(
                    "Fill",
                    typeof(Brush),
                    typeof(BetterEllipse),
                    new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeProperty =
                DependencyProperty.Register(
                    "Stroke",
                    typeof(Pen),
                    typeof(BetterEllipse),
                    new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        //Переопредление MeasureOverride
        protected override Size MeasureOverride(Size availableSize)
        {
            Size sizeDesired = base.MeasureOverride(availableSize);
            if(Stroke != null)
            {
                sizeDesired = new Size(Stroke.Thickness, Stroke.Thickness);
            }
            return sizeDesired;
        }

        //Переопределение OnRender
        protected override void OnRender(DrawingContext drawingContext)
        {
            Size size = RenderSize;

            //Регулировка размера воспроизведения с учётом толщины Pen
            if (Stroke != null)
            {
                size.Width = Math.Max(0, size.Width - Stroke.Thickness);
                size.Height = Math.Max(0, size.Height - Stroke.Thickness);
            }

            //Рисование эллипса
            drawingContext.DrawEllipse(
                Fill,
                Stroke,
                new Point(RenderSize.Width / 2, RenderSize.Height / 2),
                size.Width / 2,
                size.Height / 2);
        }
    }

    public class SimpleEllipse : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brushes.Blue, new Pen(Brushes.Red, 24),
                new Point(RenderSize.Width / 2, RenderSize.Height / 2),
                RenderSize.Width / 2, RenderSize.Height / 2);
        }

    }
}
