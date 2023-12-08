using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionFloorPlacementService.Models
{
    public class PlacementContractModel
    {
        public int Id { get; set; }
        public int ProductionFloorModelId { get; set; }
        public int TechnologicalEquipmentModelId { get; set; }
        public int TechnologicalEquipmentQuantity { get; set; }
        public ProductionFloorModel ProductionFloor { get; set; }
        public TechnologicalEquipmentModel TechnologicalEquipment { get; set; }
    }
}
