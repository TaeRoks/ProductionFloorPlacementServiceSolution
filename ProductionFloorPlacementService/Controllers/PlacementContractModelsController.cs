using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionFloorPlacementService.Data;
using ProductionFloorPlacementService.Exceptions;
using ProductionFloorPlacementService.Interfaces;
using ProductionFloorPlacementService.Models;

namespace ProductionFloorPlacementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementContractModelsController : ControllerBase
    {
        private readonly ProductionFloorPlacementServiceContext _context;
        private readonly IContractService _contractService;

        public PlacementContractModelsController(ProductionFloorPlacementServiceContext context, IContractService contractService)
        {
            _context = context;
            _contractService = contractService;
        }

        // GET: api/PlacementContractModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlacementContractModel>>> GetPlacementContractModel()
        {
            var contracts = await _contractService.GetContracts();
            return Ok(contracts);
        }

        // GET: api/PlacementContractModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlacementContractModel>> GetPlacementContractModel(int id)
        {
                        
            try
            {
                var placementContractModel = await _contractService.GetContract(id);
                return Ok(placementContractModel);
            }
            catch (Exception)
            {

                return NotFound();

            }

            
        }

        // PUT: api/PlacementContractModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlacementContractModel(int id, PlacementContractModel placementContractModel)
        {
            if (id != placementContractModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(placementContractModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacementContractModelExists(id))
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

        // POST: api/PlacementContractModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlacementContractModel>> PostPlacementContractModel(PlacementContractDto placementContractDto) //принимать дто модель
        {

            
            try
            {
                var contract = await _contractService.CreateContract(placementContractDto);
                return CreatedAtAction("GetPlacementContractModel", new { id = contract.Id }, contract);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

           
        }

        // DELETE: api/PlacementContractModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlacementContractModel(int id)
        {
            if (_context.PlacementContractModel == null)
            {
                return NotFound();
            }
            var placementContractModel = await _context.PlacementContractModel.FindAsync(id);
            if (placementContractModel == null)
            {
                return NotFound();
            }

            _context.PlacementContractModel.Remove(placementContractModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlacementContractModelExists(int id)
        {
            return (_context.PlacementContractModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
