namespace TrucksWebApi.Models
{
    public class TruckDTO
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public TruckStatus Status { get; set; }
        public string? Description { get; set; }
    }

    public enum TruckStatus
    {
        OutOfService,
        Loading,
        ToJob,
        AtJob,
        Returning
    }
}
