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


namespace _22_ClickTheButton
{
    // page 73
    class ClickTheButton : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ClickTheButton());            
        }

        public ClickTheButton()
        {
            Title = "Click the Button";

            Button btn = new Button();
            btn.Content = "_Click me, please!";
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The button has been clicked and all is well.", Title);
        }
    }
}
