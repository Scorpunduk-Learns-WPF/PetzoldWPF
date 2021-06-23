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

namespace MainProject._13_ListBox
{
    public class NamedBrush
    {
        static NamedBrush[] nbrushes;
        Brush brush;
        string str;

        //Статический конструктор
        static NamedBrush()
        {
            PropertyInfo[] props = typeof(Brushes).GetProperties();
            nbrushes = new NamedBrush[props.Length];
            for(int i = 0; i < props.Length; i++)
            {
                nbrushes[i] = new NamedBrush(props[i].Name, (Brush)props[i].GetValue(null, null));
            }
        }

        //Приватный конструктор
        private NamedBrush(string str, Brush brush)
        {
            this.str = str;
            this.brush = brush;
        }

        //Статическое свойство, доступное только для чтения
        public static NamedBrush[] All
        {
            get { return nbrushes; }
        }

        //Свойства, доступные только для чтения 
        public Brush Brush
        {
            get { return brush; }
        }

        public string Name
        {
            get
            {
                string strSpaced = str[0].ToString();
                for(int i = 1; i < str.Length; i++)
                {
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") + str[i].ToString();
                }
                return strSpaced;
            }
        }

        //Переопределение метода ToString()
        public override string ToString()
        {
            return str;
        }
    }
}
