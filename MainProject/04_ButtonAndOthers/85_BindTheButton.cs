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
using System.Windows.Controls.Primitives;
using System.Windows.Documents; // new Italic()
using System.ComponentModel;


namespace MainProject.ButtonAndOthers
{
    [PetzoldExampleProject(chapterNumber: 4, page: 85)]
    public class BindTheButton : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new BindTheButton());
        }

        public BindTheButton()
        {
            Title = "Bind the Button";

            ToggleButton btn = new ToggleButton();
            btn.Content = "Make _Topmost";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.SetBinding(ToggleButton.IsCheckedProperty, "Topmost");
            btn.DataContext = this;
            Content = btn;

            ToolTip tip = new ToolTip();
            tip.Content = "Toggle the button on to make" + "the window topmost on the desktop";
            btn.ToolTip = tip;
        }
    }
}
