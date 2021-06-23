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

using System.Windows.Shapes;
using MainProject._12_UserPanels;

using System.Windows.Data;
using System.Reflection;

namespace MainProject._13_ListBox
{
    public class ColorWheel : ListBox
    {
        public ColorWheel()
        {
            //Определение шаблона ItemsPanel
            FrameworkElementFactory factoryRadialPanel =
                new FrameworkElementFactory(typeof(RadialPanel));
            ItemsPanel = new ItemsPanelTemplate(factoryRadialPanel);
            //Создание объекта DataTemplate для вариантов
            DataTemplate template = new DataTemplate(typeof(Brush));
            ItemTemplate = template;

            //Создание объекта FrameworkElementFactory на базе Rectangle
            FrameworkElementFactory elRectangle =
                new FrameworkElementFactory(typeof(Rectangle));
            elRectangle.SetValue(Rectangle.WidthProperty, 4.0);
            elRectangle.SetValue(Rectangle.HeightProperty, 12.0);
            elRectangle.SetValue(Rectangle.MarginProperty, new Thickness(1, 8, 1, 8));
            elRectangle.SetBinding(Rectangle.FillProperty, new Binding(""));

            //Объект задается в качестве визуального дерева
            template.VisualTree = elRectangle;

            //Заполнение списка ListBox
            PropertyInfo[] props = typeof(Brushes).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                Items.Add((Brush)prop.GetValue(null, null));
            }
        }
    }
}
