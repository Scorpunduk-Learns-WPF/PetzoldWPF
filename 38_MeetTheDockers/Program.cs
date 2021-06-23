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
using System.Windows.Controls.Primitives; // StatusBar
using System.Windows.Documents; // new Italic()
using System.ComponentModel;

namespace _38_MeetTheDockers
{
    // page 117
    class MeetTheDockers : Window
    {
        //WARNING - this programm do nothing
        // just example
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new MeetTheDockers());
        }

        public MeetTheDockers()
        {
            Title = "Meet the Dockers";

            DockPanel dock = new DockPanel();
            Content = dock;

            //создание меню
            Menu menu = new Menu();
            MenuItem item = new MenuItem();
            item.Header = "Menu";
            menu.Items.Add(item);

            //Размещение меню у верхнего края панели
            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            // Создание панели инструментов
            ToolBar tool = new ToolBar();
            tool.Header = "ToolBar";

            // Размещение панели инструментов у верхнего края
            DockPanel.SetDock(tool, Dock.Top);
            dock.Children.Add(tool);

            // Создание строки состояния
            StatusBar status = new StatusBar();
            StatusBarItem statItem = new StatusBarItem();
            statItem.Content = "Status";
            status.Items.Add(statItem);

            //Размещение строки состояния у нижнего края панели
            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            //создание списка
            ListBox lstBox = new ListBox();
            lstBox.Items.Add("List Box Item");

            //Размещение списка у левого края панели
            DockPanel.SetDock(lstBox, Dock.Left);
            dock.Children.Add(lstBox);

            // Создание текстовго поля
            TextBox txtbox = new TextBox();
            txtbox.AcceptsReturn = true;

            // Размещение текстового поля и передача фокуса
            dock.Children.Add(txtbox);
            txtbox.Focus();
        }
    }
}
