using Common;
using Dxf;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Parameters.DXF
{
    public class DxfItem:NotifyPropertyChanged, IParameterItem
    {
        private string _recipeName = "";
        private string _recipeDate = "";
        private List<PATH> _recipePath = new List<PATH>();

        public double Max_x = 0.0;
        public double Max_y = 0.0;

        public string RecipeName
        {
            get { return _recipeName; }
            set { if (_recipeName == value) { return; } _recipeName = value; OnPropertyChanged("RecipeName"); }
        }
        public string RecipeDate
        {
            get { return _recipeDate; }
            set { if (_recipeDate == value) { return; } _recipeDate = value; OnPropertyChanged("RecipeDate"); }
        }
        public List<PATH> RecipePath
        {
            get
            {
                return _recipePath;
            }
            set
            {
                _recipePath.Clear();
                for (int i = 0; i < value.Count; i++)
                {
                    _recipePath.Add(value[i].Clon());
                }
            }
        }


        public IParameterItem Clone()
        {
            DxfItem clone = new DxfItem();
            clone.RecipeName = this.RecipeName;
            clone.RecipeDate = this.RecipeDate;
            foreach (PATH th in this.RecipePath)
            {
                clone.RecipePath.Add(th.Clon());
            }
            clone.Max_x = this.Max_x;
            clone.Max_y = this.Max_y;
            return clone;
        }
        public void Copy(IParameterItem other)
        {
            DxfItem ot = other as DxfItem;
            if (ot == null)
            {
                return;
            }
            else
            {
                this.RecipeName = ot.RecipeName;
                this.RecipeDate = ot.RecipeDate;
                foreach (PATH th in ot.RecipePath)
                {
                    this.RecipePath.Add(th.Clon());
                }
                this.Max_x = ot.Max_x;
                this.Max_y = ot.Max_y;
            }
        }

        private PATH SortPolyLine(PATH pth)
        {
            int index = 0;
            int count = 0;
            double mid_x = 0.0;
            double mid_y = 0.0;
            PATH ret = new PATH();
            ret.CopyFrom(pth);
            List<Point> ls_src = new List<Point>();
            List<Point> ls_th = new List<Point>();
            List<Point> ls_cent = new List<Point>();
            List<int> ls_tp = new List<int>();
            foreach (Point tmp in ret.throughPoints)
            {
                ls_src.Add(new Point(tmp.X, tmp.Y));
                count++;
            }
            //闭合多线段去除重合终点
            if ((ls_src[count - 1].X == ls_src[0].X) && (ls_src[count - 1].Y == ls_src[0].Y))
            {
                ls_src.RemoveAt(count - 1);
                ret.centerPoints.RemoveAt(count - 1);
                ret.types.RemoveAt(count - 1);
                //找到直线的位置
                List<int> idx = new List<int>();
                List<int> idy = new List<int>();
                for (int i = 0; i < ls_src.Count - 2; i++)
                {
                    if (ret.types[i] == 0)
                    {
                        if (Math.Round(ls_src[i].Y, 4) == Math.Round(ls_src[i + 1].Y, 4))
                        {
                            idy.Add(i);
                        }
                        if (Math.Round(ls_src[i].X, 4) == Math.Round(ls_src[i + 1].X, 4))
                        {
                            idx.Add(i);
                        }
                    }
                }
                //找到最大直线的位置
                double maxLen = 0.0;
                foreach (int id in idx)
                {
                    double tmplen = Math.Abs(ls_src[id].X - ls_src[id + 1].X);
                    if (tmplen > maxLen)
                    {
                        maxLen = tmplen;
                        index = id;
                    }
                }
                foreach (int id in idy)
                {
                    double tmplen = Math.Abs(ls_src[id].Y - ls_src[id + 1].Y);
                    if (tmplen > maxLen)
                    {
                        maxLen = tmplen;
                        index = id;
                    }
                }
                //在直线中插入两个分割点
                mid_x = (ls_src[index].X + ls_src[index + 1].X) / 2;
                mid_y = (ls_src[index].Y + ls_src[index + 1].Y) / 2;
                ls_th.Add(new Point(mid_x, mid_y));
                ls_cent.Add(new Point(0.0, 0.0));
                ls_tp.Add(0);
                for (int i = index + 1; i < ls_src.Count; i++)
                {
                    ls_th.Add(new Point(ls_src[i].X, ls_src[i].Y));
                    ls_cent.Add(ret.centerPoints[i]);
                    ls_tp.Add(ret.types[i]);
                }
                for (int i = 0; i <= index; i++)
                {
                    ls_th.Add(new Point(ls_src[i].X, ls_src[i].Y));
                    ls_cent.Add(ret.centerPoints[i]);
                    ls_tp.Add(ret.types[i]);
                }
                ls_th.Add(new Point(mid_x, mid_y));
                ls_cent.Add(new Point(0.0, 0.0));
                ls_tp.Add(0);
            }
            else
            {
                for (int i = 0; i < ls_src.Count; i++)
                {
                    ls_th.Add(new Point(ls_src[i].X, ls_src[i].Y));
                    ls_cent.Add(ret.centerPoints[i]);
                    ls_tp.Add(ret.types[i]);
                }
            }
            //折线多点排序分割完成
            ret.throughPoints.Clear();
            ret.centerPoints.Clear();
            ret.types.Clear();
            foreach (Point tmp in ls_th)
            {
                ret.throughPoints.Add(tmp);
            }
            foreach (var tmp in ls_cent)
            {
                ret.centerPoints.Add(tmp);
            }
            foreach (var tmp in ls_tp)
            {
                ret.types.Add(tmp);
            }
            return ret;
        }
    }
    public class DxfParameter:IParameter
    {
        private List<DxfItem> _data = null;
        public List<DxfItem> Data
        {
            get
            {
                if (this.Count <= 1)
                {
                    return null;
                }
                if (_data == null)
                {
                    _data = new List<DxfItem>();
                    for (int i = 1; i < this.Count; i++)
                    {
                        _data.Add(this[i] as DxfItem);
                    }
                }
                else
                {
                    _data.Clear();
                    for (int i = 1; i < this.Count; i++)
                    {
                        _data.Add(this[i] as DxfItem);
                    }
                }
                return _data;
            }
        }
        public override void Create()
        {
            DxfItem itm = new DxfItem();
            itm.RecipeName = "占位dxf";
            itm.RecipeDate = "123";
            itm.RecipePath = new List<PATH>();
            itm.Max_x = 0.0;
            itm.Max_y = 0.0;
            this.Add(itm);
        }
    }
}
