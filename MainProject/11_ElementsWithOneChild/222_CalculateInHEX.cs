using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Threading;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MainProject.OneChildElements
{
    [PetzoldExampleProject(chapterNumber: 11, page: 222)]
    public class CalculateInHEX : Window
    {
        //Приватные поля 
        RoundedButton btnDisplay;
        ulong numDisplay;
        ulong numFirst;
        bool bNewNumber = true;
        char chOperation = '=';

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CalculateInHEX());
        }

        //Конструктор
        public CalculateInHEX()
        {
            Title = "Calculate in HEX";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            //Создание объекта Grid содержимого окна
            Grid grid = new Grid();
            grid.Margin = new Thickness(4);
            Content = grid;
            //Создание пяти столбцов
            for(int i = 0; i<5; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }
            //Создание семи строк
            for(int i = 0; i < 7; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }
            //Текст, выводимый на кнопках
            string[] strButtons = {"0",
            "D", "E", "F", "+", "&",
            "A", "B", "C", "-", "|",
            "7", "8", "9", "*", "^",
            "4", "5", "6", "/", "<<",
            "1", "2", "3", "%", ">>",
            "0", "Back", "Equals" };

            int iRow = 0, iCol = 0;

            //Создание кнопок
            foreach (string str in strButtons)
            {
                //Создание RoundedButton
                RoundedButton btn = new RoundedButton();
                btn.Focusable = false;
                btn.Height = 32;
                btn.Margin = new Thickness(4);
                btn.Click += ButtonOnClick;

                //Создание объекта TextBlock
                //для свойства Child объекта RoundedButton
                TextBlock txt = new TextBlock();
                txt.Text = str;
                btn.Child = txt;

                //Включение объекта RoundedButton на панель Grid
                grid.Children.Add(btn);
                Grid.SetRow(btn, iRow);
                Grid.SetColumn(btn, iCol);

                //Особая обработка для кнопки Display
                if (iRow == 0 && iCol == 0)
                {
                    btnDisplay = btn;
                    btn.Margin = new Thickness(4, 4, 4, 6);
                    Grid.SetColumnSpan(btn, 5);
                    iRow = 1;
                }

                //а также для кнопок Back и Equals
                else if(iRow == 6 && iCol > 0)
                {
                    Grid.SetColumnSpan(btn, 2);
                    iCol += 2;
                }
                //для всех остальных кнопок
                else
                {
                    btn.Width = 32;
                    if (0 == (iCol = (iCol + 1) % 5)) 
                    {
                        iRow++; 
                    }
                }
            }
        }

        //Обработчик события Click
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            //Получение кнопки, на которой был сделан щелчок
            RoundedButton btn = args.Source as RoundedButton;
            if(btn == null)
            {
                return;
            }

            //Получение текста кнопка и первого символа
            string strButton = (btn.Child as TextBlock).Text;
            char chButton = strButton[0];
            
            //Некоторые особые случаи
            if(strButton == "Equals")
            {
                chButton = '=';
            }
            if(btn == btnDisplay)
            {
                numDisplay = 0;
            }
            else if(strButton == "Back")
            {
                numDisplay /= 16;
            }

            //Шестнадцатиричные цифры
            else if (Char.IsLetterOrDigit(chButton))
            {
                if (bNewNumber)
                {
                    numFirst = numDisplay;
                    numDisplay = 0;
                    bNewNumber = false;
                }
                if(numDisplay <= ulong.MaxValue >> 4)
                {
                    numDisplay =
                        16 * numDisplay + (ulong)(chButton - (Char.IsDigit(chButton) ? '0' : 'A' - 10));
                }
            }

            //Рабочий режим
            else
            {
                if (!bNewNumber)
                {
                    switch (chOperation)
                    {
                        case '=': break;
                        case '+':
                            numDisplay = numFirst + numDisplay; break;
                        case '-':
                            numDisplay = numFirst - numDisplay; break;
                        case '*':
                            numDisplay = numFirst * numDisplay; break;
                        case '&':
                            numDisplay = numFirst & numDisplay; break;
                        case '|':
                            numDisplay = numFirst | numDisplay; break;
                        case '^':
                            numDisplay = numFirst ^ numDisplay; break;
                        case '<':
                            numDisplay = numFirst << (int)numDisplay; break; 
                        case '>':
                            numDisplay = numFirst >> (int)numDisplay; break;
                        case '/':
                            numDisplay = numFirst != 0 ? numFirst / numDisplay : ulong.MaxValue;
                            break;
                        case '%':
                            numDisplay = numFirst != 0 ? numFirst % numDisplay : ulong.MaxValue;
                            break;
                        default: numDisplay = 0; break;
                    }
                }
                bNewNumber = true;
                chOperation = chButton;
            }

            //Форматирование вывода
            TextBlock text = new TextBlock();
            text.Text = String.Format("{0:X}", numDisplay);
            btnDisplay.Child = text;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if(e.Text.Length == 0)
            {
                return;
            }

            //Получение нажатой клавиши
            char chKey = Char.ToUpper(e.Text[0]);

            //Перебор кнопок 
            foreach (UIElement child in (Content as Grid).Children)
            {
                RoundedButton btn = child as RoundedButton;
                string strButton = (btn.Child as TextBlock).Text;

                //Messy logic to check for matching button
                if(
                    (chKey == strButton[0] && btn != btnDisplay && strButton != "Equals" && strButton != "Back") ||
                    (chKey == '=' && strButton == "Equals") ||
                    (chKey == '\r' && strButton == "Equals") ||
                    (chKey == '\b' && strButton == "Bask") ||
                    (chKey == '\x1B' && btn == btnDisplay)
                  )
                {
                    //Имитация события Click для обработки нажатия клавиши
                    RoutedEventArgs argsClick = new RoutedEventArgs(RoundedButton.ClickEvent, btn);
                    btn.RaiseEvent(argsClick);

                    //Имитация нажатия кнопки
                    btn.IsPressed = true;

                    //Установка таймера для возврата кнопки
                    //в обычное состояние
                    DispatcherTimer tmr = new DispatcherTimer();
                    tmr.Interval = TimeSpan.FromMilliseconds(100);
                    tmr.Tag = btn;
                    tmr.Tick += TimerOnTick;
                    tmr.Start();
                    e.Handled = true; 
                }
            }
        }

        void TimerOnTick(object sender, EventArgs args)
        {
            //Отмена нажатия кнопка
            DispatcherTimer tmr = sender as DispatcherTimer;
            RoundedButton btn = tmr.Tag as RoundedButton;
            btn.IsPressed = false;

            //Остановка таймера и удаление обработчика события
            tmr.Stop();
            tmr.Tick -= TimerOnTick;
        }
    }
}
