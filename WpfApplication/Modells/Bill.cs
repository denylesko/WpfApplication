using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApplication.Modells
{
    public class Bill : INotifyPropertyChanged
    {
        public int idBill { get; set; }
        public int idProduct { get; set; }
        public int countProduct { get; set; }
        public DateTime DateWrite { get; set; }
        public string nameProduct { get; set; }
        public int CountProduct
        { 
            get => countProduct;
            set
            {
                if (countProduct != value)
                    return;
                
                    countProduct = value;
                    OnPropertyChanged();
                
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
