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
using System.Windows.Controls.Primitives; // ToggleButton
using System.Windows.Documents; // new Italic()
using System.ComponentModel;


namespace _26_ToggleTheButton
{
    // page 83
    class ToggleTheButton : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ToggleTheButton());
        }

        public ToggleTheButton()
        {
            Title = "Toggle the Button";
            ToggleButton btn = new ToggleButton();
            btn.Content = "Can Resize";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.IsChecked = (ResizeMode == ResizeMode.CanResize);
            btn.Checked += ButtonOnChecked;
            btn.Unchecked += ButtonOnChecked;
            Content = btn;
        }

        void ButtonOnChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = sender as ToggleButton;
            ResizeMode = (bool)btn.IsChecked ?  ResizeMode.CanResize : ResizeMode.NoResize;
            btn.Content = (bool)btn.IsChecked ? "Can Resize" : "Can't Resize";
        }
    }
}
