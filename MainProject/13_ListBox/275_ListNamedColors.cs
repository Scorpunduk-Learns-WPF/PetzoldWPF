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
    [PetzoldExampleProject(chapterNumber: 13, page: 275)]
    public class ListNamedColors : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListNamedColors());
        }

        public ListNamedColors()
        {
            Title = "ListNamedColors";
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            //Заполнение списка и задание свойства
            lstbox.ItemsSource = NamedColor.All;
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Color";
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            if (lstbox.SelectedValue != null)
            {
                Color clr = (Color)lstbox.SelectedValue;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
