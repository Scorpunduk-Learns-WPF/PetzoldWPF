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


namespace _33_ScrollFiftyButtons
{
    // page 103
    class ScrollFiftyButtons : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ScrollFiftyButtons());
        }

        public ScrollFiftyButtons()
        {
            Title = "Scroll Fifty Buttons";
            SizeToContent = SizeToContent.Width;
            AddHandler(
                Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));
            ScrollViewer scroll = new ScrollViewer();
            Content = scroll;
            StackPanel stack = new StackPanel();
            stack.Margin = new Thickness(5);
            scroll.Content = stack;

            for(int i = 0; i < 50; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + (i + 1);
                btn.Content = btn.Name + " says 'Click me'";
                btn.Margin = new Thickness(5);
                stack.Children.Add(btn);
            }
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            if(btn != null)
            {
                MessageBox.Show(btn.Name + " has been clicked", "Button Click");
            }
        }
    }
}
