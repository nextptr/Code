using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridBind
{
    public enum CuttingMode
    {
        Half,
        Full
    }

    public class MyData: NotifyPropertyChanged
    {
        public double OffsetX
        {
            get { return _offsetX; }
            set { if (_offsetX != value) { _offsetX = value; OnPropertyChanged("OffsetX"); } }
        }

        public double OffsetY
        {
            get { return _offsetY; }
            set { if (_offsetY != value) { _offsetY = value; OnPropertyChanged("OffsetY"); } }
        }
        public double StartX
        {
            get { return _startX; }
            set { if (_startX != value) { _startX = value; OnPropertyChanged("StartX"); } }
        }
        public double StartY
        {
            get { return _startY; }
            set { if (_startY != value) { _startY = value; OnPropertyChanged("StartY"); } }
        }
        public double EndX
        {
            get { return _endX; }
            set { if (_endX != value) { _endX = value; OnPropertyChanged("EndX"); } }
        }
        public double EndY
        {
            get { return _endY; }
            set { if (_endY != value) { _endY = value; OnPropertyChanged("EndY"); } }
        }
        public double Power
        {
            get { return _power; }
            set { if (_power != value) { _power = value; OnPropertyChanged("Power"); } }
        }


        private double _offsetX;
        private double _offsetY;
        private double _startX;
        private double _startY;
        private double _endX;
        private double _endY;
        private double _power;
    }
}
