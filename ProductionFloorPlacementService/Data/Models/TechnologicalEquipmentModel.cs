using System.ComponentModel.DataAnnotations;

namespace ProductionFloorPlacementService.Data.Models
{
    public class TechnologicalEquipmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Area { get; set; }
    }
}
