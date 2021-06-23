using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MainProject._14_MenuHierarchy
{
    [PetzoldExampleProject(chapterNumber: 14, page: 310)]
    public class CutCopyAndPaste : Window
    {
        TextBlock text;
        protected MenuItem itemCut, itemCopy, itemPaste, itemDelete;
        //****************************//
        //**********SET URI***********//
        //****************************//
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CutCopyAndPaste());
        }

        public CutCopyAndPaste()
        {
            Title = "Cut, Copy, and Paste";

            //Создание объекта DockPanel
            DockPanel dock = new DockPanel();
            Content = dock;

            //Создание меню, пристыкованного у верхнего края окна
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            //Создание объекта TextBlock, заполняющего оставшуюся площадь
            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32;
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            //Создание меню Edit
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            itemEdit.SubmenuOpened += EditOnOpened;
            menu.Items.Add(itemEdit);

            //Создание команд меню Edit
            itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Click += CutOnClick;
            Image img = new Image();
            //img.Source = new BitmapImage(new Uri());
            itemCut.Icon = img;
            itemEdit.Items.Add(itemCut);

            itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Click += CopyOnClick;
            img = new Image();
            //img.Source = new BitmapImage(new Uri());
            itemCopy.Icon = img;
            itemEdit.Items.Add(itemCopy);

            itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Click += PasteOnClick;
            img = new Image();
            //img.Source = new BitmapImage(new Uri());
            itemPaste.Icon = img;
            itemEdit.Items.Add(itemPaste);

            itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Click += DeleteOnClick;
            img = new Image();
            //img.Source = new BitmapImage(new Uri());
            itemDelete.Icon = img;
            itemEdit.Items.Add(itemDelete);
        }

        private void EditOnOpened(object sender, RoutedEventArgs args)
        {
            itemCut.IsEnabled =
            itemCopy.IsEnabled =
            itemDelete.IsEnabled = text.Text != null && text.Text.Length > 0;
            itemPaste.IsEnabled = Clipboard.ContainsText();
        }

        protected void CutOnClick(object sender, RoutedEventArgs args)
        {
            CopyOnClick(sender, args);
            DeleteOnClick(sender, args);
        }

        protected void CopyOnClick(object sender, RoutedEventArgs args)
        {
            if(text.Text != null && text.Text.Length > 0)
            {
                Clipboard.SetText(text.Text);
            }
        }

        protected void PasteOnClick(object sender, RoutedEventArgs args)
        {
            if (Clipboard.ContainsText())
            {
                text.Text = Clipboard.GetText();
            }
        }

        protected void DeleteOnClick(object sender, RoutedEventArgs args)
        {
            text.Text = null;
        }
    }
}
