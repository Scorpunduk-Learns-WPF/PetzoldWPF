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


namespace MainProject._12_UserPanels
{
    [PetzoldExampleProject(chapterNumber: 12, page: 255)]
    public class DiagonalizeTheButton : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DiagonalizeTheButton());
        }

        public DiagonalizeTheButton()
        {
            Title = "Diagonalize the Buttons";
            DiagonalPanel pnl = new DiagonalPanel();
            Content = pnl;
            Random rand = new Random();
            for(int i = 0; i < 5; i++)
            {
                Button btn = new Button();
                btn.Content = "Button Number " + (i + 1);
                btn.FontSize += rand.Next(20);
                pnl.Add(btn);
            }
        }
    }
}
