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
    public class UniformGridAlmost : Panel
    {
        public static readonly DependencyProperty ColumnsProperty;

        // Статический конструктор для создания зависимого свойства
        static UniformGridAlmost()
        {
            ColumnsProperty =
                DependencyProperty.Register(
                    "Columns",
                    typeof(int),
                    typeof(UniformGridAlmost),
                    new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        //Свойство Columns
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        //Свойство Rows, доступное только для чтения
        public int Rows
        {
            get { return (InternalChildren.Count + Columns - 1) / Columns; }
        }

        //Переопределение MeasureOverride распределяет место
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            //Вычисление размера дочернего элемента
            Size sizeChild = new Size(sizeAvailable.Width / Columns, sizeAvailable.Height / Rows);

            //Переменные для накопления максимальных ширин и высот
            double maxwidth = 0;
            double maxheight = 0;
            foreach (UIElement child in InternalChildren)
            {
                //Вызывать Measure для каждого дочернего объекта...
                child.Measure(sizeChild);

                //...а затем проверить свойство DesiredSize
                //дочернего объекта
                maxwidth = Math.Max(maxwidth, child.DesiredSize.Width);
                maxheight = Math.Max(maxheight, child.DesiredSize.Height);
            }

            //Теперь вычисляется желательный размер для самой решетки
            return new Size(Columns * maxwidth, Rows * maxheight);
        }

        //Переопределение ArrangeOverride размещает дочерние объекты
        protected override Size ArrangeOverride (Size sizeFinal)
        {
            //Вычисление размера дочерних объектов
            // для строк и столбцов одинакового размера
            Size sizeChild = new Size(sizeFinal.Width / Columns, sizeFinal.Height / Rows);
            for(int index = 0; index < InternalChildren.Count; index++)
            {
                int row = index / Columns;
                int col = index % Columns;

                //Вычисление прямоугольника для каждого дочернего объекта
                // в границах sizeFinal...
                Rect rectChild =
                    new Rect(new Point(col * sizeChild.Width, row * sizeChild.Height), sizeChild);
                //... И вызов Arrange для этого объекта
                InternalChildren[index].Arrange(rectChild);
            }
            return sizeFinal;
        }
    }
}
