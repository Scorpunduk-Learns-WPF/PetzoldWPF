using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

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
    [PetzoldExampleProject(chapterNumber: 4, page: 90)]
    public class EditSomeText : Window     
    {
        private static string strFileName =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "C:\\#csprojects\\L\\notes.txt");
        TextBox txtbox;

        [STAThread]
        private static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit some text";
            Width = 300;
            Height = 600;

            txtbox = new TextBox();
            txtbox.AcceptsReturn = true;
            txtbox.TextWrapping = TextWrapping.Wrap;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.KeyDown += TextBoxOnKeyDown;
            Content = txtbox;

            // Загрузка текстового файла
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch
            {

            }

            txtbox.CaretIndex = txtbox.Text.Length;
            txtbox.Focus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(strFileName));
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch(Exception exc)
            {
                MessageBoxResult result =
                    MessageBox.Show(
                        "File could not be saved: " + exc.Message + "\nClose program anyway?", 
                        Title, 
                        MessageBoxButton.YesNo, 
                        MessageBoxImage.Exclamation);

                e.Cancel = (result == MessageBoxResult.No);
            }
        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs args)
        {
            if(args.Key == Key.F5)
            {
                txtbox.SelectedText = DateTime.Now.ToString();
                txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
            }
        }




    }
}
