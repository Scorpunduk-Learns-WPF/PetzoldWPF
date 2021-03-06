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


namespace _35_TuneTheRadio
{
    //  page 108
    class TuneTheRadio : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new TuneTheRadio());
        }

        public TuneTheRadio()
        {
            Title = "Tune the Radio";
            SizeToContent = SizeToContent.WidthAndHeight;

            GroupBox group = new GroupBox();
            group.Header = "Window Style";
            group.Margin = new Thickness(96);
            group.Padding = new Thickness(5);
            Content = group;

            StackPanel stack = new StackPanel();
            group.Content = stack;

            stack.Children.Add(CreateRadioButton("No border or caption", WindowStyle.None));
            stack.Children.Add(CreateRadioButton("Single border window", WindowStyle.SingleBorderWindow));
            stack.Children.Add(CreateRadioButton("3D border window", WindowStyle.ThreeDBorderWindow));
            stack.Children.Add(CreateRadioButton("Tool window", WindowStyle.ToolWindow));

            AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioOnChecked));
        }

        RadioButton CreateRadioButton (string strText, WindowStyle winstyle)
        {
            RadioButton radio = new RadioButton();
            radio.Content = strText;
            radio.Tag = winstyle;
            radio.Margin = new Thickness(5);
            radio.IsChecked = (winstyle == WindowStyle);
            return radio;
        }

        void RadioOnChecked(object sender, RoutedEventArgs args)
        {
            RadioButton radio = args.Source as RadioButton;
            WindowStyle = (WindowStyle)radio.Tag;
        }
    }
}
