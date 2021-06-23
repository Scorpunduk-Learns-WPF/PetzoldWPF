using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace _04_InheritAppAndWindow
{
    class InheritAppAndWindow
    {
        [STAThread]
        static void Main(string[] args)
        {
            MyApplication myApp = new MyApplication();
            myApp.Run();

        }
    }

    class MyApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MyWindow myWin = new MyWindow();
            myWin.Show();
        }
    }

    class MyWindow : Window
    {
        public MyWindow()
        {
            Title = "Inherit App & Window";
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            string msg =
                $"Window clicked with {e.ChangedButton} button at point {e.GetPosition(this)}";

            MessageBox.Show(msg, Title);
        }
    }

}
