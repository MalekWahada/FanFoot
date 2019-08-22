using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work1Back.Models.Product;

namespace work1Back.Models
{
    public class AuthentifcationContext : IdentityDbContext
    {
        public AuthentifcationContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // many to many cart <====> Produit
            builder.Entity<CartProduct>().HasKey(cp => new {cp.idProduct, cp.idCart });

            builder.Entity<CartProduct>().HasOne<Produit>(cp => cp.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.idProduct);

            builder.Entity<CartProduct>().HasOne<Cart>(cp => cp.Cart)
                            .WithMany(p => p.CartProducts)
                            .HasForeignKey(cp => cp.idCart);

            builder.Entity<ApplicationUser>().HasOne(a => a.Cart)
                .WithOne(b => b.User).HasForeignKey<Cart>(b => b.IdUser);

            base.OnModelCreating(builder);

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Produit> Products { get; set; }
        public DbSet<ProduitEncher> ProduitEnchers { get; set; }
        public DbSet<ProduitQuantite> ProduitQuantites { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

    }
}
