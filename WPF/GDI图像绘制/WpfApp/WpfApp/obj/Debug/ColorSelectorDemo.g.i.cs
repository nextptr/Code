﻿#pragma checksum "..\..\ColorSelectorDemo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D8692FA95BC3243DDBFA2DD4A66A575F736567E1"
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
using System.Windows.Forms.Integration;
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
    /// ColorSelectorDemo
    /// </summary>
    public partial class ColorSelectorDemo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\ColorSelectorDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgColor;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ColorSelectorDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ellColor;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ColorSelectorDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgColorSelector;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ColorSelectorDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectColorSelector;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ColorSelectorDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SelectColor;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp;component/colorselectordemo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ColorSelectorDemo.xaml"
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
            this.imgColor = ((System.Windows.Controls.Image)(target));
            
            #line 21 "..\..\ColorSelectorDemo.xaml"
            this.imgColor.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.imgColor_MouseDown);
            
            #line default
            #line hidden
            
            #line 21 "..\..\ColorSelectorDemo.xaml"
            this.imgColor.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgColor_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ellColor = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.imgColorSelector = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\ColorSelectorDemo.xaml"
            this.imgColorSelector.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.imgColorSelector_MouseDown);
            
            #line default
            #line hidden
            
            #line 28 "..\..\ColorSelectorDemo.xaml"
            this.imgColorSelector.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgColorSelector_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rectColorSelector = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.SelectColor = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

