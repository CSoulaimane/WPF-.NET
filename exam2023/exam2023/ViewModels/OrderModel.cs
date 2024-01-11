using exam_2023.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam2023.ViewModels
{
    internal class OrderModel : INotifyPropertyChanged
    {
        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }


        private string _country;
        private int _total;



        public OrderModel(string country, int total)
        {
            _country = country;
            _total = total;
        }

        public string  Country {
            get{ return _country; }
            set { _country = value; OnPropertyChanged("Country"); }
        }
        public int Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged("Total");
            }
        }
}
}
