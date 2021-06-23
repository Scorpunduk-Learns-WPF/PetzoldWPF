using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MainProject._10_UserElements
{
    public class MedievalButton : Control
    {
        //Всего два приватных поля
        FormattedText formtxt;
        bool isMouseReallyOver;

        //Статтические поля, доступные только для чтения
        public static readonly DependencyProperty TextProperty;
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent PreviewKnockEvent;

        //Статический конструктор
        static MedievalButton()
        {
            //Регистрация зависимого свойства
            TextProperty =
                DependencyProperty.Register(
                    "Text",
                    typeof(string),
                    typeof(MedievalButton),
                    new FrameworkPropertyMetadata(
                        " ",
                        FrameworkPropertyMetadataOptions.AffectsMeasure));

            //Регистрация маршрутизируемых событий
            KnockEvent =
                EventManager.RegisterRoutedEvent(
                    "Knock",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(MedievalButton));
            PreviewKnockEvent =
                EventManager.RegisterRoutedEvent(
                    "PreviewKnock",
                    RoutingStrategy.Tunnel,
                    typeof(RoutedEventHandler),
                    typeof(MedievalButton));
        }

        //Открытый интерфейс к зависимому свойсту property
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value == null ? " " : value); }
        }

        //Открытый интерфейс к маршрутизируемым событиям
        public event RoutedEventHandler Knock
        {
            add { AddHandler(KnockEvent, value); }
            remove { RemoveHandler(KnockEvent, value); }
        }

        public event RoutedEventHandler PreviewKnock
        {
            add { AddHandler(PreviewKnockEvent, value); }
            remove { RemoveHandler(PreviewKnockEvent, value); }
        }

        //Метод вызывается при возможном изменении размеров кнопка
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            formtxt = new FormattedText(
                Text,
                CultureInfo.CurrentCulture,
                FlowDirection,
                new Typeface(
                    FontFamily,
                    FontStyle,
                    FontWeight,
                    FontStretch),
                FontSize,
                Foreground,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            // последний аргумент pixelsperdip 
            // https://stackoverflow.com/questions/1918877/how-can-i-get-the-dpi-in-wpf

            //Внутренние отступы учитываются при вычислении размера
            Size sizeDesired = new Size(Math.Max(48, formtxt.Width) + 4, formtxt.Height + 4);
            sizeDesired.Width += Padding.Left + Padding.Right;
            sizeDesired.Height += Padding.Top + Padding.Bottom;
            return sizeDesired;
        }
        //Метод OnRender вызывается для перерисовки кнопки
        protected override void OnRender(DrawingContext dc)
        {
            //Определение цвета фона
            Brush brushBackground = SystemColors.ControlBrush;
            if(isMouseReallyOver && IsMouseCaptured)
            {
                brushBackground = SystemColors.ControlDarkBrush;
            }

            //Определение ширины пера
            Pen pen = new Pen(Foreground, IsMouseOver ? 2 : 1);

            //Рисование прямоугольника с закруглёнными углами
            dc.DrawRoundedRectangle(brushBackground, pen, new Rect(new Point(0, 0), RenderSize), 4, 4);

            //Определение основного цвета
            formtxt.SetForegroundBrush(IsEnabled ? Foreground : SystemColors.ControlDarkBrush);

            //Определение начальной точки текста
            Point ptText = new Point(2, 2);
            switch (HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                    ptText.X += Padding.Left;
                    break;
                case HorizontalAlignment.Right:
                    ptText.X += RenderSize.Width - formtxt.Width - Padding.Right;
                    break;
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    ptText.X += (RenderSize.Width - formtxt.Width - Padding.Left - Padding.Right) / 2;
                    break;
            }

            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Top:
                    ptText.Y += Padding.Top;
                    break;
                case VerticalAlignment.Bottom:
                    ptText.Y += RenderSize.Height - formtxt.Height - Padding.Bottom;
                    break;
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    ptText.Y += (RenderSize.Height - formtxt.Height - Padding.Top - Padding.Bottom) / 2;
                    break;
            }

            //Вывод текста
            dc.DrawText(formtxt, ptText);
        }

        //События мыши, влияющие на внешний вид кнопки
        protected override void OnMouseEnter(MouseEventArgs args)
        {
            base.OnMouseEnter(args);
            InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //Проверка направления движения
            Point pt = e.GetPosition(this);
            bool isReallyOverNow = (pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight);
            if(isReallyOverNow != isMouseReallyOver)
            {
                isMouseReallyOver = isReallyOverNow;
                InvalidateVisual();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            InvalidateVisual();
            e.Handled = true;
        }

        //Событие, фактически инициирующее "knock"
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (IsMouseCaptured)
            {
                if (isMouseReallyOver)
                {
                    OnPreviewKnock();
                    OnKnock();
                }
                e.Handled = true;
                Mouse.Capture(null);
            }
        }

        //При потере захвата мыши кнопка перерисовывается 
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            InvalidateVisual();
        }

        //Клавиши "пробел" и Enter также вызывают 
        //срабатываение кнопки
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.Key == Key.Space || e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if(e.Key == Key.Space || e.Key == Key.Enter)
            {
                OnPreviewKnock();
                OnKnock();
                e.Handled = true;
            }
        }

        //Метод OnKnock инициирует событие Knock
        protected virtual void OnKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton.PreviewKnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }

        //Метод OnPreviewKnock инициирует событие "PreviewKnock"
        protected virtual void OnPreviewKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton.KnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
