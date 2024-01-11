using exam_2023.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam2023.ViewModels
{
    class ProductModel : INotifyPropertyChanged
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


        private  Product _product;
        private  string _categorie;
        private  string _fournisseur;

        public ProductModel(Product product)
        {
            _product = product;
        }

        public int ProductId
        {
            get { return this._product.ProductId; }
            set { this._product.ProductId = value;}
        }
        public string ProductName
        {
            get { return this._product.ProductName;}
            set { _product.ProductName = value;}
        }
        public string Categorie
        {
            get { return _categorie; }
            set { this._categorie = value; }
        }
        public string Fournisseur
        {
            get { return _fournisseur; }
            set { _fournisseur = value;}
        }
    }
}
