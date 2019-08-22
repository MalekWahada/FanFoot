using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work1Back.Models.Product;

namespace work1Back.Models.productModels
{
    public class CartModel
    {
        public float TotalPrice { get; set; }
      //  public Produit Product { get; set; }
        public string UserId { get; set; }
    }
}
