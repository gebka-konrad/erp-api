using TrucksWebApi.Models;

namespace TrucksWebApi.Infrastructure.Interfaces
{
    public interface ITruckRepository
    {
        IEnumerable<TruckDTO> GetTrucks();
        TruckDTO GetTruckByCode(string code);
        TruckDTO CreateTruck(TruckDTO truckDTO);
        void UpdateTruck(string code, TruckDTO truckDTO);
        void DeleteTruck(string code);
    }
}
