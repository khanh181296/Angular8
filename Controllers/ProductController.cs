using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular8.Data;
using Angular8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular8.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController (ApplicationDbContext db)
        {
            _db = db;
        }


        // GET: api/values
        [HttpGet("[action]")]
        public IActionResult GetProducts()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct ([FromBody] ProductModel formdata)
        {
            var newproduct = new ProductModel
            {
                Name = formdata.Name,
                ImagerUrl = formdata.ImagerUrl,
                Description = formdata.Description,
                OutOfStock = formdata.OutOfStock,
                Price = formdata.Price
            };
            await _db.Products.AddAsync(newproduct);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] int id, [FromBody] ProductModel formdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var findProduct = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (findProduct == null)
            {
                return NotFound();
            }

            //If the product was found
            findProduct.Name = formdata.Name;
            findProduct.Description = formdata.Description;
            findProduct.ImagerUrl = formdata.ImagerUrl;
            findProduct.OutOfStock = formdata.OutOfStock;
            findProduct.Price = findProduct.Price;

            _db.Entry(findProduct).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("the Product with id " + id + " is updated"));
        }

        [HttpDelete("[action]/{id}")]

        public async Task<IActionResult> DeleteProduct ([FromBody] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //find the product

            var findProduct = await _db.Products.FindAsync(id);
            if (findProduct == null)
            {
                return NotFound();
            }

            _db.Products.Remove(findProduct);
            await _db.SaveChangesAsync();

            //finally return the result to client

            return Ok(new JsonResult("the Product with id" + id + " is Deleted"));
        }
     }
}
