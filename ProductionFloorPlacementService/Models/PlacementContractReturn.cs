namespace ProductionFloorPlacementService.Models
{
    public class PlacementContractReturn
    {
        public int Id { get; set; }
        public string ProductionFloorModelName { get; internal set; }
        public string TechnologicalEquipmentName { get; internal set; }
        public int TechnologicalEquipmentQuantity { get; set; }

    }
}
