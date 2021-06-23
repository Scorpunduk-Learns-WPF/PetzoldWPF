using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.IO;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents; // new Italic()
using System.ComponentModel;


namespace _30_EditSomeRichText
{
    // page 93
    class EditSomeRichText : Window
    {
        RichTextBox txtbox;
        string strFilter = "Document Files(*.xaml)|*.xaml|All files (*.*)|*.*";

        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new EditSomeRichText());
        }

        public EditSomeRichText()
        {
            Title = "Edit some Rich Text";
            txtbox = new RichTextBox();
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = txtbox;
            txtbox.Focus();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if(e.ControlText.Length > 0 && e.ControlText[0] == '\x0F')
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.Filter = strFilter;

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Open);
                        //range.Load(strm, DataFormats.Xaml);
                        range.Load(strm, DataFormats.Text);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null) strm.Close();
                    }
                }
                e.Handled = true;
            }

            if (e.ControlText.Length > 0 && e.ControlText[0] == '\x13')
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = strFilter;

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Create);
                        //range.Save(strm, DataFormats.Xaml);
                        range.Load(strm, DataFormats.Text);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null) strm.Close();
                    }
                }
                e.Handled = true;
            }
        }
    }
}
