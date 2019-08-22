using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work1Back.Models.Product
{
    public enum Categorie
    {
        None = 0, Tshirt,Boots,Balls
    }
    public enum Size
    {
        S,M,L,Xl,XXl
    }
    
    public class Produit
    {
        [Key]
        public int idProduit { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string name{ get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string addate { get; set; }
        [Required]
     
        public bool isNews { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string description { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string picture { get; set; }
        [Required]
       
        public Categorie categorie { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string brand { get; set; }
        [Required]
        public Size size { get; set; }
        [Required]
        public string Color { get; set; }
        // entity association
        public virtual ICollection<CartProduct> CartProducts { get; set; }

    }
}
