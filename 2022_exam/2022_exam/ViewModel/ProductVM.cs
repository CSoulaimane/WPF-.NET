using _2022_exam.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.ViewModels;

namespace _2022_exam.ViewModel
{
    class ProductVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        private ObservableCollection<ProductModel> _products;
        private ObservableCollection<ProductSalesModel> _productSellQuantity;
        private NorthwindContext context  = new NorthwindContext();
        private DelegateCommand _updateProduct;
        private ProductModel _selectedItem;

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _products = _products ?? loadProducts(); }
        }

        public ObservableCollection<ProductSalesModel> ProductSellQuantity
        {
            get { return _productSellQuantity = _productSellQuantity ?? loadSell(); }
        }

        public ProductModel SelectedProduct
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged("SelectedProduct"); }
        }

        public DelegateCommand UpdateProduct
        {
            get { return _updateProduct = _updateProduct ?? new DelegateCommand(update); }
        }

        private void update()
        {
            if (_selectedItem != null)
            {
                var productOriginal = context.Products
                    .Where(e => e.ProductId == _selectedItem.ProductId)
                    .Select(e => e)
                    .FirstOrDefault();
                if (productOriginal != null)
                {
                    productOriginal.ProductName = _selectedItem.ProductName;
                    productOriginal.QuantityPerUnit = _selectedItem.Quantity;
                }

                var supplierId = context.Suppliers
                    .Where(e => e.ContactName == _selectedItem.Fournisseur)
                    .Select(e => e.SupplierId)
                    .FirstOrDefault();

                if (supplierId > 0) productOriginal.SupplierId = supplierId;
            
             
                context.SaveChanges();
            }
        }

        private ObservableCollection<ProductModel> loadProducts()
        {
            ObservableCollection<ProductModel> products = new ObservableCollection<ProductModel>();
            foreach (var item in context.Products)
            {
                ProductModel tempProduct = new ProductModel(item);
                tempProduct.Fournisseur = (from s in context.Suppliers
                                          where s.SupplierId == item.SupplierId
                                          select s.ContactName).First();
              
                products.Add(tempProduct);
            }
            return products;
        }

        private ObservableCollection<ProductSalesModel> loadSell()
        {
            var products = new ObservableCollection<ProductSalesModel>();

            var productSales = from p in context.Products
                               join od in context.OrderDetails on p.ProductId equals od.ProductId into orderDetailsGroup
                               select new ProductSalesModel
                               {
                                   ProductId = p.ProductId,
                                   TotalSales = orderDetailsGroup.Sum(od => od.UnitPrice * od.Quantity)
                               };

            foreach (var productGroup in productSales)
            {
                products.Add(productGroup);
            }

            return products;
        }




    }
}
