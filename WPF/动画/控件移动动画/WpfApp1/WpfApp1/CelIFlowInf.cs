using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class CelIFlowInf : NotifyPropertyChanged
    {
        private bool _inPos = false;
        private int _cellid = -1;


        public bool InPosition
        {
            get
            {
                return _inPos;
            }
            set
            {
                _inPos = value;
                OnPropertyChanged(nameof(InPosition));
            }
        }
        public int Cellid
        {
            get
            {
                return _cellid;
            }
            set
            {
                _cellid = value;
                OnPropertyChanged(nameof(Cellid));
            }
        }

        public CelIFlowInf(int id, bool inpos)
        {
            Cellid = id;
            InPosition = inpos;
        }

    }
}
