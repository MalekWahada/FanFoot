using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using work1Back.Models.Product;

namespace work1Back.Models
{
    public class CartProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdOrder { get; set; }
        public string OrederDate { get; set; }
        public int Quantity { get; set; }

        // raltion many to many Product <=====> Cart
        public int idProduct { get; set; }
        public virtual Produit Product { get; set; }
        public int idCart { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
