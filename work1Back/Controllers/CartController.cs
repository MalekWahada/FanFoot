using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work1Back.Models;
using work1Back.Models.Product;
using work1Back.Models.productModels;

namespace work1Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly AuthentifcationContext _context;

        public CartController(UserManager<ApplicationUser> userManager, AuthentifcationContext context)
        {
            _userManager = userManager;
            _context = context;
        }





        // POST: api/Cart/InitialiseCart
        [HttpPost]
        [Route("InitialiseCart")]
        public async Task<IActionResult> InitialiseCart(CartModel Cart)
        {
            var c = _context.Carts.Where(crt1 => crt1.IdUser == Cart.UserId);
            if (c.Any())
                return BadRequest("Card alraedy created");
            Cart crt = new Cart();

            // just to test adding 
            var user = await _userManager.FindByIdAsync(Cart.UserId);
            crt.IdUser = _context.ApplicationUsers.Single(a => a.Id == Cart.UserId).Id;
            crt.TotalPrice = 0;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Carts.Add(crt);
            await _context.SaveChangesAsync();

            return Ok("Cart Created");
        }


        // POST: api/Cart/OrderProduct
        [HttpPost]
        [Route("OrderProduct")]
        public async Task<IActionResult> OrderProduct(OrderModel Order)
        {
            // isuser + idProd + quantity
            var Cart = _context.Carts.Single(a => a.IdUser == Order.idUser);
            var prod = _context.Products.FirstOrDefault(a => a.idProduit == Order.idProduct);

            if (prod != null && Cart != null)
            {
                ((ProduitQuantite)prod).quantite -= Order.Quantity;

                CartProduct CP = new CartProduct();

                CP.idCart = _context.Carts.Single(a => a.IdUser == Order.idUser).IdCart;
                CP.idProduct = _context.Products.Single(a => a.idProduit == Order.idProduct).idProduit;
                CP.Quantity = Order.Quantity;
                CP.OrederDate = DateTime.Now.ToString();

                if (((ProduitQuantite)prod).priceAfterDiscount != 0)
                    Cart.TotalPrice += Order.Quantity * ((ProduitQuantite)prod).priceAfterDiscount;
                else
                    Cart.TotalPrice += Order.Quantity * ((ProduitQuantite)prod).price;

                // update product quantity
                _context.Carts.Update(Cart);
                _context.Products.Update(prod);
                _context.CartProducts.Add(CP);

                await _context.SaveChangesAsync();
            }
            else
                return NotFound("Card or Product does not exist");

            return Ok();

        }



        // POST: api/Cart/UpdateProdQuantity
        [HttpPost]
        [Route("UpdateProdQuantity")]
        public async Task<IActionResult> UpdateProdQuantity(OrderModel Order)
        {
            var cart = _context.Carts.Single(a => a.IdUser == Order.idUser);
            var order = _context.CartProducts.Single(a => a.idCart == cart.IdCart && a.idProduct == Order.idProduct);

            if (order == null)
                return BadRequest("Order does not exist");

            var product = _context.Products.Single(a => a.idProduit == Order.idProduct);

            // update cart total price
            if (((ProduitQuantite)product).priceAfterDiscount != 0)
                cart.TotalPrice += (Order.Quantity - order.Quantity) * ((ProduitQuantite)product).priceAfterDiscount;
            else
                cart.TotalPrice += (Order.Quantity - order.Quantity) * ((ProduitQuantite)product).price;

            ((ProduitQuantite)product).quantite -= (Order.Quantity - order.Quantity);
            order.Quantity = Order.Quantity;
            order.OrederDate = DateTime.Now.ToString();

            _context.Carts.Update(cart);
            _context.CartProducts.Update(order);
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return Ok();

        }


        // GET: api/Cart/MyOrders
        [HttpGet]
        [Route("MyOrders/{id}")]
        public IEnumerable<CartProduct> MyOrders(string id)
        {
            System.Console.WriteLine("0000000000000000000000 id cart " + id);
            Cart cart = _context.Carts.Single(a => a.IdUser == id);
            System.Console.WriteLine("1111111111111111111111 id cart" + cart.IdCart);
            if (cart == null)
                return null;
            System.Console.WriteLine("22222222222222222222");

            var Orders = _context.CartProducts.Where(a => a.idCart == cart.IdCart);
            var Orders1 = Orders.ToList();
            var Orders2 = new List<CartProduct>();
            for (var i = 0; i < Orders1.Count(); i++)
            {
                var cc = new CartProduct()
                {
                    IdOrder = Orders1[i].IdOrder,
                    idCart = Orders1[i].idCart,
                    idProduct = Orders1[i].idProduct,
                    Quantity = Orders1[i].Quantity,
                    OrederDate = Orders1[i].OrederDate,
                    Product = _context.Products.Single(a => a.idProduit == Orders1[i].idProduct)
                };

                Orders2.Add(cc);
            }

            System.Console.WriteLine("666666666666666666666   orders");
            return Orders2;
        }

        [HttpGet]
        [Route("TotCart/{id}")]
        public object TotCart(string id)
        {
            var cart = _context.Carts.Single(a => a.IdUser == id);

            if (cart == null)
                return NotFound("cart does not exist");


            return new { cart.TotalPrice };
        }

        [HttpDelete]
        [Route("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteProduitQuantite(int id)
        {
            var order = _context.CartProducts.Single(a => a.IdOrder == id);
            if (order == null)
                return NotFound("order does not exist");
            var prod = _context.Products.Single(a => a.idProduit == order.idProduct);

            var cart = _context.Carts.Single(a => a.IdCart == order.idCart);
            if (((ProduitQuantite)prod).priceAfterDiscount != 0)
                cart.TotalPrice -= order.Quantity * ((ProduitQuantite)prod).priceAfterDiscount;
            else
                cart.TotalPrice -= order.Quantity * ((ProduitQuantite)prod).price;

            _context.CartProducts.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);

        }

    }
}