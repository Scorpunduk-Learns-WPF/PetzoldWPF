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
    [PetzoldExampleProject(chapterNumber: 15, page: 330)]
    public class MoveTheToolBar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MoveTheToolBar());
        }

        public MoveTheToolBar()
        {
            Title = "Move the Toolbar";

            //Создание объекта  DockPanel как содержимого окна
            DockPanel dock = new DockPanel();
            Content = dock;

            // Создание областей ToolBarTray у левого и верхнего края
            ToolBarTray trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);
            ToolBarTray trayLeft = new ToolBarTray();
            trayLeft.Orientation = Orientation.Vertical;
            dock.Children.Add(trayLeft);
            DockPanel.SetDock(trayLeft, Dock.Left);

            // Создание объекта TextBox, заполняющего остальную площадь
            // клиентской области
            TextBox txtBox = new TextBox();
            dock.Children.Add(txtBox);

            // Создание 6 панелей инструментов
            for (int i = 0; i < 6; i++)
            {
                ToolBar toolBar = new ToolBar();
                toolBar.Header = "Toolbar" + (i + 1);
                if (i < 3) trayTop.ToolBars.Add(toolBar);
                else trayLeft.ToolBars.Add(toolBar);

                // ... и шести кнопок на каждой панели
                for (int j = 0; j < 6; j++)
                {
                    Button btn = new Button();
                    btn.FontSize = 16;
                    btn.Content = (char)('A' + j);
                    toolBar.Items.Add(btn);
                }
            }
        }
    }
}
