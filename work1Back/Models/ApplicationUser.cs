using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work1Back.Models
{
    public enum Gender
    {
        Male , Female
    }
    public enum FavoriteTeam
    {
        None = 0 , RealMadrid , Barcelona , Juventus , Milan , Munchen , Dortmund , ManchesterUnited , Arsenal , Chelsea 
    }
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string PictureUrl { get; set; }
        public Gender Gender { get; set; }
        public string BirthDate { get; set; }
        public FavoriteTeam FavoriteTeam { get; set; }
        // one to one Identity User  <====>  Cart
        public virtual Cart Cart { get; set; }
        
    }
}
