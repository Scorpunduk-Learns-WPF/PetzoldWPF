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

namespace MainProject._12_UserPanels
{
    [PetzoldExampleProject(chapterNumber: 12, page: 246)]
    public class DuplicateUniformGrid : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DuplicateUniformGrid());
        }

        public DuplicateUniformGrid()
        {
            Title = "Duplicate Uniform Grid";

            //Создание объекта UniformGridAlmost как содержимого окна
            UniformGridAlmost unigrid = new UniformGridAlmost();
            unigrid.Columns = 5;
            Content = unigrid;

            //Заполнение UniformGridAlmost как содержимого окна
            Random rand = new Random();
            for(int index = 0; index < 48; index++)
            {
                Button btn = new Button();
                btn.Name = "Button" + index;
                btn.Content = btn.Name;
                btn.FontSize += rand.Next(10);
                unigrid.Children.Add(btn);
            }

            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            MessageBox.Show(btn.Name + " has been clicked", Title);
        }
    }
}
