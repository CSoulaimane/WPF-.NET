using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmplopyeModel : INotifyPropertyChanged
    {
        public readonly Employee employee;

        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged;
        // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public EmplopyeModel(Employee employee)
        {
            this.employee = employee;
        }

        public string FullName { get { return employee.FirstName + " " + employee.LastName; }}
        public DateTime? DisplayBirthDate {get { return employee.BirthDate; }}

        public string FirstName { get { return employee.FirstName; }
            set { employee.FirstName = value; OnPropertyChanged("FullName"); }
        }
        public string TitleOfCourtesy
        {
            get { return employee.TitleOfCourtesy; }
            set
            {
                employee.TitleOfCourtesy = value;
            }
        }


        public string LastName { get { return employee.LastName; } set { employee.LastName = value; OnPropertyChanged("FullName"); } }
        public DateTime? BirthDate
        { get { return employee.BirthDate; }
            set { employee.BirthDate = value; OnPropertyChanged("DisplayBirthDate"); }
        }
        public DateTime? HireDate { get { return employee.HireDate; }
            set
            {
                employee.HireDate = value;
                OnPropertyChanged(nameof(HireDate));
            }
        }
    }
}
