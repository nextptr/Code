using Microsoft.Win32;
using Parameters;
using Parameters.DXF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Dxf
{
    /// <summary>
    /// DxfWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DxfWindow:UserControl
    {
        private double act_wid = 0.0;
        private double act_hei = 0.0;
        double max_x = 0.0;
        double max_y = 0.0;
        private List<PATH> dxfPath = null;

        private static int wheelCount = 0;     //滚轮放大倍数
        private static int wheelMaxCount = 20; //滚轮放大最大倍数
        private double center_w;               //旋转中心
        private double center_h;

        private int selectRecIndex = -1;
        private DxfItem selectRceItem = null;



        public DxfWindow()
        {
            InitializeComponent();
            this.Loaded += DxfWindow_Loaded;

            btn_AddRecipe.Click += Btn_AddRecipe_Click;
            btn_DelRecipe.Click += Btn_DelRecipe_Click;

            cav_board.MouseWheel += Cav_board_MouseWheel;
            dg_recipe.SelectionChanged += Dg_recipe_SelectionChanged;
        }
        private void DxfWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsVisible)
            {
                this.Loaded -= DxfWindow_Loaded;
                act_wid = cav_board.ActualWidth;
                act_hei = cav_board.ActualHeight;
                double len = act_hei < act_wid ? act_hei : act_wid;
                center_w = len / 2;
                center_h = len / 2;

                dg_recipe.ItemsSource = ParameterInstance.Instance.dxfParameter.Data;
            }
        }

        private void Dg_recipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParameterInstance.Instance.dxfParameter.Data.Count < 1)
            {
                return;
            }
            selectRecIndex = dg_recipe.SelectedIndex;
            if (selectRecIndex < 0)
            {
                return;
            }
            selectRceItem = ParameterInstance.Instance.dxfParameter.Data[selectRecIndex];
            PaintingPath(selectRceItem.RecipePath, selectRceItem.Max_x, selectRceItem.Max_y);
        }

        private void Btn_AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog().Value)
            {
                //读取dxf，获取路径和文件名
                string path = openFileDialog1.FileName;
                string[] arr = openFileDialog1.SafeFileName.Split('.');
                string name = arr[0];
                DxfReader.Instance.Read(path);
                if (DxfReader.Instance.PathList.Count < 0)
                {
                    MessageBox.Show("dxf文件读取失败");
                    return;
                }
                List<PATH> ls = null;
                DxfReader.Instance.TransformToOneQuadrant(DxfReader.Instance.PathList, ref ls, ref max_x, ref max_y);
                //获取时间
                string tim = DateTime.Now.ToString("yyMMdd HH:mm");
                //保存参数
                DxfItem item = new DxfItem();
                item.RecipeName = name;
                item.RecipeDate = tim;
                item.RecipePath = ls;

                item.Max_x = max_x;
                item.Max_y = max_y;
                ParameterInstance.Instance.dxfParameter.Add(item);
                //绘图
                PaintingPath(ls, max_x, max_y);
                int index = selectRecIndex;
                dg_recipe.ItemsSource = null;
                dg_recipe.ItemsSource = ParameterInstance.Instance.dxfParameter.Data;
                dg_recipe.SelectedIndex = index + 1;
            }
        }
        private void Btn_DelRecipe_Click(object sender, RoutedEventArgs e)
        {
            if ((selectRecIndex != -1) && (ParameterInstance.Instance.dxfParameter.Data.Count >= 1))
            {
                ParameterInstance.Instance.dxfParameter.RemoveAt(selectRecIndex + 1);
            }
            int index = selectRecIndex;
            dg_recipe.ItemsSource = null;
            dg_recipe.ItemsSource = ParameterInstance.Instance.dxfParameter.Data;
            if (index <= ParameterInstance.Instance.dxfParameter.Data.Count - 1)
            {
                dg_recipe.SelectedIndex = index;
            }
            else
            {
                if (index - 1 >= 0)
                {
                    dg_recipe.SelectedIndex = index - 1;
                }
            }
        }

        private void ScalaPath(ref List<PATH> ls, double max_x, double max_y, double sca_len)
        {
            double max_len = max_x;
            if (max_x < max_y)
            {
                max_len = max_y;
            }
            foreach (PATH pth in ls)
            {
                if (pth.ePathType == EPathType.Line)
                {
                    pth.StartX = (pth.StartX / max_len) * sca_len;
                    pth.StartY = (pth.StartY / max_len) * sca_len;
                    pth.EndX = (pth.EndX / max_len) * sca_len;
                    pth.EndY = (pth.EndY / max_len) * sca_len;
                }
                if (pth.ePathType == EPathType.Lwpolyline)
                {
                    for (int i = 0; i < pth.throughPoints.Count; i++)
                    {
                        Point pos = new Point();
                        pos.X = (pth.throughPoints[i].X / max_len) * sca_len;
                        pos.Y = (pth.throughPoints[i].Y / max_len) * sca_len;
                        pth.throughPoints[i] = pos;
                    }
                }
                if (pth.ePathType == EPathType.Arc)
                {
                    pth.CenterX = (pth.CenterX / max_len) * sca_len;
                    pth.CenterY = (pth.CenterY / max_len) * sca_len;
                    pth.StartX = (pth.StartX / max_len) * sca_len;
                    pth.StartY = (pth.StartY / max_len) * sca_len;
                    pth.EndX = (pth.EndX / max_len) * sca_len;
                    pth.EndY = (pth.EndY / max_len) * sca_len;
                    pth.Radio = (pth.Radio / max_len) * sca_len;
                }
            }
        }
        private void ScalaPath1(ref ArrayList ls, double max_x, double max_y, double sca_len)
        {
            foreach (var tmp in ls)
            {
                PATH pth = tmp as PATH;
                if (pth.ePathType == EPathType.Line)
                {
                    pth.StartX = (pth.StartX / max_x) * sca_len;
                    pth.StartY = (pth.StartY / max_y) * sca_len;
                    pth.EndX = (pth.EndX / max_x) * sca_len;
                    pth.EndY = (pth.EndY / max_y) * sca_len;
                }
                if (pth.ePathType == EPathType.Lwpolyline)
                {
                    for (int i = 0; i < pth.throughPoints.Count; i++)
                    {
                        Point pos = new Point();
                        pos.X = (pth.throughPoints[i].X / max_x) * sca_len;
                        pos.Y = (pth.throughPoints[i].Y / max_y) * sca_len;
                        pth.throughPoints[i] = pos;
                    }
                }
            }
        }
        private void OffsetPath(ref List<PATH> ls, double offset_x, double offset_y)
        {
            foreach (var pth in ls)
            {
                if (pth.ePathType == EPathType.Line)
                {
                    pth.StartX = pth.StartX + offset_x;
                    pth.StartY = pth.StartY + offset_y;
                    pth.EndX = pth.EndX + offset_x;
                    pth.EndY = pth.EndY + offset_y;
                }
                if (pth.ePathType == EPathType.Lwpolyline)
                {
                    for (int i = 0; i < pth.throughPoints.Count; i++)
                    {
                        Point pos = new Point();
                        pos.X = pth.throughPoints[i].X + offset_x;
                        pos.Y = pth.throughPoints[i].Y + offset_y;
                        pth.throughPoints[i] = pos;
                    }
                }
                if (pth.ePathType == EPathType.Arc)
                {
                    pth.CenterX = pth.CenterX + offset_x;
                    pth.CenterY = pth.CenterY + offset_y;
                    pth.StartX = pth.StartX + offset_x;
                    pth.StartY = pth.StartY + offset_y;
                    pth.EndX = pth.EndX + offset_x;
                    pth.EndY = pth.EndY + offset_y;
                }
            }
        }
        private void OffsetPath(ref List<PATH> ls, List<PATH> ori, Point setpos)
        {
            ls = new List<PATH>();
            foreach (PATH pth in ori)
            {
                if (pth.ePathType == EPathType.Line)
                {
                    PATH tmp = new PATH();
                    tmp.CopyFrom(pth);
                    tmp.StartX = pth.StartX + setpos.X;
                    tmp.StartY = pth.StartY + setpos.Y;
                    tmp.EndX = pth.EndX + setpos.X;
                    tmp.EndY = pth.EndY + setpos.Y;
                    ls.Add(tmp);
                }
                if (pth.ePathType == EPathType.Lwpolyline)
                {
                    PATH tmp = new PATH();
                    tmp.CopyFrom(pth);
                    for (int i = 0; i < pth.throughPoints.Count; i++)
                    {
                        Point pos = new Point();
                        pos.X = pth.throughPoints[i].X + setpos.X;
                        pos.Y = pth.throughPoints[i].Y + setpos.Y;
                        tmp.throughPoints[i] = pos;
                    }
                    ls.Add(tmp);
                }
            }
        }

        private void PaintingPath(List<PATH> ls, double max_x, double max_y)
        {
            cav_board.Children.Clear();

            List<PATH> paintPth = new List<PATH>();
            foreach (PATH th in ls)
            {
                paintPth.Add(th.Clon());
            }

            if (act_hei < act_wid)
            {
                ScalaPath(ref paintPth, max_x, max_y, act_hei - 10);
            }
            else
            {
                ScalaPath(ref paintPth, max_x, max_y, act_wid - 10);
            }

            OffsetPath(ref paintPth, 5, 5);
            foreach (var tmp in paintPth)
            {
                PATH pth;
                pth = tmp as PATH;
                if (pth.ePathType == EPathType.Line)
                {
                    Polyline lin = new Polyline();
                    lin.Stroke = Brushes.White;
                    lin.StrokeThickness = 1;
                    lin.Points.Add(new Point(pth.StartX, pth.StartY));
                    lin.Points.Add(new Point(pth.EndX, pth.EndY));
                    cav_board.Children.Add(lin);
                }
                if (pth.ePathType == EPathType.Lwpolyline)
                {
                    Polyline lin = new Polyline();
                    lin.Stroke = Brushes.White;
                    lin.StrokeThickness = 1;
                    foreach (Point pos in pth.throughPoints)
                    {
                        lin.Points.Add(pos);
                    }
                    cav_board.Children.Add(lin);
                }
                if (pth.ePathType == EPathType.Arc)
                {
                    Path path = new Path();
                    PathGeometry pathGeometry = new PathGeometry();
                    ArcSegment arc = null;
                    if (pth.StartAngle > pth.EndAngle)
                    {
                        if (360 - pth.StartAngle + pth.EndAngle < 180)
                        {
                            arc = new ArcSegment(new Point(pth.EndX, pth.EndY), new Size(pth.Radio, pth.Radio), 0, false, SweepDirection.Clockwise, true);
                        }
                        else
                        {
                            arc = new ArcSegment(new Point(pth.EndX, pth.EndY), new Size(pth.Radio, pth.Radio), 0, true, SweepDirection.Clockwise, true);
                        }
                    }
                    else
                    {
                        if (pth.EndAngle - pth.StartAngle < 180)
                        {
                            arc = new ArcSegment(new Point(pth.EndX, pth.EndY), new Size(pth.Radio, pth.Radio), 0, false, SweepDirection.Clockwise, true);
                        }
                        else
                        {
                            arc = new ArcSegment(new Point(pth.EndX, pth.EndY), new Size(pth.Radio, pth.Radio), 0, true, SweepDirection.Clockwise, true);
                        }
                    }
                    PathFigure figure = new PathFigure();
                    figure.StartPoint = new Point(pth.StartX, pth.StartY);
                    figure.Segments.Add(arc);
                    pathGeometry.Figures.Add(figure);
                    path.Data = pathGeometry;
                    path.Stroke = Brushes.White;
                    path.StrokeThickness = 1;
                    cav_board.Children.Add(path);
                }
            }
        }
        private void Cav_board_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point center = e.GetPosition(cav_board);
            ScaleTransform st;
            if (e.Delta > 0)
            {
                wheelCount++;
                if (wheelCount > wheelMaxCount)
                {
                    wheelCount = wheelMaxCount;
                }
                st = new ScaleTransform(1 + wheelCount * 1, 1 + wheelCount * 1, center.X, center.Y);
                cav_board.RenderTransform = st;
            }
            else
            {
                wheelCount--;
                if (wheelCount < 0)
                {
                    wheelCount = 0;
                    st = new ScaleTransform(1, 1, center_w, center_h);
                }
                else
                {
                    st = new ScaleTransform(1 + wheelCount * 1, 1 + wheelCount * 1, center.X, center.Y);
                }
                cav_board.RenderTransform = st;
            }
        }
    }
}
