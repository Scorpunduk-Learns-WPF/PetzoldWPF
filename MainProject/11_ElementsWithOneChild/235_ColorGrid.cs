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

namespace MainProject.OneChildElements
{
    public class ColorGrid : Control
    {
        //Количество строк и стобцов
        const int yNum = 5;
        const int xNum = 8;
        //Отображаемые цвета
        string[,] strColors = new string[yNum, xNum]
        {
            {"Black","Brown", "DarkGreen", "MidnightBlue",
             "Navy","DarkBlue","Indigo", "DimGray"},
            {"DarkRed", "OrangeRed", "Olive","Green",
            "Teal", "Blue", "SlateGray", "Gray"},
            {"Red", "Orange", "YellowGreen","SeaGreen",
            "Aqua","LightBlue","Violet", "DarkGray"},
            {"Pink", "Gold", "Yellow", "Lime",
            "Turquoise", "SkyBlue", "Plum", "LightGray"},
            {"LightPink","Tan","LightYellow","LightGreen",
            "LightCyan", "LightSkyBlue","Lavender", "White"}
        };

        //Создаваемые объекты  ColorCell
        ColorCell[,] cells = new ColorCell[yNum, xNum];
        ColorCell cellSelected;
        ColorCell cellHighlighted;

        //Интерфейсные элменты, входящие в состав элемента урпавления
        Border bord;
        UniformGrid unigrid;

        //Текущий выбранный цвет
        Color clrSelected = Colors.Black;

        //Открытое событие "Changed"
        public event EventHandler SelectedColorChanged;

        //Открытый конструктор
        public ColorGrid()
        {
            //Создание объекта Border для элемента
            bord = new Border();
            bord.BorderBrush = SystemColors.ControlDarkDarkBrush;
            bord.BorderThickness = new Thickness(1);
            AddVisualChild(bord); // Необходимо для маршрутизируемых событий
            AddLogicalChild(bord);

            //Создание UniformGrid как дочернего объекта Border
            unigrid = new UniformGrid();
            unigrid.Background = SystemColors.WindowBrush;
            unigrid.Columns = xNum;
            bord.Child = unigrid;

            // Заполнение панели UniformGrid объектами ColorCell
            for(int y = 0; y < yNum; y++)
            {
                for(int x = 0; x <xNum; x++)
                {
                    Color clr = (Color)typeof(Colors).GetProperty(strColors[y, x]).GetValue(null, null);
                    cells[y, x] = new ColorCell(clr);
                    unigrid.Children.Add(cells[y, x]);
                    if(clr == SelectedColor)
                    {
                        cellSelected = cells[y, x];
                        cells[y, x].IsSelected = true;
                    }
                    ToolTip tip = new ToolTip();
                    tip.Content = strColors[y, x];
                    cells[y, x].ToolTip = tip;
                }
            }
        }

        //Открытое свойство SelectedColor (только чтение)
        public Color SelectedColor
        {
            get { return clrSelected; }
        }

        //Переопределение VisualChildrenCount
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        //Переопределение GetVisualChild
        protected override Visual GetVisualChild(int index)
        {
            if(index > 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return bord;
        }

        //Переопределение MeasureOverride
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            bord.Measure(sizeAvailable);
            return bord.DesiredSize;
        }

        //Переопределение ArrangeOverride
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            bord.Arrange(new Rect(new Point(0, 0), sizeFinal));
            return sizeFinal;
        }

        //Обработка событий мыши
        protected override void OnMouseEnter(MouseEventArgs args)
        {
            base.OnMouseEnter(args);
            if(cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            ColorCell cell = args.Source as ColorCell;
            if( cell != null)
            {
                if(cellHighlighted != null)
                {
                    cellHighlighted.IsHighlighted = false;
                }
                cellHighlighted = cell;
                cellHighlighted.IsHighlighted = true;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs args)
        {
            base.OnMouseLeave(args);
            if(cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);
            ColorCell cell = args.Source as ColorCell;
            if(cell != null)
            {
                if(cellHighlighted != null)
                {
                    cellHighlighted.IsSelected = false;
                }
                cellHighlighted = cell;
                cellHighlighted.IsSelected = true;
            }
            Focus();
        }
        protected override void OnMouseUp(MouseButtonEventArgs args)
        {
            base.OnMouseUp(args);
            ColorCell cell = args.Source as ColorCell;
            if(cell != null)
            {
                if (cellSelected != null)
                    cellSelected.IsSelected = false;
                cellSelected = cell;
                cellSelected.IsSelected = true;
                clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                OnSelectedColorChanged(EventArgs.Empty);
            }
        }

        //Обработка событий клавиатуры
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs args)
        {
            base.OnGotKeyboardFocus(args);
            if(cellHighlighted == null)
            {
                if(cellSelected != null)
                {
                    cellHighlighted = cellSelected;
                }
                else
                {
                    cellHighlighted = cells[0, 0];
                }
                cellHighlighted.IsHighlighted = true;
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs args)
        {
            base.OnLostKeyboardFocus(args);
            //base.OnGotKeyboardFocus(args);
            if(cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);
            int index = unigrid.Children.IndexOf(cellHighlighted);
            int y = index / xNum;
            int x = index % yNum;
            switch (args.Key)
            {
                case Key.Home:
                    y = 0;
                    x = 0;
                    break;
                case Key.End:
                    y = yNum - 1;
                    x = xNum + 1;
                    break;
                case Key.Down:
                    if((y = (y + 1) % yNum) == 0) { x++; }
                    break;
                case Key.Up:
                    if((y = (y + yNum - 1) % yNum) == yNum - 1) { x--; }
                    break;
                case Key.Right:
                    if((x= (x + 1) % xNum) == 0) { y++; }
                    break;
                case Key.Left:
                    if((x = (x + xNum - 1) % xNum) == xNum - 1) { y--; }
                    break;
                case Key.Enter:
                case Key.Space:
                    if(cellSelected != null) { cellSelected.IsSelected = false; }
                    cellSelected = cellHighlighted;
                    cellSelected.IsSelected = true;
                    clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                    OnSelectedColorChanged(EventArgs.Empty);
                    break;
                default:
                    return;
            }
            if(x>= xNum || y >= yNum)
            {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            else if (x < 0 || y < 0)
            {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }
            else
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = cells[y, x];
                cellHighlighted.IsHighlighted = true;
            }
            args.Handled = true;
        }

        //Защищенный метод, инициирующий событие SelectedColorChanged
        protected virtual void OnSelectedColorChanged(EventArgs args)
        {
            if(SelectedColorChanged != null) { SelectedColorChanged(this, args); }
        }
    }
}
