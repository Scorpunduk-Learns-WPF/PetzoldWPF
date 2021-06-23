using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media; //fonts
using System.Windows.Input;
using System.ComponentModel;

namespace MainProject.ContentConceptions
{
    [PetzoldExampleProject(chapterNumber: 3, page: 58)]
    public class RecordKeyStrokes : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new RecordKeyStrokes());
        }

        public RecordKeyStrokes()
        {
            Title = "Record keystrokes";
            Content = "";
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            string str = Content as string;
            if(e.Text == "\b")
            {
                if(str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            else
            {
                str += e.Text;
            }

            Content = str;

        }

    }
}
