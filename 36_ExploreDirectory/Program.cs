using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics; // для класса Process
using System.IO;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents; // new Italic()
using System.ComponentModel;


namespace _36_ExploreDirectories
{
    // page 110
    class ExploreDirectories : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ExploreDirectories());
        }

        public ExploreDirectories()
        {
            Title = "Explore Directories";

            ScrollViewer scroll = new ScrollViewer();
            Content = scroll;

            WrapPanel wrap = new WrapPanel();
            scroll.Content = wrap;

            wrap.Children.Add(new FileSystemInfoButton());
        }
    }

    public class FileSystemInfoButton : Button
    {
        FileSystemInfo info;

        // Непараметризованный конструктор 
        // создаёт кнопку для каталога "Мои документы"
        public FileSystemInfoButton()
            : this (new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
        {

        }

        // Конструктор с одним аргументом создаёт кнопку
        // родительского каталога
        public FileSystemInfoButton(FileSystemInfo info)
        {
            this.info = info;
            Content = info.Name;
            if (info is DirectoryInfo)
            {
                FontWeight = FontWeights.Bold;
            }
            Margin = new Thickness(10);
        }

        // Конструктор с двумя аргументами создаёт кнопку
        // родительского каталога
        public FileSystemInfoButton(FileSystemInfo info, string str)
            : this(info)
        {
            Content = str;
        }

        // Переопределение OnClick делает всё остальное
        protected override void OnClick()
        {
            if (info is FileInfo)
            {
                Process.Start(info.FullName);
            }
            else if(info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;
                Application.Current.MainWindow.Title = dir.FullName;

                Panel pnl = Parent as Panel;
                pnl.Children.Clear();

                if(dir.Parent != null)
                {
                    pnl.Children.Add(new FileSystemInfoButton(dir.Parent, ".."));
                }

                foreach (FileSystemInfo inf in dir.GetFileSystemInfos())
                {
                    pnl.Children.Add(new FileSystemInfoButton(inf));
                }
            }

            base.OnClick();
        }
    }
}
