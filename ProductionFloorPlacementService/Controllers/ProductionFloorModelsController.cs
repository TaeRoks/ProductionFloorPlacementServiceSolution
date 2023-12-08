using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionFloorPlacementService.Data;
using ProductionFloorPlacementService.Models;

namespace ProductionFloorPlacementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionFloorModelsController : ControllerBase
    {
        private readonly ProductionFloorPlacementServiceContext _context;

        public ProductionFloorModelsController(ProductionFloorPlacementServiceContext context)
        {
            _context = context;
        }

        // GET: api/ProductionFloorModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionFloorModel>>> GetProductionFloorModel()
        {
            if (_context.ProductionFloorModel == null)
            {
                return NotFound();
            }
            return await _context.ProductionFloorModel.ToListAsync();
        }

        // GET: api/ProductionFloorModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionFloorModel>> GetProductionFloorModel(int id)
        {
            if (_context.ProductionFloorModel == null)
            {
                return NotFound();
            }
            var productionFloorModel = await _context.ProductionFloorModel.FindAsync(id);

            if (productionFloorModel == null)
            {
                return NotFound();
            }

            return productionFloorModel;
        }

        // PUT: api/ProductionFloorModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductionFloorModel(int id, ProductionFloorModel productionFloorModel)
        {
            if (id != productionFloorModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(productionFloorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionFloorModelExists(id))
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

        // POST: api/ProductionFloorModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductionFloorModel>> PostProductionFloorModel(ProductionFloorModel productionFloorModel)
        {
            if (_context.ProductionFloorModel == null)
            {
                return Problem("Entity set 'ProductionFloorPlacementServiceContext.ProductionFloorModel'  is null.");
            }
            _context.ProductionFloorModel.Add(productionFloorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductionFloorModel", new { id = productionFloorModel.Id }, productionFloorModel);
        }

        // DELETE: api/ProductionFloorModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionFloorModel(int id)
        {
            if (_context.ProductionFloorModel == null)
            {
                return NotFound();
            }
            var productionFloorModel = await _context.ProductionFloorModel.FindAsync(id);
            if (productionFloorModel == null)
            {
                return NotFound();
            }

            _context.ProductionFloorModel.Remove(productionFloorModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductionFloorModelExists(int id)
        {
            return (_context.ProductionFloorModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
