using System.Windows;
using System.Windows.Controls;

namespace Gui.Common
{
    public class ActionExtention : DependencyObject
    {
        #region selectTextFocused
        public static readonly DependencyProperty SelectTextFocusedProperty = DependencyProperty.RegisterAttached("SelectTextFocused", typeof(bool?), typeof(ActionExtention),
            new PropertyMetadata(null, SelectTextFocusedChangedCallback));
        public static bool? GetSelectTextFocused(DependencyObject o)
        {
            return (bool?)o.GetValue(SelectTextFocusedProperty);
        }
        public static void SetSelectTextFocused(DependencyObject o, bool? value)
        {
            o.SetValue(SelectTextFocusedProperty, value);
        }
        private static void SelectTextFocusedChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = o as TextBox;
            if (textBox != null)
            {
                if (e.NewValue is bool result)
                {
                    if (result)
                    {
                        textBox.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;
                        textBox.GotFocus += TextBox_GotFocus;
                    }
                    else
                    {
                        textBox.PreviewMouseLeftButtonDown -= TextBox_PreviewMouseLeftButtonDown;
                        textBox.GotFocus -= TextBox_GotFocus;
                    }
                }
            }
        }

        private static void TextBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (!textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
            }
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }
        #endregion;
    }
}