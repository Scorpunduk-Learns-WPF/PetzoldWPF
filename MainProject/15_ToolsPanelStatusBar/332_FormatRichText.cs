using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MainProject._15_ToolsPanelStatusBar
{
    [PetzoldExampleProject(chapterNumber: 15, page: 332)]
    public partial class FormatRichText : Window
    {
        RichTextBox txtbox;
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FormatRichText());
        }


        public FormatRichText()
        {
            Title = "Format Rich Text";
            // Создание объекта DockPanel как содержимого окна
            DockPanel dock = new DockPanel();
            Content = dock;

            // Создание области ToolBarTray у верхнего края окна
            ToolBarTray tray = new ToolBarTray();
            dock.Children.Add(tray);
            DockPanel.SetDock(tray, Dock.Top);

            // Создание объекта RichTextBox
            txtbox = new RichTextBox();
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            // Вызов методов из других файлов
            AddFileToolBar(tray, 0, 0);
            AddEditToolBar(tray, 1, 0);
            AddCharToolBar(tray, 2, 0);
            AddParaToolBar(tray, 2, 1);
            AddStatusBar(dock);

            // Создание объекта RichTextBox, заполняющего 
            // остальную площадь, и передача ему фокуса
            dock.Children.Add(txtbox);
            txtbox.Focus();
        }

    }
}
