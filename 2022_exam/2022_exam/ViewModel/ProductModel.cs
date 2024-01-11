using _2022_exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_exam.ViewModel
{
    class ProductModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }


        private Product _product;
        private string fournisseur;
        public ProductModel(Product product)
        {
            _product = product;
        }


        public int ProductId{
            get { return _product.ProductId; }
            set { _product.ProductId = value; }
    }
        public string ProductName
        {
            get { return _product.ProductName; }
            set { _product.ProductName = value;}
        }
        public string Quantity
        {
            get { return _product.QuantityPerUnit; }
            set { _product.QuantityPerUnit = value;}
        }
        public string Fournisseur
        {
            get { return fournisseur; }
            set { fournisseur = value;}
        } 

    }
}
