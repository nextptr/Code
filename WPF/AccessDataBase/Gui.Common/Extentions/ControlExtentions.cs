using System.Windows;
using System.Windows.Media;

namespace Gui.Common
{
    public class ControlExtentions : DependencyObject
    {
        #region background
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(ControlExtentions),
            new PropertyMetadata(Brushes.Orange));
        public static Brush GetMouseOverBackground(DependencyObject o)
        {
            return (Brush)o.GetValue(MouseOverBackgroundProperty);
        }
        public static void SetMouseOverBackground(DependencyObject o, Brush value)
        {
            o.SetValue(MouseOverBackgroundProperty, value);
        }

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.RegisterAttached("PressedBackground", typeof(Brush), typeof(ControlExtentions),
            new FrameworkPropertyMetadata(Brushes.DarkOrange));
        public static Brush GetPressedBackground(DependencyObject o)
        {
            return (Brush)o.GetValue(PressedBackgroundProperty);
        }
        public static void SetPressedBackground(DependencyObject o, Brush value)
        {
            o.SetValue(PressedBackgroundProperty, value);
        }
        #endregion

        #region logo
        public static readonly DependencyProperty LogoWidthProperty = DependencyProperty.RegisterAttached("LogoWidth", typeof(double), typeof(ControlExtentions),
            new PropertyMetadata(0.0));
        public static double GetLogoWidth(DependencyObject o)
        {
            return (double)o.GetValue(LogoWidthProperty);
        }
        public static void SetLogoWidth(DependencyObject o, double value)
        {
            o.SetValue(LogoWidthProperty, value);
        }

        public static readonly DependencyProperty LogoHeightProperty = DependencyProperty.RegisterAttached("LogoHeight", typeof(double), typeof(ControlExtentions),
            new PropertyMetadata(0.0));
        public static double GetLogoHeight(DependencyObject o)
        {
            return (double)o.GetValue(LogoHeightProperty);
        }
        public static void SetLogoHeight(DependencyObject o, double value)
        {
            o.SetValue(LogoHeightProperty, value);
        }

        public static readonly DependencyProperty LogoMarginProperty = DependencyProperty.RegisterAttached("LogoMargin", typeof(Thickness), typeof(ControlExtentions),
            new PropertyMetadata(null));
        public static Thickness GetLogoMargin(DependencyObject o)
        {
            return (Thickness)o.GetValue(LogoMarginProperty);
        }
        public static void SetLogoMargin(DependencyObject o, Thickness value)
        {
            o.SetValue(LogoMarginProperty, value);
        }

        public static readonly DependencyProperty LogoProperty = DependencyProperty.RegisterAttached("Logo", typeof(Brush), typeof(ControlExtentions),
            new PropertyMetadata(Brushes.Transparent));
        public static Brush GetLogo(DependencyObject o)
        {
            return (Brush)o.GetValue(LogoProperty);
        }
        public static void SetLogo(DependencyObject o, Brush value)
        {
            o.SetValue(LogoProperty, value);
        }
        #endregion

        public static readonly DependencyProperty SecondLineContentProperty = DependencyProperty.RegisterAttached("SecondLineContent", typeof(object), typeof(ControlExtentions),
            new PropertyMetadata(null));
        public static object GetSecondLineContent(DependencyObject o)
        {
            return o.GetValue(SecondLineContentProperty);
        }
        public static void SetSecondLineContent(DependencyObject o, object value)
        {
            o.SetValue(SecondLineContentProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ControlExtentions),
            new PropertyMetadata(null));
        public static CornerRadius GetCornerRadius(DependencyObject o)
        {
            return (CornerRadius)o.GetValue(CornerRadiusProperty);
        }
        public static void SetCornerRadius(DependencyObject o, CornerRadius value)
        {
            o.SetValue(CornerRadiusProperty, value);
        }
    }
}