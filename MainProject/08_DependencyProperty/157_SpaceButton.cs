using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MainProject._08_DependencyProp
{
    public class SpaceButton : Button
    {
        //Традиционное приватное поле .NET и открытое свойство 
        string txt;
        public string Text
        {
            get
            {
                return txt;
            }

            set
            {
                txt = value;
                Content = SpaceOutText(txt);
            }
        }

        //DependencyProperty и открытое свойство
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            get { return (int)GetValue(SpaceProperty); }
            set { SetValue(SpaceProperty, value); }
        }

        // Статический конструктор
        static SpaceButton()
        {
            //Определение метаданных
            FrameworkPropertyMetadata metadata =
                new FrameworkPropertyMetadata();
            metadata.DefaultValue = 1;
            metadata.AffectsMeasure = true;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;

            // Регистрация DependencyProperty
            SpaceProperty = DependencyProperty.Register("Space", typeof(int), typeof(SpaceButton), metadata, ValidateSpaceValue);
        }

        // Метода обратного вызова для проверки значения
        static bool ValidateSpaceValue(object obj)
        {
            int i = (int)obj;
            return i >= 0;
        }

        // метод обратного вызова для оповещения об изменении свойства
        static void OnSpacePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SpaceButton btn = obj as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);
        }

        //Метод для вставки пробелов в текст
        string SpaceOutText(string str)
        {
            if(str == null)
            {
                return null;
            }

            StringBuilder build = new StringBuilder();
            foreach (char ch in str)
            {
                build.Append(ch + new string(' ', Space));
            }

            return build.ToString();
        }
    }
}
