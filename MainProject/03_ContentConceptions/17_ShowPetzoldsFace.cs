using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media; //fonts
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace MainProject.ContentConceptions
{
    [PetzoldExampleProject(chapterNumber: 3, page: 60)]
    public class ShowPetzoldsFace : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ShowPetzoldsFace());
        }

        public ShowPetzoldsFace()
        {
            Title = "The Great Petzold";
            Uri uri = new Uri("http://www.charlespetzold.com/PetzoldTattoo.jpg");
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;
            Content = img;
        }
    }
}
