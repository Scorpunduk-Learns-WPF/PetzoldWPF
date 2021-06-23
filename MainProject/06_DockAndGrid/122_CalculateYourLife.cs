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
    [PetzoldExampleProject(chapterNumber: 6, page: 122)]
    public class CalculateYourLife : Window
    {
        TextBox txtBoxBegin, txtBoxEnd;
        Label lblLifeYears;


        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CalculateYourLife());
        }

        public CalculateYourLife()
        {
            Title = "Calculate Your Life";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            //Создание объекта Grid
            Grid grid = new Grid();
            Content = grid;

            //Определения строк и столбцов
            for(int i = 0; i<3; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDef);
            }
            for (int i = 0; i<2; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            //Первый объект Label
            Label lbl = new Label();
            lbl.Content = "Begin Date";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            //Первый объект TextBox
            txtBoxBegin = new TextBox();
            txtBoxBegin.Text = new DateTime(1980, 1, 1).ToShortDateString();
            txtBoxBegin.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtBoxBegin);
            Grid.SetRow(txtBoxBegin, 0);
            Grid.SetColumn(txtBoxBegin, 1);

            //Второй объект Label
            lbl = new Label();
            lbl.Content = "EndDate: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 1);
            Grid.SetColumn(lbl, 0);


            //Второй объект TextBox
            txtBoxEnd = new TextBox();
            txtBoxEnd.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtBoxEnd);
            Grid.SetRow(txtBoxEnd, 1);
            Grid.SetColumn(txtBoxEnd, 1);

            //Третий объект Label
            lbl = new Label();
            lbl.Content = "Life years: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 2);
            Grid.SetColumn(lbl, 0);

            //Объект Label для вычысленного результата
            lblLifeYears = new Label();
            grid.Children.Add(lblLifeYears);
            Grid.SetRow(lblLifeYears, 2);
            Grid.SetColumn(lblLifeYears, 1);

            // Задание внешних отступов
            Thickness thick = new Thickness(5);
            grid.Margin = thick;
            foreach (Control ctrl in grid.Children)
            {
                ctrl.Margin = thick;
            }
        }

        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            DateTime dtBeg, dtEnd;

            if(DateTime.TryParse(txtBoxBegin.Text, out dtBeg) 
                && DateTime.TryParse(txtBoxEnd.Text, out dtEnd))
            {
                int iYears = dtEnd.Year - dtBeg.Year;
                int iMonths = dtEnd.Month - dtBeg.Month;
                int iDays = dtEnd.Day - dtBeg.Day;

                if (iDays < 0)
                {
                    iDays += DateTime.DaysInMonth(dtEnd.Year, 1 + (dtEnd.Month + 10) % 12);
                    iMonths -= 1;
                }

                if(iMonths < 0)
                {
                    iMonths += 12;
                    iYears -= 1;
                }

                lblLifeYears.Content =
                    String.Format(
                        "{0} year {1}, {2} month{3}, {4} day{5}",
                        iYears,
                        iYears == 1 ? "" : "S",
                        iMonths,
                        iMonths == 1 ? "" : "S",
                        iDays,
                        iDays == 1 ? "" : "S");
            }
            else
            {
                lblLifeYears.Content = "";
            }
        }
    }
}
