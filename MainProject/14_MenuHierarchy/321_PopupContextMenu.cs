﻿using System;
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

namespace MainProject._14_MenuHierarchy
{
    [PetzoldExampleProject(chapterNumber: 14, page: 321)]
    public class PopupContextMenu : Window
    {
        ContextMenu menu;
        MenuItem itemBold, itemItalic;
        MenuItem[] itemDecor;
        Inline inlClicked;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PopupContextMenu());
        }

        public PopupContextMenu()
        {
            Title = "Popup Context Menu";
            //Создание объекта ContextMenu
            menu = new ContextMenu();

            //Добавление команды "Bold"
            itemBold = new MenuItem();
            itemBold.Header = "Bold";
            menu.Items.Add(itemBold);

            //Добавление команды "Italic"
            itemItalic = new MenuItem();
            itemItalic.Header = "Italic";
            menu.Items.Add(itemItalic);

            //получение всех данных TextDecorationLocation
            TextDecorationLocation[] locs =
                (TextDecorationLocation[])Enum.GetValues(typeof(TextDecorationLocation));

            //Создание массива объектов MenuItem и его заполнение
            itemDecor = new MenuItem[locs.Length];
            for(int i = 0; i < locs.Length; i++)
            {
                TextDecoration decor = new TextDecoration();
                decor.Location = locs[i];
                itemDecor[i] = new MenuItem();
                itemDecor[i].Header = locs[i].ToString();
                itemDecor[i].Tag = decor;
                menu.Items.Add(itemDecor[i]);
            }

            //Один обработчик используется для всех контекстных меню
            menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MenuOnClick));

            //Создание объекта TextBlock как содержимого окна
            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            //Разбиение знаменитой цитаты на слова
            string strQuote = "To be, or not to be, that is the question";
            string[] strWords = strQuote.Split();

            //Создание объекта Run для каждого слова
            //и его включение в TextBlock
            foreach (string str in strWords)
            {
                Run run = new Run(str);
                run.TextDecorations = new TextDecorationCollection();
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }

            
        


        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonUp(args);
            if((inlClicked = args.Source as Inline) != null)
            {
                //Пометка команд меню в соответствии
                // со значениями свойств Inline
                itemBold.IsChecked = (inlClicked.FontWeight == FontWeights.Bold);
                itemItalic.IsChecked = (inlClicked.FontStyle == FontStyles.Italic);

                foreach(MenuItem item in itemDecor)
                {
                    item.IsChecked = (inlClicked.TextDecorations.Contains(item.Tag as TextDecoration));
                }
                // Отображение контекстного меню
                menu.IsOpen = true;
                args.Handled = true;
            }
        }

        void MenuOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = args.Source as MenuItem;
            item.IsChecked ^= true;

            //изменение объекта InLine в зависимости
            //от установленной или снятой пометки
            if (item == itemBold)
            {
                inlClicked.FontWeight = (item.IsChecked ? FontWeights.Bold : FontWeights.Normal);
            }
            else if (item == itemItalic)
            {
                inlClicked.FontStyle = (item.IsChecked ? FontStyles.Italic : FontStyles.Normal);
            }
            else
            {
                if (item.IsChecked)
                {
                    inlClicked.TextDecorations.Add(item.Tag as TextDecoration);
                }
                else
                {
                    inlClicked.TextDecorations.Remove(item.Tag as TextDecoration);
                }
            }
            (inlClicked.Parent as TextBlock).InvalidateVisual();
        }
    }
}
