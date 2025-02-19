﻿using System;
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

namespace WpfApp
{
    /// <summary>
    /// CarDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class CarDetailView : UserControl
    {
        private Car car;

        public Car Car
        {
            get
            {
                return car;
            }
            set
            {
                car = value;
                this.txt_name.Text = car.Name;
                this.txt_year.Text = car.Year;
                this.txt_auto.Text = car.Automaker;
                this.txt_topSpeed.Text = car.TopSpeed;
                string uriStr = string.Format(@"Resources/Logos/{0}.png", car.Name);
                this.image_Photo.Source = new BitmapImage(new Uri(uriStr, UriKind.Relative));
            }
        }


        public CarDetailView()
        {
            InitializeComponent();
        }
    }
}
