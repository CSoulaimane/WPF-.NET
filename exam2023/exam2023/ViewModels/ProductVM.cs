using Castle.Core.Resource;
using exam_2023.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace exam2023.ViewModels
{
    internal class ProductVM : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> _product;
        private ObservableCollection<OrderModel> _orderCountry;
        private NorthwindContext context = new NorthwindContext();
        private ProductModel _selectedProduct;
        private DelegateCommand delete_product;

        public ProductVM() { }
        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        public DelegateCommand DeleteProduct
        {
            get { return delete_product = delete_product?? new DelegateCommand(deleteCommand); }
        }

        private void deleteCommand()
        {
            ProductList.Remove(SelectedProduct);
        }

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged("OrderCountry");
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public ObservableCollection<ProductModel> ProductList
        {
            get { return _product = _product ?? loadProduct(); }
        }

        public ObservableCollection<OrderModel> OrderCountry
        {

            get { return  loadCountry();
            }
        }

        private ObservableCollection<OrderModel> loadCountry()
        {
            if (SelectedProduct == null) return null;

                ObservableCollection<OrderModel> orderModels = new ObservableCollection<OrderModel>();

            
                var result = (from od in context.OrderDetails
                          join o in context.Orders on od.OrderId equals o.OrderId
                          where od.ProductId == SelectedProduct.ProductId
                          select new OrderModel(o.ShipCountry, od.Quantity)).ToList();
                MessageBox.Show(result.First().Country);


            foreach (var orderModel in result)
            {
                orderModels.Add(orderModel);
            }

            

         

 
            

            return orderModels;
        }



        private ObservableCollection<ProductModel> loadProduct()
        {
            List<Product> products = (from p in context.Products
                                      where p.Discontinued == false
                                      select p).ToList();
            ObservableCollection<ProductModel> productModels = new ObservableCollection<ProductModel>();
                    



            foreach (var item in products)
            {
                ProductModel temp = new ProductModel(item);
                temp.Categorie = (from c in context.Categories
                                 where c.CategoryId == item.CategoryId
                                 select c.CategoryName).First();
                temp.Fournisseur = (from c in context.Suppliers
                                    where c.SupplierId == item.SupplierId
                                    select c.CompanyName).First();
                productModels.Add(temp);

            }
            return productModels;

        }

    }
}
