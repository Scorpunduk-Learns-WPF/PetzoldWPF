using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace _06_GrowAndShrink
{
    // page 29
    class GrowWindow : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();

            app.Run(new GrowWindow());
        }

        public GrowWindow()
        {
            Title = "Grow and Shrink";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 300;
            Height = 300;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(e.Key == Key.Up)
            {
                Left -= 0.05 * Width;
                Top -= 0.05 * Height;

                Width *= 1.1;
                Height *= 1.1;
            }
            else if (e.Key == Key.Down)
            {
                Left += 0.05 * (Width /= 1.1);
                Top += 0.05 * (Height /= 1.1);
            }
        }

    }
}
