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
using System.Windows.Threading;

namespace Gui.Common.Clock
{
    /// <summary>
    /// ClockPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ClockPanel : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();
        DateTime CurrTime = DateTime.Now;

        double act_height = 0.0;
        double act_width = 0.0;
        double radius = 250;
        double diameter = 500;
        double angle = 360;
        Point Opos = new Point();
        Point topLeft = new Point();
        Point topRight = new Point();
        Point bottomLeft = new Point();
        Point bottomRight = new Point();
        Line HourLine, MinuLine, SecdLine;

        public ClockPanel()
        {
            InitializeComponent();

            this.Loaded += ClockPanel_Loaded;

        

            HourLine = new Line();
            MinuLine = new Line();
            SecdLine = new Line();
        }

        private void ClockPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsVisible)
            {
                ReSizePanel();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += Timer_Tick;
                timer.Start();
                this.Loaded -= ClockPanel_Loaded;
            }
        }

        public void ReSizePanel()
        {
            act_height = this.ActualHeight;
            act_width = this.ActualWidth;

            Opos = new Point(act_width / 2, act_height / 2);
            if (act_height > act_width)
            {
                diameter = act_width;
            }
            else
            {
                diameter = act_height;
            }
            radius = diameter / 2;

            topLeft = new Point(Opos.X - radius, Opos.Y - radius);
            topRight = new Point(Opos.X + radius, Opos.Y - radius);
            bottomLeft = new Point(Opos.X - radius, Opos.Y + radius);
            bottomRight = new Point(Opos.X + radius, Opos.Y + radius);

            DrawCircle();
            DrawOCircle();
            DrawDigit();
            DrawGridLine();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新当前时间
            CurrTime = DateTime.Now;
            // 更新圆盘时针
            Update();
        }

        /// <summary>
        /// 画表盘外圆
        /// </summary>
        private void DrawCircle()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.DarkGray;
            ellipse.StrokeThickness = 4;
            ellipse.Width = diameter - 10;
            ellipse.Height = diameter - 10;
            ellipse.Fill = Brushes.Gray;

            Canvas.SetLeft(ellipse, topLeft.X + 5);
            Canvas.SetTop(ellipse, topLeft.Y + 5);
            AnalogCanvs.Children.Add(ellipse);

            Ellipse ellipse1 = new Ellipse();
            ellipse1.Stroke = Brushes.Gray;
            ellipse1.StrokeThickness = 2;
            ellipse1.Width = diameter;
            ellipse1.Height = diameter;

            Canvas.SetLeft(ellipse1, topLeft.X);
            Canvas.SetTop(ellipse1, topLeft.Y);
            AnalogCanvs.Children.Add(ellipse1);
        }

        /// <summary>
        /// 圆形表心圆
        /// </summary>
        private void DrawOCircle()
        {
            Ellipse ellipseO = new Ellipse();
            ellipseO.Width = 30;
            ellipseO.Height = 30;
            ellipseO.Fill = Brushes.DarkGray;

            Canvas.SetLeft(ellipseO, Opos.X - 15);
            Canvas.SetTop(ellipseO, Opos.Y - 15);

            if (AnalogCanvs.Children.Contains(ellipseO))
                AnalogCanvs.Children.Remove(ellipseO);
            AnalogCanvs.Children.Add(ellipseO);
        }

        /// <summary>
        /// 画圆表盘数字
        /// </summary>
        private void DrawDigit()
        {
            double x, y;
            for (int i = 1; i < 13; i++)
            {
                angle = WrapAngle(i * 360.0 / 12.0) - 90.0;
                angle = ConvertDegreesToRadians(angle);

                x = Opos.X + Math.Cos(angle) * (radius - 36) - 8;
                y = Opos.Y + Math.Sin(angle) * (radius - 36) - 15;

                TextBlock digit = new TextBlock();
                digit.FontSize = 26;
                digit.Text = i.ToString();

                // 数字12位置校正
                if (i == 12)
                {
                    Canvas.SetLeft(digit, x - 8);
                }
                else
                {
                    Canvas.SetLeft(digit, x);
                }
                Canvas.SetTop(digit, y);
                AnalogCanvs.Children.Add(digit);
            }
        }
        /// <summary>
        /// 画圆表刻度
        /// </summary>
        private void DrawGridLine()
        {
            double x1 = 0, y1 = 0;
            double x2 = 0, y2 = 0;

            for (int i = 0; i < 60; i++)
            {
                double angle1 = WrapAngle(i * 360.0 / 60.0) - 90;
                angle1 = ConvertDegreesToRadians(angle1);

                if (i % 5 == 0)
                {
                    x1 = Math.Cos(angle1) * (radius - 20);
                    y1 = Math.Sin(angle1) * (radius - 20);
                }
                else
                {
                    x1 = Math.Cos(angle1) * (radius - 10);
                    y1 = Math.Sin(angle1) * (radius - 10);
                }

                x2 = Math.Cos(angle1) * (radius - 5);
                y2 = Math.Sin(angle1) * (radius - 5);

                Line line = new Line();
                line.X1 = x1;
                line.Y1 = y1;
                line.X2 = x2;
                line.Y2 = y2;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 3;

                Canvas.SetLeft(line, Opos.X);
                Canvas.SetTop(line, Opos.Y);
                AnalogCanvs.Children.Add(line);

            }
        }
        /// <summary>
        /// 画时针
        /// </summary>
        private void DrawHourLine()
        {
            int hour = CurrTime.Hour;
            int minu = CurrTime.Minute;
            double dminu = minu / 60.0;         // 根据分钟数增加时针偏移
            double dhour = hour + dminu;

            double hour_angle = WrapAngle(dhour * (360.0 / 12.0) - 90.0);
            hour_angle = ConvertDegreesToRadians(hour_angle);

            double x = Math.Cos(hour_angle) * (radius - 100);
            double y = Math.Sin(hour_angle) * (radius - 100);

            HourLine.X1 = 0;
            HourLine.Y1 = 0;
            HourLine.X2 = x;
            HourLine.Y2 = y;
            HourLine.Stroke = Brushes.Black;
            HourLine.StrokeThickness = 16;

            Canvas.SetLeft(HourLine, Opos.X);
            Canvas.SetTop(HourLine, Opos.Y);
            if (AnalogCanvs.Children.Contains(HourLine))
            {
                AnalogCanvs.Children.Remove(HourLine);
            }
            AnalogCanvs.Children.Add(HourLine);
        }
        /// <summary>
        /// 画秒针
        /// </summary>
        private void DrawSecondLine()
        {
            int second = CurrTime.Second;

            // 秒针正方向点
            double se_angle = WrapAngle(second * (360.0 / 60.0) - 90);
            se_angle = ConvertDegreesToRadians(se_angle);
            double sec_x = Math.Cos(se_angle) * (radius - 40);
            double sec_y = Math.Sin(se_angle) * (radius - 40);

            // 秒针反方向点
            se_angle = WrapAngle(second * (360.0 / 60.0) + 90);
            se_angle = ConvertDegreesToRadians(se_angle);
            double sec_x_ = Math.Cos(se_angle) * (radius - 180);
            double sec_y_ = Math.Sin(se_angle) * (radius - 180);

            SecdLine.X1 = sec_x_;
            SecdLine.Y1 = sec_y_;
            SecdLine.X2 = sec_x;
            SecdLine.Y2 = sec_y;
            SecdLine.Stroke = Brushes.Red;
            SecdLine.StrokeThickness = 4;

            Canvas.SetLeft(SecdLine, Opos.X);
            Canvas.SetTop(SecdLine, Opos.Y);
            if (AnalogCanvs.Children.Contains(SecdLine))
            {
                AnalogCanvs.Children.Remove(SecdLine);
            }
            AnalogCanvs.Children.Add(SecdLine);
        }
        /// <summary>
        /// 角度360度进制
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private double WrapAngle(double angle)
        {
            return angle % 360;
        }

        /// <summary>
        /// 将角度转为弧度
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        private double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;

            return radians;
        }
        /// <summary>
        /// 更新小时针、分针、秒针
        /// </summary>
        private void Update()
        {
            DrawHourLine();
            DrawSecondLine();
            DrawOCircle();
        }
    }
}
