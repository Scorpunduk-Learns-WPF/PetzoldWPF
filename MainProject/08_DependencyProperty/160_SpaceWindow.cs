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
    [PetzoldExampleProject(chapterNumber: 8, page: 160)]
    public class SpaceWindow : Window
    {
        //DependencyProperty и свойство
        public static readonly DependencyProperty SpaceProperty;
        public int Space
        {
            set { SetValue(SpaceProperty, value); }
            get { return (int)GetValue(SpaceProperty); }
        }

        //Статический конструктор
        static SpaceWindow()
        {
            //Определение метаданных
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.Inherits = true;
            //Добавление владельца к SpaceProperty
            //и переопределение метаданных
            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof(SpaceWindow));
            SpaceProperty.OverrideMetadata(typeof(SpaceWindow), metadata);
        }
    }
}
