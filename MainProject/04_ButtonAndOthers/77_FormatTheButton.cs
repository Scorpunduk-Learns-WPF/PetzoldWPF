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


namespace MainProject.ButtonAndOthers
{
    [PetzoldExampleProject(chapterNumber: 4, page: 77)]
    public class FormatTheButton : Window
    {
        Run runbutton;
        
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new FormatTheButton());
        }

        public FormatTheButton()
        {
            Title = "Format the Button";

            // button as window content creation
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.MouseEnter += ButtonOnMouseEnter;
            btn.MouseLeave += ButtonOnMouseLeave;
            Content = btn;

            //
            TextBlock txtblk = new TextBlock();
            txtblk.FontSize = 24;
            txtblk.TextAlignment = TextAlignment.Center;
            btn.Content = txtblk;

            // Добавление 
            txtblk.Inlines.Add(new Italic(new Run("Click ")));
            txtblk.Inlines.Add("the ");
            txtblk.Inlines.Add(runbutton = new Run("button "));
            txtblk.Inlines.Add(new LineBreak());
            txtblk.Inlines.Add("to launch the ");
            txtblk.Inlines.Add(new Bold(new Run("rocket")));
        }

        void ButtonOnMouseEnter(object sender, MouseEventArgs args)
        {
            runbutton.Foreground = Brushes.Red;
        }

        void ButtonOnMouseLeave(object sender, MouseEventArgs args)
        {
            runbutton.Foreground = SystemColors.ControlTextBrush;
        }
    }
}
