using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class TabItem :INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        public string Title { get; set; }
        public string Pic { get; set; }
        public string Route { get; set; }
        public bool NotHighlighted { get { return notHighlighted; } set { notHighlighted = value; OnPropertyChanged(); } }
        private bool notHighlighted;
        public TabItem(string title_, string pic_, string route_)
        {
            Title = title_;
            Pic = pic_;
            Route = route_;
            NotHighlighted=true;
        }
        public TabItem() { }
        public TabItem(string title_,string route_) : this(title_, "", route_)
        {

        }
    }
}
