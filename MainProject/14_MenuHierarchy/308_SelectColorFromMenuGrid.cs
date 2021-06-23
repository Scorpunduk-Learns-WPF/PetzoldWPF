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

using MainProject._13_ListBox;

namespace MainProject._14_MenuHierarchy
{
    [PetzoldExampleProject(chapterNumber: 14, page: 308)]
    public class SelectColorFromMenuGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromMenuGrid());
        }

        public SelectColorFromMenuGrid()
        {
            Title = "Select Color from Menu Grid";

            //Создание объекта DockPanel
            DockPanel dock = new DockPanel();
            Content = dock;

            //Создание меню, пристыкованного у верхнего края окна
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            //Создание объекта TextBlock,заполняющего оставшуюся площадь
            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            //Включение команд в меню
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);
            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemColor.Items.Add(itemForeground);

            //Создание объекта ColorGridBox и его привязка 
            //к свойству Foreground окна
            ColorGridBox clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Foreground");
            clrbox.DataContext = this;
            itemForeground.Items.Add(clrbox);
            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemColor.Items.Add(itemBackground);

            //Создание объекта ColorGridBox и его привязка
            // к свойству Background окна
            clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrbox.DataContext = this;
            itemBackground.Items.Add(clrbox);
        }
    }
}
