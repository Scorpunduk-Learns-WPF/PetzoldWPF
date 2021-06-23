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
    [PetzoldExampleProject(chapterNumber: 3, page: 66)]
    public class FormatTheText : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new FormatTheText());
        }

        public FormatTheText()
        {
            Title = "Format the Text";
            TextBlock txt = new TextBlock();

            txt.FontSize = 32;
            txt.Inlines.Add("This is some");
            txt.Inlines.Add(new Italic(new Run("italic"))); // System.Windows.Document
            txt.Inlines.Add("text, and this is some");
            txt.Inlines.Add(new Bold(new Run("bold")));
            txt.Inlines.Add("text, and let's cap it off with some");
            txt.Inlines.Add(new Bold(new Italic(new Run("bold italic"))));
            txt.Inlines.Add(" text");
            txt.TextWrapping = TextWrapping.Wrap;

            Content = txt;
        }
    }
}
