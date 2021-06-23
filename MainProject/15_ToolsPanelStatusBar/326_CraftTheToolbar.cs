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
    [PetzoldExampleProject(chapterNumber: 15, page: 326)]
    public class CraftTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CraftTheToolbar());
        }

        public CraftTheToolbar()
        {
            Title = "Craft the Toolbar";
            RoutedUICommand[] comm =
            {
                ApplicationCommands.New, ApplicationCommands.Open,
                ApplicationCommands.Save, ApplicationCommands.Print,
                ApplicationCommands.Cut, ApplicationCommands.Copy,
                ApplicationCommands.Paste, ApplicationCommands.Delete
            };
            string[] strImages =
            {
                "NewDocumentHS.png", "opneHS.png", "saveHS.png",
                "PrintHS.png", "CutHS.png", "CopyHS.png",
                "PasteHS.png", "DeleteHS.png"
            };

            //Создание объекта DockPanel как содержимого окна
            DockPanel dock = new DockPanel();
            dock.LastChildFill = false;
            Content = dock;

            // Создание панели инструментов, пристыкованной 
            // у верхнего края окна
            ToolBar toolBar = new ToolBar();
            dock.Children.Add(toolBar);
            DockPanel.SetDock(toolBar, Dock.Top);

            //Создание кнопок панели инструментов
            for (int i = 0; i < 8; i++)
            {
                if(i == 4) { toolBar.Items.Add(new Separator()); }

                //Создание объекта Button
                Button btn = new Button();
                btn.Command = comm[i];
                toolBar.Items.Add(btn);

                //Создание объекта Image как содержимого Button
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(""));
                img.Stretch = Stretch.None;
                btn.Content = img;

                //Создание объекта ToolTip на основе текста UICommand
                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;

                //Включение UICommand в привязки команд окна
                CommandBindings.Add(new CommandBinding(comm[i], ToolBarButtonOnClick));
            }
        }

        // Фиктивный обработчик команды для кнопки
        void ToolBarButtonOnClick(object sender, ExecutedRoutedEventArgs args)
        {
            RoutedUICommand comm = args.Command as RoutedUICommand;
            MessageBox.Show(comm.Name + " command not yet implemented", Title);
        }
    }
}
