using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfLifeGame
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class cell : NotifyPropertyChanged
    {
        public bool LiveNext;
        protected bool _now;
        public cell()
        {
            LiveNext = false;
            _now = false;
        }

        public bool LiveNow
        {
            set
            {
                _now = value; OnPropertyChanged("LiveNow");

            }
            get
            {
                return _now;
            }
        }
    }

    

    public class Cells
    {
        public List<List<cell>> cells = new List<List<cell>>();
        private DispatcherTimer _time = new DispatcherTimer();


        public bool IsWork = false;
        public int Count;
        public Cells(int count)
        {
            //计时器
            _time.Tick += _time_Tick; ;
            _time.Interval = new TimeSpan(0, 0, 0, 0, 300);
            _time.Start();
            //cell
            Count = count;
            for (int i = 0; i < count; i++)
            {
                cells.Add(new List<cell>());
                for (int j = 0; j < count; j++)
                {
                    cells[i].Add(new cell());
                }
            }
        }

        private void _time_Tick(object sender, EventArgs e)
        {
            if (true == IsWork)
            {
                Computer();
                Flash();
            }
        }

        public void Flash()
        {
            for(int i = 0;i<Count;i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    cells[i][j].LiveNow = cells[i][j].LiveNext;
                }
            }
        }

        public void Start()
        {
            IsWork = true;
        }

        public void Stop()
        {
            IsWork = false;
        }

        public void ReSet()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    cells[i][j].LiveNow = false;
                    cells[i][j].LiveNext = false;
                }
            }
        }

        private void Computer()
        {
            int row = 0;
            int col = 0;
            int naber = 0;
            //00点
            if (cells[0][1].LiveNow)
            {
                naber++;
            }
            if (cells[1][1].LiveNow)
            {
                naber++;
            }
            if (cells[1][0].LiveNow)
            {
                naber++;
            }
            core(0, 0, naber);
            //01点
            naber = 0;
            if (cells[0][Count - 2].LiveNow)
            {
                naber++;
            }
            if (cells[1][Count - 2].LiveNow)
            {
                naber++;
            }
            if (cells[1][Count - 1].LiveNow)
            {
                naber++;
            }
            core(0, Count-1, naber);
            //10
            naber = 0;
            if (cells[Count - 2][0].LiveNow)
            {
                naber++;
            }
            if (cells[Count - 2][1].LiveNow)
            {
                naber++;
            }
            if (cells[Count - 1][1].LiveNow)
            {
                naber++;
            }
            core(Count-1, 0, naber);
            //11
            naber = 0;
            if (cells[Count - 1][Count - 2].LiveNow)
            {
                naber++;
            }
            if (cells[Count - 2][Count - 2].LiveNow)
            {
                naber++;
            }
            if (cells[Count - 2][Count - 1].LiveNow)
            {
                naber++;
            }
            core(Count-1, Count-1, naber);
            //上边
            for (int i = 1; i < Count - 1; i++)
            {
                naber = 0;
                if (cells[0][i - 1].LiveNow)
                {
                    naber++;
                }
                if (cells[0][i + 1].LiveNow)
                {
                    naber++;
                }
                if (cells[1][i - 1].LiveNow)
                {
                    naber++;
                }
                if (cells[1][i].LiveNow)
                {
                    naber++;
                }
                if (cells[1][i + 1].LiveNow)
                {
                    naber++;
                }
                core(0, i, naber);
            }
            //下边
            for (int i = 1; i < Count - 1; i++)
            {
                naber = 0;
                if (cells[Count-1][i - 1].LiveNow)
                {
                    naber++;
                }
                if (cells[Count - 1][i + 1].LiveNow)
                {
                    naber++;
                }
                if (cells[Count - 2][i - 1].LiveNow)
                {
                    naber++;
                }
                if (cells[Count - 2][i].LiveNow)
                {
                    naber++;
                }
                if (cells[Count - 2][i + 1].LiveNow)
                {
                    naber++;
                }
                core(Count-1, i, naber);
            }
            //左边
            for (int i = 1; i < Count - 1; i++)
            {
                naber = 0;
                if (cells[i - 1][0].LiveNow)
                {
                    naber++;
                }
                if (cells[i + 1][0].LiveNow)
                {
                    naber++;
                }
                if (cells[i - 1][1].LiveNow)
                {
                    naber++;
                }
                if (cells[i][1].LiveNow)
                {
                    naber++;
                }
                if (cells[i + 1][1].LiveNow)
                {
                    naber++;
                }
                core(i, 0, naber);
            }
            //右边
            for (int i = 1; i < Count - 1; i++)
            {
                naber = 0;
                if (cells[i - 1][Count-1].LiveNow)
                {
                    naber++;
                }
                if (cells[i + 1][Count - 1].LiveNow)
                {
                    naber++;
                }
                if (cells[i - 1][Count - 2].LiveNow)
                {
                    naber++;
                }
                if (cells[i][Count - 2].LiveNow)
                {
                    naber++;
                }
                if (cells[i + 1][Count - 2].LiveNow)
                {
                    naber++;
                }

                core(i, Count-1, naber);
            }
            //正常
            for (row=1; row < Count-1; row++)
            {
                for (col = 1; col < Count-1; col++)
                {
                    naber = 0;
                    if (cells[row - 1][col - 1].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row - 1][col].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row - 1][col + 1].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row][col - 1].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row][col + 1].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row + 1][col - 1].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row + 1][col].LiveNow)
                    {
                        naber++;
                    }
                    if (cells[row + 1][col + 1].LiveNow)
                    {
                        naber++;
                    }
                    core(row, col, naber);
                }
            }
        }

        private void core(int x,int y ,int num)
        {
            if (num == 2)
            {
                cells[x][y].LiveNext = cells[x][y].LiveNow;
            }
            else if (num == 3)
            {
                cells[x][y].LiveNext = true;
            }
            else
            {
                cells[x][y].LiveNext = false;
            }
        }
    }
}
