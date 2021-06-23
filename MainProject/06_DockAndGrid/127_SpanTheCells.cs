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


namespace MainProject.DockAndGrid
{
    [PetzoldExampleProject(chapterNumber: 6, page: 127)]
    public class SpanTheCells : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SpanTheCells());
        }

        public SpanTheCells()
        {
            Title = "SpanTheCells";
            SizeToContent = SizeToContent.WidthAndHeight;

            //Создание объекта Grid
            Grid grid = new Grid();
            grid.Margin = new Thickness(5);
            Content = grid;

            //Создание определений строк
            for (int i = 0; i < 6; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            //Создание определений столбцов
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();

                if(i == 1)
                {
                    coldef.Width = new GridLength(100, GridUnitType.Star);
                }
                else
                {
                    coldef.Width = GridLength.Auto;
                }

                grid.ColumnDefinitions.Add(coldef);
            }

            //создание надписей и текстовых полей
            string[] astrLabel =
            {
                "_First name:", "_Last name:",
                "_Social security number:",
                "_Credit card number:",
                "_Other personal stuff:"
            };

            for(int i = 0; i < astrLabel.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = astrLabel[i];
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);

                TextBox txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);
                grid.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
                Grid.SetColumnSpan(txtbox, 3);
            }

            // создание кнопок
            Button btn = new Button();
            btn.Content = "Submit";
            btn.Margin = new Thickness(5);
            btn.Click += delegate { Close(); };
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 2);

            btn = new Button();
            btn.Content = "Cancel";
            btn.Margin = new Thickness(5);
            btn.Click += delegate { Close(); };
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 3);

            //Передача фокуса первому текстовому полю
            grid.Children[1].Focus();
        }
    }
}
