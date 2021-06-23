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

namespace MainProject._14_MenuHierarchy
{
    [PetzoldExampleProject(chapterNumber: 14, page: 303)]
    public class CheckTheWindowStyle : Window
    {
        MenuItem itemChecked;
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheWindowStyle());
        }

        public CheckTheWindowStyle()
        {
            Title = "Check the Window Style";

            //Создание объекта DockPanel
            DockPanel dock = new DockPanel();
            Content = dock;

            //Создание меню, пристыкованного у верхнего края окна
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            //Создание объекта TextBlock, заполняющего оставшуюся площадь
            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            //Создание объектов MenuItem для изменения WindowStyle
            MenuItem itemStyle = new MenuItem();
            itemStyle.Header = "_Style";
            menu.Items.Add(itemStyle);
            itemStyle.Items.Add(CreateMenuItem("_No border or caption", WindowStyle.None));
            itemStyle.Items.Add(CreateMenuItem("_Single border window", WindowStyle.SingleBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("3_D-border window", WindowStyle.ThreeDBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("_Tool window", WindowStyle.ToolWindow));
        }

        private MenuItem CreateMenuItem (string str, WindowStyle style)
        {
            MenuItem item = new MenuItem();
            item.Header = str;
            item.Tag = style;
            item.IsChecked = (style == WindowStyle);
            item.Click += StyleOnClick;
            if (item.IsChecked) { itemChecked = item; }
            return item;
        }

        private void StyleOnClick(object sender, RoutedEventArgs args)
        {
            itemChecked.IsChecked = false;
            itemChecked = args.Source as MenuItem;
            itemChecked.IsChecked = true;
            WindowStyle = (WindowStyle)itemChecked.Tag;
        }
    }
}
