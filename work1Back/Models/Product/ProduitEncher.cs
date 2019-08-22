using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work1Back.Models.Product
{
    public class ProduitEncher:Produit
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string limiteDate { get; set; }
        [Required]
        public float startPrice { get; set; }
        [Required]
        public float soldPrice { get; set; }
    }
}
