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
    public class EllipseWithChild : _10_UserElements.BetterEllipse
    {
        UIElement child;
        
        // Открытое свойство Child
        public UIElement Child
        {
            get { return child; }
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }
        }

        //Переопределение VisualChildrenCount возвращает 1
        //если свойство Child отлично от null
        protected override int VisualChildrenCount
        {
            get
            {
                return Child != null ? 1 : 0;
            }
        }

        // Переопределение  GetVisualChildren возвращает Child
        protected override Visual GetVisualChild(int index)
        {
            if(index > 0 || Child == null)
            {
                throw new ArgumentOutOfRangeException("index");                
            }
            return Child;
        }

        //Переопределение MeasureOverride вызывает метод
        // Measure дочернего объекта
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = new Size(0, 0);
            if(Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;
                sizeAvailable.Width =
                    Math.Max(0, sizeAvailable.Width - 2 * Stroke.Thickness);
                sizeAvailable.Height = Math.Max(0, sizeAvailable.Height - 2 * Stroke.Thickness);
            }

            if(Child != null)
            {
                Child.Measure(sizeAvailable);
                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Width += Child.DesiredSize.Height;
            }
            return sizeDesired;
        }

        //Переопределение ArrangeOverride вызывает метод
        //Arrange дочернего объекта
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            if(Child != null)
            {
                Rect rect = new Rect(
                    new Point((sizeFinal.Width - Child.DesiredSize.Width) / 2, (sizeFinal.Height - Child.DesiredSize.Height) / 2),
                    Child.DesiredSize);
                Child.Arrange(rect);
            }
            return sizeFinal;
        }
    }
}
