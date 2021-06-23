using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;


namespace _03_ThrowWindowParty
{
    class ThrowWindowParty: Application
    {
        [STAThread]
        static void Main()
        {
            ThrowWindowParty app = new ThrowWindowParty();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose; // all windows closed when MainWindow closed
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window winMain = new Window();
            winMain.Title = "MainWindow";
            winMain.MouseDown += WindowOnMouseDown;            
            winMain.Show();

            for (int i=0; i < 2; i++)
            {
                Window win = new Window();
                win.Owner = winMain;
                win.ShowInTaskbar = false; // hide extra windows in taskbar
                win.Title = $"Extra Window No. {i + 1}";
                win.Show();

            }
      
        }

        void WindowOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            Window win = new Window();
            win.ShowInTaskbar = false; // hide extra windows in taskbar
            win.Title = "Modal Dialog Box";
            win.ShowDialog();
        }

    }
}
