﻿#pragma checksum "..\..\..\UseCommandDemo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4A74C4F847F88E7C3583A6B049AAAADE0785D718"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp;


namespace WpfApp {
    
    
    /// <summary>
    /// UseCommandDemo
    /// </summary>
    public partial class UseCommandDemo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stack_wrap;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_RecvCmd;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_sendCmd;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid unif_wrap;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_command_cut;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_command_copy;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_command_past;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_copy_dst;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_Name;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ls_NewItems;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\UseCommandDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_command_dest;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp;component/usecommanddemo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UseCommandDemo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 18 "..\..\..\UseCommandDemo.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.CommandBinding_CanExecute);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\UseCommandDemo.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 19 "..\..\..\UseCommandDemo.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.CommandBinding_CanExecute_MyDeleteCommand);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\UseCommandDemo.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed_MyDeleteCommand);
            
            #line default
            #line hidden
            return;
            case 3:
            this.stack_wrap = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.txt_RecvCmd = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btn_sendCmd = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.unif_wrap = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 7:
            this.btn_command_cut = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.btn_command_copy = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.btn_command_past = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.txt_copy_dst = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.txt_Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.ls_NewItems = ((System.Windows.Controls.ListBox)(target));
            return;
            case 13:
            this.txt_command_dest = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

