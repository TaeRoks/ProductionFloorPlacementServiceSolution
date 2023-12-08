using ProductionFloorPlacementService.Models;

namespace ProductionFloorPlacementService.Interfaces
{
    public interface IContractService
    {
        Task<PlacementContractReturn> CreateContract(PlacementContractDto placementContract);
        Task<List<PlacementContractReturn>> GetContracts();
        Task<PlacementContractReturn> GetContract(int contractId);
        


    }
}
