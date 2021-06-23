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
    public class NamedColor
    {
        static NamedColor[] nclrs;
        Color clr;
        string str;

        //Статический конструктор
        static NamedColor()
        {
            PropertyInfo[] props = typeof(Colors).GetProperties();
            nclrs = new NamedColor[props.Length];
            for(int i = 0; i < props.Length; i++)
            {
                nclrs[i] =
                    new NamedColor(props[i].Name, (Color)props[i].GetValue(null, null));
            }            
        }

        //Приватный конструктор
        private NamedColor(string str, Color clr)
        {
            this.str = str;
            this.clr = clr;
        }

        //Статическое свойство, доступное только для чтения
        public static NamedColor[] All
        {
            get { return nclrs; }
        }

        //Свойства, доступные только для чтения
        public Color Color
        {
            get { return clr; }
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

        //Переопределение метода ToString
        public override string ToString()
        {
            return str;
        }
    }
}
