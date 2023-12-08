using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionFloorPlacementService.Data;
using ProductionFloorPlacementService.Data.Models;
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

        

        private bool PlacementContractModelExists(int id)
        {
            return (_context.PlacementContractModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
