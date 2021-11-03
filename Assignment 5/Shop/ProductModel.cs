using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    public class ProductModel
    {
        [Index(0)]
        public int ID { get; set; }
        [Index(1)]
        public string ProductName { get; set; }
        [Index(2)]
        public int count { get; set; }
        [Index(3)]
        public double price { get; set; }

    }
}
