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


namespace MainProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Type[] typesList = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Window))).ToArray();

            Console.WriteLine(typesList.Length);

            foreach (Type t in typesList)
            {
                Console.WriteLine($"{t.Namespace} - {t.Name}");
            }
            
            Console.ReadKey();
        }
    }

    [AttributeUsageAttribute(AttributeTargets.Class)]
    public sealed class PetzoldExampleProjectAttribute : Attribute
    {
        public int Page
        {
            get; set;
        }

        public string Chapter
        {
            get; set;
        }

        public int ChapterNumber
        {
            get; set;
        }

        public PetzoldExampleProjectAttribute(int chapterNumber, int page)
        {
            Chapter = ChaptersNames.numberName[chapterNumber];
            ChapterNumber = chapterNumber;
            Page = page;
        }
    }

    public enum Chapters
    {
        ApplicationsAndWindows = 1,
        BaseBrushes = 2,
    }

    public static class ChaptersNames
    {
        public static Dictionary<int, string> numberName = new Dictionary<int, string>()
        {
            {1, "Applications And Windows" },
            {2, "Base Brushes" },
            {3, "Content Concepts" },
            {4, "Buttons and Other control elements" },
            {5, "StackPanel and WrapPanel" },
            {6, "Dock and Grid" },
            {7, "Canvas" },
            {8, "Dependency Properties" },
            {9, "Routed Events of User Input" },
            {10, "User Elements" },
            {11, "Elements With One Child" },
            {12, "User Panels" },
            {13, "List Box" },
            {14, "Menu Hierarchy" },
            {15, "Tools Panel and Status Bar" },
            {16, "TreeView and ListView" },
            {17, "Printing and Dialogs" },
            {18, "Notepad Clone" },
        };

    }
}
