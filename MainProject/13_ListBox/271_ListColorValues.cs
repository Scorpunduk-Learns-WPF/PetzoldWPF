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
    [PetzoldExampleProject(chapterNumber: 13, page: 271)]
    public class ListColorValues : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorValues());
        }

        public ListColorValues()
        {
            Title = "List Color Values";
            //Создание объекта ListBox как содержимого окна
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            //Заполнение ListBox объектами Color
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                lstbox.Items.Add(prop.GetValue(null, null));
            }
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            if(lstbox.SelectedIndex != -1)
            {
                Color clr = (Color)lstbox.SelectedItem;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
