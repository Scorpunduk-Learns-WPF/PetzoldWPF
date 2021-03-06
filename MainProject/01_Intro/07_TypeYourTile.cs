using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace MainProject.Intro
{
    [PetzoldExampleProject(chapterNumber: 1, page: 31)]
    class TypeYourTitle : Window
    {        
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new TypeYourTitle());           
        }

        public TypeYourTitle()
        {
            //WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.CanResizeWithGrip;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            if(e.Text == "\b" && Title.Length > 0)
            {
                Title = Title.Substring(0, Title.Length - 1);
            }
            else if(e.Text.Length > 0 && !Char.IsControl(e.Text[0]))
            {
                Title += e.Text;
            }
        }
    }
}
