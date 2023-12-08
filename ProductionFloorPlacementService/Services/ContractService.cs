using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductionFloorPlacementService.Data;
using ProductionFloorPlacementService.Exceptions;
using ProductionFloorPlacementService.Interfaces;
using ProductionFloorPlacementService.Models;

namespace ProductionFloorPlacementService.Services
{
    public class ContractService : IContractService
    {
        private readonly ProductionFloorPlacementServiceContext _context;

        public ContractService(ProductionFloorPlacementServiceContext context)
        {
            _context = context;
        }

        public async Task<PlacementContractReturn> CreateContract(PlacementContractDto placementContract)
        {
            var placementModel = new PlacementContractModel
            {
                ProductionFloorModelId = placementContract.ProductionFloorModelId,
                TechnologicalEquipmentModelId = placementContract.TechnologicalEquipmentModelId,
                TechnologicalEquipmentQuantity = placementContract.TechnologicalEquipmentQuantity,
            };
            var floorAreaOccupied = await _context.PlacementContractModel.Where(c => c.ProductionFloorModelId == placementModel.ProductionFloorModelId).Select(c => c.TechnologicalEquipmentQuantity * c.TechnologicalEquipment.Area).SumAsync();
            var floorArea = await _context.ProductionFloorModel.Where(f => f.Id == placementModel.ProductionFloorModelId).Select(f => f.Area).FirstAsync();
            var floorAreaOccupiedAdd = (await _context.TechnologicalEquipmentModel.Where(e => e.Id == placementModel.TechnologicalEquipmentModelId).Select(f => f.Area).FirstAsync()) * placementModel.TechnologicalEquipmentQuantity;

            if (floorArea < floorAreaOccupied + floorAreaOccupiedAdd)
            {

                throw new NotEnoughAreaException();

            }

            _context.PlacementContractModel.Add(placementModel);
            await _context.SaveChangesAsync();


            return await GetContract(placementModel.Id);
        }

        public async Task<PlacementContractReturn> GetContract(int contractId)
        {
            var contract = await _context.PlacementContractModel.Where(c => c.Id == contractId).Select(c => new PlacementContractReturn
            {
                Id = c.Id,
                ProductionFloorModelName = c.ProductionFloor.Name,
                TechnologicalEquipmentName = c.TechnologicalEquipment.Name,
                TechnologicalEquipmentQuantity = c.TechnologicalEquipmentQuantity
            }).FirstOrDefaultAsync();
            if (contract == null)
            {
                throw new Exception("Not found");
            }
            return contract;
        }

        public async Task<List<PlacementContractReturn>> GetContracts()
        {
            var contracts = await _context.PlacementContractModel.Select(c => new PlacementContractReturn
            {
                Id = c.Id,
                // ProductionFloorModelId = c.ProductionFloorModelId,
                ProductionFloorModelName = c.ProductionFloor.Name,
                // TechnologicalEquipmentModelId = c.TechnologicalEquipmentModelId,
                TechnologicalEquipmentName = c.TechnologicalEquipment.Name,
                TechnologicalEquipmentQuantity = c.TechnologicalEquipmentQuantity
            }).ToListAsync();
            return contracts;
        }

    }
}
