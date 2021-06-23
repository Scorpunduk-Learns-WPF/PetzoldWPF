using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace MainProject.Intro
{ 
    class InheritWindow : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new InheritWindow());
        }

        public InheritWindow()
        {
            Title = "Inherit the win";
        }
    }
}
