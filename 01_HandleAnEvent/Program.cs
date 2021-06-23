using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;

namespace _01_HandleAnEvent
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // PresentationFramework Assembly
            Application app = new Application();
            Window win = new Window();

            win.Title = "Handle an Event";

            // event MouseDown defined in PresentationCore Assembly
            win.MouseDown += 
                (object sender, MouseButtonEventArgs arg) => 
                {
                    Window w = sender as Window;
                    string msg =
                    //arg.GetPosition - need to add ref to System.Xaml assebmly
                    //for access to interface IQuaryAmbient - don't know where it needs
                    // maybe the reason is this fact - Window class ancestor - FrameworkElement 
                    // - derived from several interfaces and classes
                    // One of them - IInputElement, another - IQuaryAmbient
                    $"Window clicked with {arg.ChangedButton} button at point {arg.GetPosition(w)}";
                    MessageBox.Show(msg, w.Title);
                };

            // method Run defined in WindowsBase Assembly - Application derived 
            // from DispathcerObject - this class defined in WindowsBase
            app.Run(win);

        }
    }

    // Assemblies for WPF
    // PresentationFramework
    // PresentationCore
    // SystemXaml
    // WindowsBase
    // System.Windows
}
