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

namespace MainProject._14_MenuHierarchy
{
    [PetzoldExampleProject(chapterNumber: 14, page: 305)]
    public class CheckTheColor : Window
    {
        TextBlock text;
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheColor());
        }

        public CheckTheColor()
        {
            Title = "Check the Color";
            //Создание объекта DockPanel
            DockPanel dock = new DockPanel();
            Content = dock;

            //Создание меню,  пристыкованного у верхнего края окна
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            //Создание объекта TextBlock, заполняющего оставшуюся площадь
            text = new TextBlock();
            text.Text = Title;
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 32;
            text.Background = SystemColors.WindowBrush;
            text.Foreground = SystemColors.WindowTextBrush;
            dock.Children.Add(text);

            //Создание команд меню
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);
            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemForeground.SubmenuOpened += ForegroundOnOpened;
            itemColor.Items.Add(itemForeground);
            FillWithColors(itemForeground, ForegroundOnClick);
            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemBackground.SubmenuOpened += BackgroundOnOpened;
            itemColor.Items.Add(itemBackground);
            FillWithColors(itemBackground, BackgroundOnClick);
        }

        void FillWithColors(MenuItem itemParent, RoutedEventHandler handler)
        {
            foreach(PropertyInfo prop in typeof(Colors).GetProperties())
            {
                Color clr = (Color)prop.GetValue(null, null);
                int iCount = 0;
                iCount += clr.R == 0 || clr.R == 255 ? 1 : 0;
                iCount += clr.G == 0 || clr.G == 255 ? 1 : 0;
                iCount += clr.B == 0 || clr.B == 255 ? 1 : 0;

                if(clr.A == 255 && iCount > 1)
                {
                    MenuItem item = new MenuItem();
                    item.Header = "_" + prop.Name;
                    item.Tag = clr;
                    item.Click += handler;
                    itemParent.Items.Add(item);
                }
            }
        }

        private void ForegroundOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem itemParent = sender as MenuItem;
            foreach (MenuItem item in itemParent.Items)
            {
                item.IsChecked = ((text.Foreground as SolidColorBrush).Color == (Color)item.Tag);
            }
        }

        private void BackgroundOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem itemParent = sender as MenuItem;
            foreach(MenuItem item in itemParent.Items)
            {
                item.IsChecked = ((text.Background as SolidColorBrush).Color == (Color)item.Tag);
            }
        }

        private void ForegroundOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Foreground = new SolidColorBrush(clr);
        }

        private void BackgroundOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Background = new SolidColorBrush(clr);
        }
    }
}