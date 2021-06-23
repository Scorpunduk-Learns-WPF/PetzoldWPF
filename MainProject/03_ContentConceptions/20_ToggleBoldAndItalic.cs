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

namespace MainProject.ContentConceptions
{
    [PetzoldExampleProject(chapterNumber: 3, page: 68)]
    public class ToggleBoldAndItalic : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ToggleBoldAndItalic());
        }

        public ToggleBoldAndItalic()
        {
            Title = "Toggle Bold and Italic ";
            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            string strQuote = "To be or not to be, that the question";
            string[] strWords = strQuote.Split();

            foreach (string str in strWords)
            {
                Run run = new Run(str);
                run.MouseDown += RunOnMouseDown;
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }
        }

        void RunOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Run run = sender as Run;

            if (e.ChangedButton == MouseButton.Left)
            {
                run.FontStyle = run.FontStyle == FontStyles.Italic ? FontStyles.Normal : FontStyles.Italic;
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                run.FontWeight = run.FontWeight == FontWeights.Bold ? FontWeights.Normal : FontWeights.Bold;
            }
        }
    }
}
