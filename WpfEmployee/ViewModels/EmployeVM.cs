using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApplication1.ViewModels;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmployeVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }


        private ObservableCollection<EmplopyeModel> _employeesList;
        private NorthwindContext context = new NorthwindContext();
        private List<string> _title;
        private DelegateCommand _addCommand;
        private DelegateCommand _save;
        private EmplopyeModel _selectedEmployee;
        private ObservableCollection<OrderModel> _order = null;

        public EmplopyeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value;OnPropertyChanged("OrdersList");  }

        }


        public DelegateCommand AddCommand { get { return _addCommand = _addCommand ?? new DelegateCommand(AddEmployee); } }
        public DelegateCommand SaveCommand {get { return _save = _save ?? new DelegateCommand (SaveEmployee);} }
        private void AddEmployee()
        {
            Employee employee = new Employee();
            EmplopyeModel employeModel = new EmplopyeModel(employee);
            MessageBox.Show("al ==   " + employeModel.FirstName + employee.LastName);
            EmployeesList.Add(employeModel);
            SelectedEmployee = employeModel;
        }
        private void SaveEmployee()
        {
            context.Add(SelectedEmployee.employee);
            context.SaveChanges();
            MessageBox.Show("Enregistrement en base de données fait");


        }
        public ObservableCollection<EmplopyeModel> EmployeesList
        {
            get { return  _employeesList = _employeesList ?? loadEmploye();
        }
    }
        public ObservableCollection<OrderModel> OrdersList
        {
            get
            {
                if (SelectedEmployee != null)
                {
                    _order = loadOrder();
                }

                return _order;

            }

        }


        public List<string> ListTitle
        {
            get { return _title = _title ?? loadTitle(); }
        }
        private ObservableCollection<EmplopyeModel> loadEmploye()
        {
            ObservableCollection<EmplopyeModel> localCollection = new ObservableCollection<EmplopyeModel>();
            foreach (var item in context.Employees)
            {
                localCollection.Add(new EmplopyeModel(item));
            }
            return localCollection;
        }


        private List<string> loadTitle()
        {
            List<string> titles = new List<string>();
            List<string> titleGet = (from e in context.Employees
                                     where e.TitleOfCourtesy != null && e.TitleOfCourtesy.Length > 0
                                     select e.TitleOfCourtesy)
                        .Distinct()
                        .ToList();
            return titleGet;
        }

        private ObservableCollection<OrderModel> loadOrder()
        {
            if (SelectedEmployee != null)
            {
                // Example query to fetch the last three orders for the selected employee
                var lastThreeOrdersQuery = context.Orders
                    .Where(o => o.EmployeeId == SelectedEmployee.employee.EmployeeId)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(3)
                    .Select(o => new OrderModel(o));
                MessageBox.Show("test : " + lastThreeOrdersQuery.ToList().Count);

                return new ObservableCollection<OrderModel>(lastThreeOrdersQuery);
            }
            MessageBox.Show("test 233 :");

            // Handle the case where SelectedEmployee is null
            return new ObservableCollection<OrderModel>();
        }

    }
}
