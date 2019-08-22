using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work1Back.Models.Product;

namespace work1Back.Models.productModels
{
    public class ProductQuantityFilterModel
    {
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public string Color { get; set; }
        public Categorie Categorie { get; set; }
    }
}
