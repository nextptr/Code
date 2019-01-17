using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Thread localThread = null;

        public bool IsWork = false;
        public bool IsExist = false;
        public int Count;
        public Cells(int count)
        {
            localThread = new Thread(Action);
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

        private void Action() //线程函数
        {
            while (IsExist)
            {
                while (true == IsWork)
                {
                    Calculate();
                    Flash();
                    Thread.Sleep(300);
                }
            }
        }

        public void Flash()   //一次刷新
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
            IsExist = true;
            IsWork = true;
            if (!localThread.IsAlive)
            {
                localThread.Start();
            }
            else
            {
                Resume();
            }
        }
        public void Suspand()
        {
            IsWork = false;
        }
        public void Resume()
        {
            IsWork = true;
        }
        public void Stop()
        {
            IsWork = false;
            IsExist = false;
            if (localThread != null)
            {
                localThread.Abort();
            }
        }

        public void SetPattern(Pattern pattern)
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    cells[i][i].LiveNow = pattern.bitMaps[i][j];
                }
            }
        }

        public void ReSet() //重置
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

        public void setMode(int mod)
        {
            int center = Count / 2;
            int i = 0;
            switch (mod)
            {
                case 1:
                {
                    for (i = center - 7; i <= center + 7; i++)
                    {
                        cells[center][i].LiveNow = true;
                    }
                    cells[center][center - 2].LiveNow = false;
                    cells[center][center + 2].LiveNow = false;
                    cells[center][center + 3].LiveNow = false;
                }
                break;
                case 2:
                {
                    for (i = center - 4; i <= center + 5; i++)
                    {
                        cells[center][i].LiveNow = true;
                    }
                    cells[center][center - 2].LiveNow = false;
                    cells[center][center + 3].LiveNow = false;
                    cells[center-1][center - 2].LiveNow = true;
                    cells[center-1][center + 3].LiveNow = true;
                    cells[center+1][center - 2].LiveNow = true;
                    cells[center+1][center + 3].LiveNow = true;
                }
                break;
                case 3:
                {
                    for (i = center - 13; i <= center + 13; i++)
                    {
                        cells[center][i].LiveNow = true;
                    }
                    cells[center][center - 4].LiveNow = false;
                    cells[center][center + 4].LiveNow = false;
                    cells[center][center + 5].LiveNow = false;
                    cells[center][center + 6].LiveNow = false;
                }
                break;
                case 4:
                {
                    cells[center - 1][center + 1].LiveNow = true;
                    cells[center][center-1].LiveNow = true;
                    cells[center][center].LiveNow = true;
                    cells[center][center+1].LiveNow = true;
                    cells[center+1][center].LiveNow = true;
                }
                break;
                case 5:
                {
                    cells[center-1][center-1].LiveNow = true;
                    cells[center-1][center].LiveNow = true;
                    cells[center][center].LiveNow = true;
                    cells[center][center+1].LiveNow = true;
                    cells[center][center+2].LiveNow = true;
                    cells[center+1][center+1].LiveNow = true;
                }
                break;
                case 6:
                {
                    cells[center-1][center+1].LiveNow = true;
                    cells[center][center].LiveNow = true;
                    cells[center][center+1].LiveNow = true;
                    cells[center+1][center-1].LiveNow = true;
                    cells[center+1][center].LiveNow = true;
                    cells[center+2][center-1].LiveNow = true;
                }
                break;
                case 7:
                {
                    cells[center-2][center-2].LiveNow = true;
                    cells[center-2][center-1].LiveNow = true;
                    cells[center-2][center].LiveNow = true;
                    cells[center-2][center+1].LiveNow = true;
                    cells[center-1][center-2].LiveNow = true;
                    cells[center-1][center].LiveNow = true;
                    cells[center-1][center+1].LiveNow = true;
                    cells[center][center-2].LiveNow = true;
                    cells[center+1][center-2].LiveNow = true;
                    cells[center+1][center+1].LiveNow = true;
                    cells[center+2][center-2].LiveNow = true;
                }
                break;
                case 8:
                {
                    cells[center - 2][center - 2].LiveNow = true;
                    cells[center - 2][center - 1].LiveNow = true;
                    cells[center - 2][center].LiveNow = true;
                    cells[center - 2][center + 1].LiveNow = true;
                    cells[center - 1][center - 2].LiveNow = true;
                    cells[center - 1][center].LiveNow = true;
                    cells[center - 1][center + 1].LiveNow = true;
                    cells[center][center - 2].LiveNow = true;
                    cells[center + 1][center - 2].LiveNow = true;
                    cells[center + 1][center - 1].LiveNow = true;
                    cells[center + 2][center - 1].LiveNow = true;
                    cells[center + 2][center].LiveNow = true;
                   
                }
                break;
                case 9:
                {
                    cells[center-2][center-2].LiveNow = true;
                    cells[center-2][center-1].LiveNow = true;
                    cells[center-2][center+1].LiveNow = true;
                    cells[center-1][center-2].LiveNow = true;
                    cells[center][center-1].LiveNow = true;
                    cells[center][center+1].LiveNow = true;
                    cells[center+1][center-2].LiveNow = true;
                    cells[center+1][center-1].LiveNow = true;
                    cells[center+1][center+1].LiveNow = true;
                    cells[center+2][center+1].LiveNow = true;
                }
                break;
                case 10:
                {
                    cells[center-1][center-1].LiveNow = true;
                    cells[center-1][center].LiveNow = true;
                    cells[center-1][center+1].LiveNow = true;
                    cells[center][center].LiveNow = true;
                    cells[center+1][center-1].LiveNow = true;
                    cells[center+1][center].LiveNow = true;
                    cells[center+1][center+1].LiveNow = true;

                    cells[center+1][center+2].LiveNow = true;
                    cells[center+2][center+2].LiveNow = true;
                }
                break;
                case 11:
                {
                    cells[center - 1][center - 1].LiveNow = true;
                    cells[center - 1][center].LiveNow = true;
                    cells[center - 1][center + 1].LiveNow = true;
                    cells[center][center].LiveNow = true;
                    cells[center + 1][center - 1].LiveNow = true;
                    cells[center + 1][center].LiveNow = true;
                    cells[center + 1][center + 1].LiveNow = true;
                    cells[center + 1][center + 2].LiveNow = true;
                    cells[center + 2][center - 1].LiveNow = true;
                    cells[center + 2][center + 2].LiveNow = true;
                }
                break;
                case 12:
                {
                    cells[center - 1][center - 1].LiveNow = true;
                    cells[center - 1][center].LiveNow = true;
                    cells[center - 1][center + 1].LiveNow = true;
                    cells[center][center].LiveNow = true;
                    cells[center + 1][center].LiveNow = true;
                    cells[center + 1][center + 1].LiveNow = true;
                    cells[center + 1][center + 2].LiveNow = true;
                    cells[center + 2][center].LiveNow = true;
                    cells[center + 2][center + 2].LiveNow = true;
                }
                break;
                case 13:
                {
                    cells[center][center+1].LiveNow = true;
                    cells[center+1][center].LiveNow = true;
                    cells[center+2][center].LiveNow = true;
                    cells[center+2][center+3].LiveNow = true;
                    cells[center+3][center].LiveNow = true;
                    cells[center+3][center+2].LiveNow = true;
                    cells[center+3][center+3].LiveNow = true;

                }
                break;
                case 14:
                {
                    cells[center][center].LiveNow = true;
                    cells[center][center+2].LiveNow = true;
                    cells[center+1][center+1].LiveNow = true;
                    cells[center+1][center+3].LiveNow = true;
                    cells[center+2][center].LiveNow = true;
                    cells[center+2][center+1].LiveNow = true;
                    cells[center+2][center+3].LiveNow = true;
                    cells[center+3][center].LiveNow = true;
                    cells[center+3][center+3].LiveNow = true;
                }
                break;
                case 15:
                {
                    cells[center][center].LiveNow = true;
                    cells[center][center+2].LiveNow = true;
                    cells[center+1][center+2].LiveNow = true;
                    cells[center+1][center+3].LiveNow = true;
                    cells[center+2][center+1].LiveNow = true;
                    cells[center+3][center].LiveNow = true;
                    cells[center+3][center+1].LiveNow = true;
                    cells[center+3][center+2].LiveNow = true;
                    cells[center+3][center+3].LiveNow = true;

                }
                break;
            }

        }
        public void StepTo(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Calculate();
            }
            Flash();
        }

        private void Calculate() //存活计算
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
        private void core(int x,int y ,int num) //存活条件
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
