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

namespace MainProject._13_ListBox
{
    [PetzoldExampleProject(chapterNumber: 13, page: 278)]
    public class ListColorShapes : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorShapes());
        }

        public ListColorShapes()
        {
            Title = "List Color Shapes";
            //Создание объекта ListBox как содержимого окна
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            //Заполнение ListBox объектами Ellipse
            PropertyInfo[] props = typeof(Brushes).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 100;
                ellipse.Height = 25;
                ellipse.Margin = new Thickness(10, 5, 0, 5);
                ellipse.Fill = prop.GetValue(null, null) as Brush;
                lstbox.Items.Add(ellipse);
            }
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            if(lstbox.SelectedIndex != -1)
            {
                Background = (lstbox.SelectedItem as Shape).Fill;
            }
        }
    }
}
