namespace ProductionFloorPlacementService.Exceptions
{
    public class NotEnoughAreaException : Exception
    {
        public NotEnoughAreaException() : base("Ramaining floor area is not enough for this equpment quantity")
        {

        }
    }
}
