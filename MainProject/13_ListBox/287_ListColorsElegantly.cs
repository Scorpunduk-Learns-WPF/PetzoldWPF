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
    [PetzoldExampleProject(chapterNumber: 13, page: 287)]
    public class ListColorsElegantly : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsElegantly());
        }

        public ListColorsElegantly()
        {
            Title = "List Colors Elegantly";
            ColorListBox lstbox = new ColorListBox();
            lstbox.Height = 150;
            lstbox.Width = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            //Инициализация SelectedColor
            lstbox.SelectedColor = SystemColors.WindowColor;
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ColorListBox lstbox = sender as ColorListBox;
            Background = new SolidColorBrush(lstbox.SelectedColor);
        }
    }
}
