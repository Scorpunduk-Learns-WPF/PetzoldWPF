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


namespace MainProject.ButtonAndOthers
{
    [PetzoldExampleProject(chapterNumber: 4, page: 87)]
    public class NavigateTheWeb : Window
    {
        Frame frm;

        // при попытке ввести существующий адрес будет выдано сообщение
        // о том, что браузер устарел и предложение выбрать приложение либо другой браузер
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the Web";

            frm = new Frame();
            Content = frm;

            Loaded += OnWindowLoaded;
        }

        void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            UriDialog dlg = new UriDialog();
            dlg.Owner = this;
            dlg.Text = "http://";
            dlg.ShowDialog();

            try
            {
                frm.Source = new Uri(dlg.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }
        }
    }

    class UriDialog : Window
    {
        TextBox txtBox;

        public UriDialog()
        {
            Title = "Enter a URI";
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            txtBox = new TextBox();
            txtBox.Margin = new Thickness(48);
            Content = txtBox;

            txtBox.Focus();
        }

        public string Text
        {
            set
            {
                txtBox.Text = value;
                txtBox.SelectionStart = txtBox.Text.Length;
            }

            get
            {
                return txtBox.Text;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }
    }
}
