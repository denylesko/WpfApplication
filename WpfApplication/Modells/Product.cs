using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApplication.Modells
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductWeight { get; set; }
        public float ProductPrice { get; set; }
        public bool ProductAvailable { get; set; }
    }
}
