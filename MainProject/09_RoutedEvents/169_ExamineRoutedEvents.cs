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

namespace MainProject._09_RoutedEvents
{
    //[PetzoldExampleProject(chapterNumber: 9, page: 169)]
    public class ExamineRoutedEvents : Application
    {
        static readonly FontFamily fontfam = new FontFamily("Lucida Console");
        const string strFormat = "{0,-30} {1,-15} {2,-15} {3,-15}";
        StackPanel stackOutput;
        DateTime dtLast;

        [STAThread]
        public static void Main()
        {
            ExamineRoutedEvents app = new ExamineRoutedEvents();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Создание окна
            Window win = new Window();
            win.Title = "Examine Routed Events";

            //Создание объекта Grid и назначение его содержимым Window
            Grid grid = new Grid();
            win.Content = grid;

            //Определение трёх строк
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            grid.RowDefinitions.Add(rowdef);

            //Создание объекта Button и его размещение на панели Grid
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Padding = new Thickness(24);
            grid.Children.Add(btn);

            // Создание объекта TextBlock и его добавление к Button
            TextBlock text = new TextBlock();
            text.FontSize = 24;
            text.Text = win.Title;
            btn.Content = text;

            // Создание заголовков для вывода над ScrollViewver
            TextBlock textHeadings = new TextBlock();
            textHeadings.FontFamily = fontfam;
            textHeadings.Inlines.Add
                (new Underline(new Run(String.Format
                (strFormat, "Routed Event", "sender", "Source", "OriginalSource"))));
            grid.Children.Add(textHeadings);
            Grid.SetRow(textHeadings, 1);

            //Создание объекта ScrollViewer
            ScrollViewer scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 2);

            //Создаение объекта StackPanel для вызова событий
            stackOutput = new StackPanel();
            scroll.Content = stackOutput;

            //Добавление обработчиков событий
            UIElement[] els = { win, grid, btn, text };
            foreach (UIElement el in els)
            {
                //Клавиатура
                el.PreviewKeyDown += AllPurposeEventHandler;
                el.PreviewKeyUp += AllPurposeEventHandler;
                el.PreviewTextInput += AllPurposeEventHandler;
                el.KeyDown += AllPurposeEventHandler;
                el.KeyUp += AllPurposeEventHandler;
                el.TextInput += AllPurposeEventHandler;

                //Мышь
                el.MouseDown += AllPurposeEventHandler;
                el.MouseUp += AllPurposeEventHandler;
                el.PreviewMouseDown += AllPurposeEventHandler;
                el.PreviewMouseUp += AllPurposeEventHandler;

                //Стилус
                el.StylusDown += AllPurposeEventHandler;
                el.StylusUp += AllPurposeEventHandler; 
                el.PreviewStylusDown += AllPurposeEventHandler;
                el.PreviewStylusUp += AllPurposeEventHandler;

                //Щелчок
                el.AddHandler(Button.ClickEvent, new RoutedEventHandler(AllPurposeEventHandler));
            }
            //Отображение окна
            win.Show();
        }

        void AllPurposeEventHandler(object sender, RoutedEventArgs args)
        {
            //Вывод пустой строки, если события разделены промежутком
            DateTime dtNow = DateTime.Now;
            if (dtNow - dtLast > TimeSpan.FromMilliseconds(100))
            {
                stackOutput.Children.Add(new TextBlock(new Run(" ")));
            }
            dtLast = dtNow;

            //Вывод информации о событии
            TextBlock text = new TextBlock();
            text.FontFamily = fontfam;
            text.Text = String.Format(
                strFormat,
                args.RoutedEvent.Name,
                TypeWithoutNamespace(sender),
                TypeWithoutNamespace(args.Source),
                TypeWithoutNamespace(args.OriginalSource));
            stackOutput.Children.Add(text);
            (stackOutput.Parent as ScrollViewer).ScrollToBottom();
        }

        string TypeWithoutNamespace(object obj)
        {
            string[] astr = obj.GetType().ToString().Split('.');
            return astr[astr.Length - 1];
        }    
    }
}
