using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work1Back.Models
{
    public class Cart
    {
        [Key]
        public int IdCart { get; set; }
        public float TotalPrice { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; }

        // one to one relation ApplicationUser <====> Cart
        [ForeignKey("User")]
        public string IdUser { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
