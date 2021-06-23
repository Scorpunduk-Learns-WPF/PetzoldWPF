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


namespace _32_StackThirtyButtons
{
    // page 102
    class StackThirtyButtons : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new StackThirtyButtons());
        }

        public StackThirtyButtons()
        {
            Title = "Stack Thirty Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            StackPanel stackMain = new StackPanel();
            stackMain.Orientation = Orientation.Horizontal;
            stackMain.Margin = new Thickness(5);
            Content = stackMain;

            for (int i = 0; i < 3; i++)
            {
                StackPanel stackChild = new StackPanel();
                stackMain.Children.Add(stackChild);

                for (int j = 0; j < 10; j++)
                {
                    Button btn = new Button();
                    btn.Content = "Button No. " + (10 * i + j + 1);
                    btn.Margin = new Thickness(5);
                    stackChild.Children.Add(btn);
                }
            }
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("You clicked the button labeled " + (args.Source as Button).Content);
        }
    }
}
