using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MainProject.OneChildElements
{
    [PetzoldExampleProject(chapterNumber: 11, page: 228)]
    public class BuildButtonFactory : Window
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new BuildButtonFactory());
        }

        public BuildButtonFactory()
        {
            Title = "Build Button Factory";

            //Создание объекта ControlTemplate для Button
            ControlTemplate template = new ControlTemplate(typeof(Button));

            //Создание объекта FrameworkElementFactory для Border
            FrameworkElementFactory factoryBorder =
                new FrameworkElementFactory(typeof(Border));

            //Назначение имени для последующих ссылок
            factoryBorder.Name = "border";

            //Задание некоторых свойств по умолчанию
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty, new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, SystemColors.ControlLightBrush);

            //Создание объекта FrameworkElementFactory для ContentPresenter
            FrameworkElementFactory factoryContent =
                new FrameworkElementFactory(typeof(ContentPresenter));

            //Назначение имени для последующих ссылок
            factoryContent.Name = "content";

            //Привязка свойств ContentPresenter к свойствам Button
            factoryContent.SetValue(ContentPresenter.ContentProperty,
                new TemplateBindingExtension(Button.ContentProperty));

            //Обратите внимание: свойство Padding кнопки
            //соответствует свойству Margin содержимого!
            factoryContent.SetValue(ContentPresenter.MarginProperty,
                new TemplateBindingExtension(Button.PaddingProperty));

            //Назначение ContentPresenter дочерним объектом Border
            factoryBorder.AppendChild(factoryContent);

            //Border назначается корневым узлом визуального дерева
            template.VisualTree = factoryBorder;

            //Определение триггера для условия IsMouseOver = true
            Trigger trig = new Trigger();
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;

            //Связывание объекта Setter с триггером
            //для изменения свойства CornerRadius элемента Border
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
            set.TargetName = "border";

            // включение объекта Setter в коллекцию Setters триггера
            trig.Setters.Add(set);

            //Определение объекта Setter для изменения FontStyle.
            //(Для свойства кнопки задавать TargetName не нужно)
            set = new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;

            //Добавление в коллекцию Setters того же триггера
            trig.Setters.Add(set);

            //Включение триггера в шаблон
            template.Triggers.Add(trig);

            //Определение триггера для IsPressed
            trig = new Trigger();
            trig.Property = Button.IsPressedProperty;
            trig.Value = true;
            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";

            //Включение объекта Setter
            trig.Setters.Add(set);

            //Включение триггера в шаблон
            template.Triggers.Add(trig);

            //Создание объекта Button
            Button btn = new Button();

            //Назначение шаблона
            btn.Template = template;

            //Другие свойства определяются обычным образом
            btn.Content = "Button with Custom Template";
            btn.Padding = new Thickness(20);
            btn.FontSize = 48;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("You clicked the button", Title);
        }
    }
}
