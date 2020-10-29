using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Gui.Common.Controls
{
    public class NumberKeyboard : Control
    {
        private Popup _pop = new Popup();
        private TextBox _owner = null;
        static NumberKeyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberKeyboard), new FrameworkPropertyMetadata(typeof(NumberKeyboard)));
        }
        public NumberKeyboard()
        {
            _pop.Opened += _pop_Opened;
            _pop.Closed += _pop_Closed;
            _pop.Child = this;
            _pop.StaysOpen = false;
            InitializeCommands();
        }

        #region PopupEvent
        private void _pop_Closed(object sender, EventArgs e)
        {
            IsOpen = false;
            _owner = null;
        }
        private void _pop_Opened(object sender, EventArgs e)
        {
            //IsOpen = true;
        }
        #endregion

        #region NumberKeyboard
        public static readonly DependencyProperty NumberKeyboardProperty = DependencyProperty.RegisterAttached("NumberKeyboard", typeof(NumberKeyboard),
            typeof(NumberKeyboard), new PropertyMetadata());
        private static NumberKeyboard GetNumberKeyboard(TextBox textBoxBase)
        {
            return (NumberKeyboard)textBoxBase.GetValue(NumberKeyboardProperty);
        }
        private static void SetNumberKeyboard(TextBox textBoxBase, NumberKeyboard value)
        {
            textBoxBase.SetValue(NumberKeyboardProperty, value);
        }
        #endregion

        #region IsVirtualKeyboardEnable
        public static readonly DependencyProperty IsVirtualKeyboardEnableProperty = DependencyProperty.RegisterAttached("IsVirtualKeyboardEnable",
            typeof(bool), typeof(NumberKeyboard), new PropertyMetadata(false, new PropertyChangedCallback(IsVirtualKeyboardEnableChanged)));
        public static bool GetIsVirtualKeyboardEnable(TextBox textBox)
        {
            return (bool)textBox.GetValue(IsVirtualKeyboardEnableProperty);
        }
        public static void SetIsVirtualKeyboardEnable(TextBox textBox, bool value)
        {
            textBox.SetValue(IsVirtualKeyboardEnableProperty, value);
        }
        private static void IsVirtualKeyboardEnableChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = o as TextBox;
            bool value = (bool)e.NewValue;
            if (textBox != null)
            {
                if (value)
                {
                    InputMethod.SetIsInputMethodEnabled(textBox, false);
                    textBox.PreviewKeyDown += TextBoxBase_PreviewKeyDown;
                    textBox.PreviewMouseLeftButtonUp += TextBox_PreviewMouseLeftButtonUp;
                    textBox.ContextMenu = null;
                    textBox.AllowDrop = false;
                }
                else
                {
                    InputMethod.SetIsInputMethodEnabled(textBox, false);
                    textBox.PreviewKeyDown -= TextBoxBase_PreviewKeyDown;
                    textBox.PreviewMouseLeftButtonUp -= TextBox_PreviewMouseLeftButtonUp;
                }
            }
        }

        private static void TextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            TextBox textBox = sender as TextBox;
            NumberKeyboard numberKeyboard = null;
            if (GetNumberKeyboard(textBox) == null)
            {
                SetNumberKeyboard(textBox, new NumberKeyboard());
            }
            numberKeyboard = GetNumberKeyboard(textBox);
            numberKeyboard._owner = textBox;
            numberKeyboard.Show();
        }

        private static void TextBoxBase_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region IsMinus
        public static readonly DependencyProperty IsMinusProperty = DependencyProperty.Register("IsMinus", typeof(bool), typeof(NumberKeyboard),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsMinusPropertyChanged)));
        public bool IsMinus
        {
            get => (bool)GetValue(IsMinusProperty);
            set => SetValue(IsMinusProperty, value);
        }
        private static void IsMinusPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)o;
            numberKeyboard.CoerceValue(ResultTextProperty);
        }
        #endregion

        private void Show()
        {
            if (_owner != null)
            {
                Keyboard.ClearFocus();
                _owner.ReleaseMouseCapture();
                RawText = _owner.Text;
            }
            IsOpen = true;
        }

        #region IsOpen
        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(typeof(NumberKeyboard),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsOpenPropertyChanged)));

        [Bindable(true), Browsable(false), Category("Appearance")]
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }
        private static void IsOpenPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            NumberKeyboard numberKeyboard = o as NumberKeyboard;
            bool newValue = (bool)e.NewValue;
            numberKeyboard._pop.PlacementTarget = numberKeyboard._owner;

            if (newValue != numberKeyboard._pop.IsOpen)
            {
                numberKeyboard._pop.IsOpen = newValue;
                if (newValue)
                {
                    numberKeyboard._pop.Focus();
                    Mouse.Capture(numberKeyboard._pop);
                }
            }
        }
        #endregion

        #region rawText
        public static readonly DependencyProperty RawTextProperty = DependencyProperty.Register("RawText", typeof(string), typeof(NumberKeyboard),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, new CoerceValueCallback(RawTextCoerceValue)));
        public string RawText
        {
            get => (string)GetValue(RawTextProperty);
            set => SetValue(RawTextProperty, value);
        }
        private static object RawTextCoerceValue(DependencyObject o, object baseValue)
        {
            StringBuilder validNumber = new StringBuilder();
            bool isMinusFlag = false;
            if (baseValue != null)
            {
                string value = (string)baseValue;
                bool dotflag = false;
                if (value.Length > 0 && value[0] == '-')
                {
                    isMinusFlag = true;
                }
                foreach (char c in value)
                {
                    if (char.IsDigit(c))
                    {
                        validNumber.Append(c);
                    }
                    else if (c == '.' && !dotflag)
                    {
                        validNumber.Append(c);
                        dotflag = true;
                    }
                }
            }

            string[] valuePart = validNumber.ToString().Split('.');
            string result = "0";
            if (valuePart.Length == 1)
            {
                string value = valuePart[0].TrimStart('0');
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "0";
                }
                result = value;
            }
            else
            {
                string left = valuePart[0].TrimStart('0');
                if (string.IsNullOrWhiteSpace(left))
                {
                    left = "0";
                }
                string right = valuePart[1].TrimEnd('0');
                if (string.IsNullOrWhiteSpace(right))
                {
                    result = left;
                }
                else
                    result = string.Format("{0}.{1}", left, right);
            }
            if (isMinusFlag)
            {
                return $"-{result}";
            }
            return result;
        }
        #endregion

        #region resultText
        public static readonly DependencyProperty ResultTextProperty = DependencyProperty.Register("ResultText", typeof(string), typeof(NumberKeyboard),
            new FrameworkPropertyMetadata("0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, new CoerceValueCallback(ResultTextCoerceValue)));

        public string ResultText
        {
            get => (string)GetValue(ResultTextProperty);
            set => SetValue(ResultTextProperty, value);
        }
        private static object ResultTextCoerceValue(DependencyObject o, object baseValue)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)o;
            StringBuilder validNumber = new StringBuilder();
            if (baseValue != null)
            {
                string value = (string)baseValue;
                bool dotflag = false;
                foreach (char c in value)
                {
                    if (char.IsDigit(c))
                    {
                        validNumber.Append(c);
                    }
                    else if (c == '.' && !dotflag)
                    {
                        validNumber.Append(c);
                        dotflag = true;
                    }
                }
            }

            string result = "0";
            string[] valuePart = validNumber.ToString().Split('.');
            if (valuePart.Length == 1)
            {
                string value = valuePart[0].TrimStart('0');
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "0";
                }
                result = value;
            }
            else
            {
                string left = valuePart[0].TrimStart('0');
                if (string.IsNullOrWhiteSpace(left))
                {
                    left = "0";
                }
                result = string.Format("{0}.{1}", left, valuePart[1]);
            }

            if (numberKeyboard.IsMinus)
            {
                return $"-{result}";
            }
            return result;
        }
        #endregion

        #region commands
        private void InitializeCommands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number0Command, OnNumber0Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number1Command, OnNumber1Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number2Command, OnNumber2Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number3Command, OnNumber3Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number4Command, OnNumber4Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number5Command, OnNumber5Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number6Command, OnNumber6Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number7Command, OnNumber7Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number8Command, OnNumber8Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(Number9Command, OnNumber9Command));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(DotCommand, OnDotCommand));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(OKCommand, OnOKCommand));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(CancelCommand, OnCancelCommand));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(ClearCommand, OnClearCommand));
            CommandManager.RegisterClassCommandBinding(typeof(NumberKeyboard), new CommandBinding(BackspaceCommand, OnBackspaceCommand));
        }
        private static void OnBackspaceCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            if (numberKeyboard.ResultText.Length >= 1)
            {
                numberKeyboard.ResultText = numberKeyboard.ResultText.Substring(0, numberKeyboard.ResultText.Length - 1);
            }
        }
        private static void OnClearCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText = "0";
        }
        private static void OnCancelCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.OnCancel();
        }
        private static void OnOKCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.OnOK();
        }
        private static void OnDotCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += ".";
        }
        private static void OnNumber0Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "0";
        }
        private static void OnNumber1Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "1";
        }
        private static void OnNumber2Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "2";
        }
        private static void OnNumber3Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "3";
        }
        private static void OnNumber4Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "4";
        }
        private static void OnNumber5Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "5";
        }
        private static void OnNumber6Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "6";
        }
        private static void OnNumber7Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "7";
        }
        private static void OnNumber8Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "8";
        }
        private static void OnNumber9Command(object sender, ExecutedRoutedEventArgs e)
        {
            NumberKeyboard numberKeyboard = (NumberKeyboard)sender;
            numberKeyboard.ResultText += "9";
        }
        public static RoutedCommand Number0Command { get; } = new RoutedCommand("Number0Command", typeof(NumberKeyboard));
        public static RoutedCommand Number1Command { get; } = new RoutedCommand("Number1Command", typeof(NumberKeyboard));
        public static RoutedCommand Number2Command { get; } = new RoutedCommand("Number2Command", typeof(NumberKeyboard));
        public static RoutedCommand Number3Command { get; } = new RoutedCommand("Number3Command", typeof(NumberKeyboard));
        public static RoutedCommand Number4Command { get; } = new RoutedCommand("Number4Command", typeof(NumberKeyboard));
        public static RoutedCommand Number5Command { get; } = new RoutedCommand("Number5Command", typeof(NumberKeyboard));
        public static RoutedCommand Number6Command { get; } = new RoutedCommand("Number6Command", typeof(NumberKeyboard));
        public static RoutedCommand Number7Command { get; } = new RoutedCommand("Number7Command", typeof(NumberKeyboard));
        public static RoutedCommand Number8Command { get; } = new RoutedCommand("Number8Command", typeof(NumberKeyboard));
        public static RoutedCommand Number9Command { get; } = new RoutedCommand("Number9Command", typeof(NumberKeyboard));
        public static RoutedCommand DotCommand { get; } = new RoutedCommand("DotCommand", typeof(NumberKeyboard));
        public static RoutedCommand EqualsCommand { get; } = new RoutedCommand("EqualsCommand", typeof(NumberKeyboard));
        public static RoutedCommand ClearCommand { get; } = new RoutedCommand("ClearCommand", typeof(NumberKeyboard));
        public static RoutedCommand CancelCommand { get; } = new RoutedCommand("CancelCommand", typeof(NumberKeyboard));
        public static RoutedCommand OKCommand { get; } = new RoutedCommand("OKCommand", typeof(NumberKeyboard));
        public static RoutedCommand BackspaceCommand { get; } = new RoutedCommand("BackspaceCommand", typeof(NumberKeyboard));
        #endregion
        private void OnOK()
        {
            if (_owner != null)
            {
                RawText = ResultText;
                _owner.Text = RawText;
                BindingExpression bindingExpression = _owner.GetBindingExpression(TextBox.TextProperty);
                bindingExpression?.UpdateSource();
                IsOpen = false;
            }
        }
        private void OnCancel()
        {
            IsOpen = false;
        }
    }
}
