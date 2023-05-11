using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModels;

namespace WpfApp1.Models.Records
{
    public class TournamentRecord:ViewModelBase
    {
        private int _w;
        public int W
        {
            get
            {
                return _w;
            }
            set
            {
                _w = value;
                OnPropertyChanged("W");
            }
        }
        private int _l;
        public int L
        {
            get
            {
                return _l;
            }
            set
            {
                _l = value;
                OnPropertyChanged("L");
            }
        }
        private int _t;
        public int T
        {
            get
            {
                return _t;
            }
            set
            {
                _t = value;
                OnPropertyChanged("T");
            }
        }
        private int _p;
        public int P
        {
            get
            {
                return _p;
            }
            set
            {
                _p = value;
                OnPropertyChanged("P");
            }
        }
    }
}
