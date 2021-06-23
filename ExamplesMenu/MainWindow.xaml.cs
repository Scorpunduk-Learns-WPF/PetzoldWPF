using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Reflection;

using MainProject;

namespace ExamplesMenu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ProjectElement> projectElements;

        public MainWindow()
        {
            InitializeComponent();
            GetListOfWindowChildrens();
        }

        void GetListOfWindowChildrens()
        {
            //Assembly ass = Assembly.LoadWithPartialName("MainProject");
            Assembly ass = Assembly.Load(new AssemblyName("MainProject"));
            //quantityOfWindowClasses.Text = ass.GetTypes().Where(t => t.IsSubclassOf(typeof(Window))).ToArray().Length.ToString();
            quantityOfWindowClasses.Text = ass.Location;
            //Type[] windowChildrens = ass.GetTypes().Where(t => t.IsSubclassOf(typeof(Window))).ToArray();

            Type[] windowChildrens = ass.GetTypes().Where(t => t.GetCustomAttribute(typeof(PetzoldExampleProjectAttribute)) != null).ToArray();



            ProjectElements = new List<ProjectElement>();
            
            ProjectElement pElem;
            //Attribute projectAttribute;

            foreach(Type t in windowChildrens)
            {
                pElem = new ProjectElement(t);
                ProjectElements.Add(pElem);
            }

            List<ProjectElement> orderedElements =
                ProjectElements.OrderBy(elem => elem.ChapterNumber).ThenBy(elem => elem.Page).ToList();

            listOfProjectTypes.ItemsSource = orderedElements;
        }
        
        public List<ProjectElement> ProjectElements
        {
            get { return projectElements; }
            set { projectElements = value; }
        }

        public Type selectedType
        {
            get; set;
        }

        private void ListOfProjectTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fieldToSeeType.Text = (listOfProjectTypes.SelectedItem as ProjectElement).ClassName;
            selectedType = (listOfProjectTypes.SelectedItem as ProjectElement).GetProjectType();
        }

        private void RunSelectedExample_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                var win = Activator.CreateInstance(selectedType) as Window;
                win.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
            
        }
    }

    public class ProjectElement
    {
        private Type typeOfProject;
        private string namespaceOfProject;
        private string className;
        private string chapter;
        private int chapterNumber;
        private int page;


        public ProjectElement(Type typeOfProject)
        {
            this.typeOfProject = typeOfProject;
            NamespaceOfProject = typeOfProject.Namespace;
            ClassName = typeOfProject.Name;

            PetzoldExampleProjectAttribute attr = 
                (PetzoldExampleProjectAttribute)typeOfProject.GetCustomAttribute(typeof(PetzoldExampleProjectAttribute));
            Chapter = attr.Chapter;
            ChapterNumber = attr.ChapterNumber;
            Page = attr.Page;
        }

        public string NamespaceOfProject
        {
            get { return namespaceOfProject; }
            set { namespaceOfProject = value; }
        }

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public string Chapter
        {
            get { return chapter; }
            set { chapter = value; }
        }

        public int ChapterNumber
        {
            get { return chapterNumber; }
            set { chapterNumber = value; }
        }

        public int Page
        {
            get { return page; }
            set { page = value; }
        }
        
        public Type GetProjectType()
        {
            return typeOfProject;
        }
    }
}
