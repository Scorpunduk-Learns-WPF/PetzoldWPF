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

namespace MainProject.OneChildElements
{
    public class RoundedButton : Control
    {
        //Приватное поле
        RoundedButtonDecorator decorator;

        //Открытое статическое событие ClickEvent
        public static readonly RoutedEvent ClickEvent;

        // Статический Конструктор
        static RoundedButton()
        {
            ClickEvent =
                EventManager.RegisterRoutedEvent(
                    "Click",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(RoundedButton));
        }

        //Конструктор
        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }

        //Открытые свойства
        public UIElement Child
        {
            get { return decorator.Child; }
            set { decorator.Child = value; }
        }

        public bool IsPressed
        {
            get { return decorator.IsPressed; }
            set { decorator.IsPressed = value; }
        }

        //Открытое событие
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        //Переопределения свойства и методов
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if(index > 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return decorator;
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            decorator.Measure(sizeAvailable);
            return decorator.DesiredSize;
        }

        protected override Size ArrangeOverride(Size sizeArrange)
        {
            decorator.Arrange(new Rect(new Point(0, 0), sizeArrange));
            return sizeArrange;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsMouseCaptured)
            {
                IsPressed = IsMouseReallyOver;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            IsPressed = true;
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);
            if (IsMouseCaptured)
            {
                if (IsMouseReallyOver)
                {
                    OnClick();
                }
                Mouse.Capture(null);
                IsPressed = false;
                e.Handled = true;
            }
        }

        bool IsMouseReallyOver
        {
            get
            {
                Point pt = Mouse.GetPosition(this);
                return (pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight);
            }
        }

        //Защищенный метод, инициирующий событие Click
        protected virtual void OnClick()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = RoundedButton.ClickEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
