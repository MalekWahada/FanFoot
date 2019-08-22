using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work1Back.Models.Product
{
    public class ProduitQuantite:Produit
    {
        [Required]      
        public int quantite { get; set; }
        [Required]
        public float price { get; set; }
        [Required]
        public float priceAfterDiscount { get; set; }
        [Required]
        public int rating { get; set; }
    }
}
