using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media; //fonts
using System.Windows.Input;
using System.ComponentModel;

namespace DisplaySomeText
{
    // page 55
    class DisplaySomeText : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DisplaySomeText());
        }

        public DisplaySomeText()
        {
            Title = "Display Some Text";
            Content = "Content can be simple text";

            FontFamily = new FontFamily("Comic Sans MS");
            FontSize = 48;
        }
    }
}
