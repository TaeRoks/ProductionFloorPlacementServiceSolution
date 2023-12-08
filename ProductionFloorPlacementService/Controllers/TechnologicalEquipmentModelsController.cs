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
    public class TechnologicalEquipmentModelsController : ControllerBase
    {
        private readonly ProductionFloorPlacementServiceContext _context;

        public TechnologicalEquipmentModelsController(ProductionFloorPlacementServiceContext context)
        {
            _context = context;
        }

        // GET: api/TechnologicalEquipmentModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnologicalEquipmentModel>>> GetTechnologicalEquipmentModel()
        {
            if (_context.TechnologicalEquipmentModel == null)
            {
                return NotFound();
            }
            return await _context.TechnologicalEquipmentModel.ToListAsync();
        }

        // GET: api/TechnologicalEquipmentModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnologicalEquipmentModel>> GetTechnologicalEquipmentModel(int id)
        {
            if (_context.TechnologicalEquipmentModel == null)
            {
                return NotFound();
            }
            var technologicalEquipmentModel = await _context.TechnologicalEquipmentModel.FindAsync(id);

            if (technologicalEquipmentModel == null)
            {
                return NotFound();
            }

            return technologicalEquipmentModel;
        }

        // PUT: api/TechnologicalEquipmentModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnologicalEquipmentModel(int id, TechnologicalEquipmentModel technologicalEquipmentModel)
        {
            if (id != technologicalEquipmentModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(technologicalEquipmentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnologicalEquipmentModelExists(id))
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

        // POST: api/TechnologicalEquipmentModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TechnologicalEquipmentModel>> PostTechnologicalEquipmentModel(TechnologicalEquipmentModel technologicalEquipmentModel)
        {
            if (_context.TechnologicalEquipmentModel == null)
            {
                return Problem("Entity set 'ProductionFloorPlacementServiceContext.TechnologicalEquipmentModel'  is null.");
            }
            _context.TechnologicalEquipmentModel.Add(technologicalEquipmentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTechnologicalEquipmentModel", new { id = technologicalEquipmentModel.Id }, technologicalEquipmentModel);
        }

        // DELETE: api/TechnologicalEquipmentModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnologicalEquipmentModel(int id)
        {
            if (_context.TechnologicalEquipmentModel == null)
            {
                return NotFound();
            }
            var technologicalEquipmentModel = await _context.TechnologicalEquipmentModel.FindAsync(id);
            if (technologicalEquipmentModel == null)
            {
                return NotFound();
            }

            _context.TechnologicalEquipmentModel.Remove(technologicalEquipmentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TechnologicalEquipmentModelExists(int id)
        {
            return (_context.TechnologicalEquipmentModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
