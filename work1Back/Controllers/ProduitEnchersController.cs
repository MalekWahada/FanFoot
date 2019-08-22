using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using work1Back.Models;
using work1Back.Models.Product;

namespace work1Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitEnchersController : ControllerBase
    {
        private readonly AuthentifcationContext _context;

        public ProduitEnchersController(AuthentifcationContext context)
        {
            _context = context;
        }

        // GET: api/ProduitEnchers
        [HttpGet]
        public IEnumerable<ProduitEncher> GetProduitE()
        {
            return _context.ProduitEnchers;
        }

        // GET: api/ProduitEnchers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduitEncher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produitEncher = await _context.ProduitEnchers.FindAsync(id);

            if (produitEncher == null)
            {
                return NotFound();
            }

            return Ok(produitEncher);
        }

        // PUT: api/ProduitEnchers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduitEncher([FromRoute] int id, [FromBody] ProduitEncher produitEncher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produitEncher.idProduit)
            {
                return BadRequest();
            }

            _context.Entry(produitEncher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitEncherExists(id))
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

        // POST: api/ProduitEnchers
        [HttpPost]
        public async Task<IActionResult> PostProduitEncher( ProduitEncher produitEncher)
        {
            ProduitEncher pd = new ProduitEncher();
            pd.name = produitEncher.name;
            pd.addate = produitEncher.addate;
            pd.categorie = produitEncher.categorie;
            pd.description = produitEncher.description;
            pd.isNews = produitEncher.isNews;
            pd.limiteDate = produitEncher.limiteDate;
            pd.picture = produitEncher.picture;
            pd.soldPrice = produitEncher.soldPrice;
            pd.startPrice = produitEncher.startPrice;
            


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProduitEnchers.Add(pd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduitEncher", new { id = produitEncher.idProduit }, produitEncher);
        }

        // DELETE: api/ProduitEnchers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduitEncher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produitEncher = await _context.ProduitEnchers.FindAsync(id);
            if (produitEncher == null)
            {
                return NotFound();
            }

            _context.ProduitEnchers.Remove(produitEncher);
            await _context.SaveChangesAsync();

            return Ok(produitEncher);
        }

        private bool ProduitEncherExists(int id)
        {
            return _context.ProduitEnchers.Any(e => e.idProduit == id);
        }
    }
}