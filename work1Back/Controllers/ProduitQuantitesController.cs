using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProduitQuantitesController : ControllerBase
    {
        private readonly AuthentifcationContext _context;

        public ProduitQuantitesController(AuthentifcationContext context)
        {
            _context = context;
        }

        // GET: api/ProduitQuantites
        [HttpGet]
        public IEnumerable<ProduitQuantite> GetProduitQ()
        {
            return _context.ProduitQuantites;
        }

        // GET: api/ProduitQuantites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduitQuantite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produitQuantite = await _context.ProduitQuantites.FindAsync(id);

            if (produitQuantite == null)
            {
                return NotFound();
            }

            return Ok(produitQuantite);
        }

        // PUT: api/ProduitQuantites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduitQuantite([FromRoute] int id, [FromBody] ProduitQuantite produitQuantite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produitQuantite.idProduit)
            {
                return BadRequest();
            }

            _context.Entry(produitQuantite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitQuantiteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProduitQuantites
        [HttpPost]
        public async Task<IActionResult> PostProduitQuantite(ProduitQuantite produitQuantite)
        {

            ProduitQuantite pd = new ProduitQuantite();
            pd.name = produitQuantite.name;
            pd.addate = produitQuantite.addate;
            pd.categorie = produitQuantite.categorie;
            pd.description = produitQuantite.description;
            pd.isNews = produitQuantite.isNews;
            pd.picture = produitQuantite.picture;
            pd.price = produitQuantite.price;
            pd.priceAfterDiscount = produitQuantite.priceAfterDiscount;
            pd.quantite = produitQuantite.quantite;
            pd.rating = produitQuantite.rating;
            pd.Color = produitQuantite.Color;
            pd.brand = produitQuantite.brand;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProduitQuantites.Add(pd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduitQuantite", new { id = produitQuantite.idProduit }, produitQuantite);
        }

        // DELETE: api/ProduitQuantites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduitQuantite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produitQuantite = await _context.ProduitQuantites.FindAsync(id);
            if (produitQuantite == null)
            {
                return NotFound();
            }

            _context.ProduitQuantites.Remove(produitQuantite);
            await _context.SaveChangesAsync();

            return Ok(produitQuantite);
        }

        private bool ProduitQuantiteExists(int id)
        {
            return _context.ProduitQuantites.Any(e => e.idProduit == id);
        }

        // malek part

        // GET: api/ProduitQuantites/PriceFilter
        [HttpPost]
        [Route("PriceFilter")]
        public IActionResult GetPriceFilter(ProductQuantityFilterModel prodQuantFiltrModel)
        {
            System.Console.Write("values: " + prodQuantFiltrModel.MinPrice + " maxxxxxx :   " + prodQuantFiltrModel.MaxPrice + "      ");
            // filter on color
            if (prodQuantFiltrModel.Color != null)
            {
                // filter on category
                if (prodQuantFiltrModel.Categorie != 0)
                {
                    var produitQuantite = _context.ProduitQuantites
                    .Where(a => a.price >= prodQuantFiltrModel.MinPrice && a.price <= prodQuantFiltrModel.MaxPrice
                    && a.Color == prodQuantFiltrModel.Color && a.categorie == prodQuantFiltrModel.Categorie);
                    if (produitQuantite.Any())
                        return Ok(produitQuantite);

                    else
                        return NotFound();
                }


                else
                {
                    var produitQuantite0 = _context.ProduitQuantites
                        .Where(a => a.price >= prodQuantFiltrModel.MinPrice && a.price <= prodQuantFiltrModel.MaxPrice
                        && a.Color == prodQuantFiltrModel.Color);
                    if (produitQuantite0.Any())
                        return Ok(produitQuantite0);

                    else
                        return NotFound();
                }
            }
            // categorie && color == null ||   categorie && color != null || categorie=null && color != null 



            else
            {
                if (prodQuantFiltrModel.Categorie != 0)
                {
                    var produitQuantite = _context.ProduitQuantites
                    .Where(a => a.price >= prodQuantFiltrModel.MinPrice && a.price <= prodQuantFiltrModel.MaxPrice
                     && a.categorie == prodQuantFiltrModel.Categorie);
                    if (produitQuantite.Any())
                        return Ok(produitQuantite);

                    else
                        return NotFound();
                }

                else
                {
                    var produitQuantite = _context.ProduitQuantites
                          .Where(a => a.price >= prodQuantFiltrModel.MinPrice && a.price <= prodQuantFiltrModel.MaxPrice);
                    if (produitQuantite.Any())
                        return Ok(produitQuantite);

                    else
                        return NotFound();
                }
            }
        }

    }
}


