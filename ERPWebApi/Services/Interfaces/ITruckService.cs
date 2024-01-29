using TrucksWebApi.Models;

namespace TrucksWebApi.Services.Interfaces
{
    public interface ITruckService
    {
        IEnumerable<TruckDTO> GetTrucks();
        TruckDTO GetTruckByCode(string code);
        TruckDTO CreateTruck(TruckDTO truckDTO);
        void UpdateTruck(string code, TruckDTO truckDTO);
        void DeleteTruck(string code);
    }
}
