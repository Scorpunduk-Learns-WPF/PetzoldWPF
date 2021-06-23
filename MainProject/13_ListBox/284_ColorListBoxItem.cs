using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Shapes;

namespace MainProject._13_ListBox
{
    public class ColorListBoxItem : ListBoxItem
    {
        string str;
        Rectangle rect;
        TextBlock text;

        public ColorListBoxItem()
        {
            //Создание панели StackPanel для вывода Rectangle и TextBlock
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            //Создание объекта Rectangle для вывода образца цвета
            rect = new Rectangle();
            rect.Width = 16;
            rect.Height = 16;
            rect.Margin = new Thickness(2);
            rect.Stroke = SystemColors.WindowTextBrush;
            stack.Children.Add(rect);

            //Создание объекта Textblock для вывода названия цвета
            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        //Свойство Text становится свойством объекта TextBlock
        public string Text
        {
            get { return str; }
            set
            {
                str = value;
                string strSpaced = str[0].ToString();
                for(int i = 1; i < str.Length; i++)
                {
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") + str[i].ToString();
                }
                text.Text = strSpaced;
            }
        }

        //Свойство Color становится свойством Brush объекта Rectangle
        public Color Color
        {
            get
            {
                SolidColorBrush brush = rect.Fill as SolidColorBrush;
                return brush == null ? Colors.Transparent : brush.Color;
            }
            set { rect.Fill = new SolidColorBrush(value); }
        }

        // Выделенный вариант помечается жирным шрифтом
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            text.FontWeight = FontWeights.Bold;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            text.FontWeight = FontWeights.Regular;
        }

        //Реализация ToString() для клавиатурного интерфейса
        public override string ToString()
        {
            return str;
        }
    }
}
