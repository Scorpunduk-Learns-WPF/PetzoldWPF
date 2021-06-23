using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Shapes;
using System.Windows.Data; // Binding

namespace MainProject._13_ListBox
{
    [PetzoldExampleProject(chapterNumber: 13, page: 288)]
    public class ListColorsEvenElegantier : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsEvenElegantier());
        }

        public ListColorsEvenElegantier()
        {
            Title = "List Colors Even Elegantier";
            //Создание шаблона DataTemplate для вариантов
            DataTemplate template = new DataTemplate(typeof(NamedBrush));

            //создание объекта FrameworkElementFactory для StackPanel
            FrameworkElementFactory factoryStack =
                new FrameworkElementFactory(typeof(StackPanel));
            factoryStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            //Назначение объекта корнем визуального дерева DataTemplate
            template.VisualTree = factoryStack;

            //создание  объекта FrameworkElementFactory для Rectangle
            FrameworkElementFactory factoryRectangle =
                new FrameworkElementFactory(typeof(Rectangle));
            factoryRectangle.SetValue(Rectangle.WidthProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.HeightProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.MarginProperty, new Thickness(2));
            factoryRectangle.SetValue(Rectangle.StrokeProperty, SystemColors.WindowTextBrush);
            factoryRectangle.SetBinding(Rectangle.FillProperty, new Binding("Brush"));

            //Присоединение к StackPanel
            factoryStack.AppendChild(factoryRectangle);

            //создание объекта FrameworkElementFactory для TextBlock
            FrameworkElementFactory factoryTextBlock =
                new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            factoryTextBlock.SetValue(TextBlock.TextProperty, new Binding("Name"));

            //Присоединение к StackPanel
            factoryStack.AppendChild(factoryTextBlock);

            //Создание объекта ListBox как содержимого окна
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            Content = lstbox;

            //Свойству ItemSource задаётся массив объектов NamedBrush
            lstbox.ItemsSource = NamedBrush.All;

            //SelectedValue привязывается к свойству Background окна
            lstbox.SelectedValuePath = "Brush";
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;
        }
    }
}
