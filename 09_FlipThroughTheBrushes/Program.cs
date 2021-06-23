using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media; // for the brushes

using System.Reflection; // for propertyinfo


namespace _09_FlipThroughTheBrushes
{    
    // page 40
    class FlipThroughTheBrushes : Window
    {
        int index = 0;
        // from mscorelib
        PropertyInfo[] props;


        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new FlipThroughTheBrushes());

        }

        public FlipThroughTheBrushes()
        {
            props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
            SetTitleAndBackground();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.Key == Key.Down || e.Key == Key.Up)
            {
                index += e.Key == Key.Up ? 1 : props.Length - 1;
                index %= props.Length;
                SetTitleAndBackground();
            }

            base.OnKeyDown(e);
        }

        void SetTitleAndBackground()
        {
            Title = $"Flip Through the Brushes - {props[index].Name}";
            Background = (Brush)props[index].GetValue(null, null);
        }
    }
}
