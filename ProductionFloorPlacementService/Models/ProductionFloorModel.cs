using System.ComponentModel.DataAnnotations;

namespace ProductionFloorPlacementService.Models
{
    public class ProductionFloorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Area { get; set; }
    }
}
