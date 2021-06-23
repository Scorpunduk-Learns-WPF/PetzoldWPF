using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents; // new Italic()
using System.ComponentModel;


namespace MainProject.StackAndWrapPanels
{
    [PetzoldExampleProject(chapterNumber: 5, page: 97)]
    public class StackTenButtons : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new StackTenButtons());
        }

        public StackTenButtons()
        {
            Title = "Stack Ten Buttons";

            Width = 300;
            Height = 500;

            StackPanel stack = new StackPanel();
            Content = stack;
            Random rand = new Random();

            for(int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                btn.Name = (((char)('A' + i)).ToString());
                btn.FontSize += rand.Next(10);
                btn.Content = "Button " + btn.Name + " says 'Click me'";
                btn.Click += ButtonOnClick;
                stack.Children.Add(btn);
                btn.Margin = new Thickness(5);
            }
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            MessageBox.Show("Button " + btn.Name + " has been clicked", "Button Click");
        }
    }
}
