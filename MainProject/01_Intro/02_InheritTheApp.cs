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
    public class InheritApp : Application
    {
        [STAThread]
        public static void Main()
        {
            InheritApp app = new InheritApp();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window win = new Window();
            win.Title = "Inherit the App";
            win.Closing += WinClose;
            win.Show();
        }

        // WARNING!!! - this method will only triggers on OS log out or shutdown, not on program exit
        // https://stackoverflow.com/questions/38190138/why-isnt-the-onsessionending-event-triggering-on-program-exit
        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);

            MessageBoxResult result =
                MessageBox.Show(
                    "Do you want to sabe your data?",
                    MainWindow.Title,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes);
            e.Cancel = false;
        }

        // this will works if MainWindow is closing
        // event was subscribed in OnStartUp method
        public void WinClose(object sender, CancelEventArgs args )
            //CancelEventArgs - using System.ComponentModel
        {
            MessageBoxResult result =
                MessageBox.Show(
                    "Do you want to save your data?",
                    MainWindow.Title,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes);

            args.Cancel = (result == MessageBoxResult.Cancel);
        }
    }
}
