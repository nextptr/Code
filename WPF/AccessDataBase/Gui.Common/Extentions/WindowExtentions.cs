using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Gui.Common
{
    public class WindowExtentions : DependencyObject
    {
        public static readonly DependencyProperty TitleBackgroundProperty = DependencyProperty.RegisterAttached("TitleBackground", typeof(Brush), typeof(WindowExtentions),
            new PropertyMetadata(Brushes.SkyBlue));
        public static Brush GetTitleBackground(DependencyObject o)
        {
            return (Brush)o.GetValue(TitleBackgroundProperty);
        }
        public static void SetTitleBackground(DependencyObject o, Brush value)
        {
            o.SetValue(TitleBackgroundProperty, value);
        }
    }
}