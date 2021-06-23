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

namespace MainProject._13_ListBox
{
    [PetzoldExampleProject(chapterNumber: 13, page: 282)]
    public class ListWithListBoxItems : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListWithListBoxItems());
        }

        public ListWithListBoxItems()
        {
            Title = "List with ListBoxItem";
            //Создание объекта ListBox как содержимого окна
            ListBox lstbox = new ListBox();
            lstbox.Height = 150;
            lstbox.Width = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            //Заполнение списка объектами ListBoxItem
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                Color clr = (Color)prop.GetValue(null, null);
                bool isBlack = 0.222 * clr.R + 0.707 * clr.G + 0.071 * clr.B > 128;

                ListBoxItem item = new ListBoxItem();
                item.Content = prop.Name;
                item.Background = new SolidColorBrush(clr);
                item.Foreground = isBlack ? Brushes.Black : Brushes.White;
                item.HorizontalContentAlignment = HorizontalAlignment.Center;
                item.Padding = new Thickness(2);
                lstbox.Items.Add(item);
            }
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            ListBoxItem item;
            if (args.RemovedItems.Count > 0)
            {
                item = args.RemovedItems[0] as ListBoxItem;
                String str = item.Content as string;
                item.Content = str.Substring(2, str.Length - 4);
                item.FontWeight = FontWeights.Regular;
            }
            if (args.AddedItems.Count > 0)
            {
                item = args.AddedItems[0] as ListBoxItem;
                String str = item.Content as String;
                item.Content = "[" + str + "]";
                item.FontWeight = FontWeights.Bold;
            }
            item = lstbox.SelectedItem as ListBoxItem;
            if (item != null)
                Background = item.Background;
        }
    }
}
