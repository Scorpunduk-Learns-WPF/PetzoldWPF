using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MainProject._09_RoutedEvents
{
    [PetzoldExampleProject(chapterNumber: 9, page: 190)]
    public class ExamineKeyStrokes : Window
    {
        StackPanel stack;
        ScrollViewer scroll;
        string strHeader = "Event Key Sys-Key Text " +
            "Ctrl-Text Sys-Text Ime KeyStates " +
            "IsDown IsUp IsToggled IsRepeat ";
        string strFormatKey = "{0,-10}{1,-20}{2,-10} " +
            " {3,-10}{4,-15}{5,-8}{6,-7}{7,-10}{8,-10}";
        string strFormatText = "{0,-10} " + 
            "{1,-10}{2,-10}{3,-10}";

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ExamineKeyStrokes());
        }

        public ExamineKeyStrokes()
        {
            Title = "Examine KeyStrokes";
            FontFamily = new FontFamily("Courier New");

            Grid grid = new Grid();
            Content = grid;

            //Одна строка в режиме Auto
            //другая заполняет всё оставшееся место
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            grid.RowDefinitions.Add(new RowDefinition());

            //Вывод заголовка
            TextBlock textHeader = new TextBlock();
            textHeader.FontWeight = FontWeights.Bold;
            textHeader.Text = strHeader;
            grid.Children.Add(textHeader);

            //Создание StackPanel как дочернего объекта ScrollViewer
            //для отображения событий
            scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 1);
            stack = new StackPanel();
            scroll.Content = stack;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            DisplayKeyInfo(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            DisplayKeyInfo(e);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            string str = String.Format
                (
                strFormatText,
                e.RoutedEvent.Name,
                e.Text,
                e.ControlText,
                e.SystemText);
            DisplayInfo(str);
        }

        void DisplayKeyInfo(KeyEventArgs e)
        {
            string str = String.Format(
                strFormatKey,
                e.RoutedEvent.Name,
                e.Key,
                e.SystemKey,
                e.ImeProcessedKey,
                e.KeyStates,
                e.IsDown,
                e.IsUp,
                e.IsToggled,
                e.IsRepeat);
            DisplayInfo(str);
        }

        void DisplayInfo(string str)
        {
            TextBlock text = new TextBlock();
            text.Text = str;
            stack.Children.Add(text);
            scroll.ScrollToBottom();
        }
    }
}
