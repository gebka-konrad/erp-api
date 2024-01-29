using TrucksWebApi.Infrastructure.Interfaces;
using TrucksWebApi.Models;
using TrucksWebApi.Services.Interfaces;

namespace TrucksWebApi.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepository;

        public TruckService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public IEnumerable<TruckDTO> GetTrucks()
        {
            return _truckRepository.GetTrucks();
        }

        public TruckDTO GetTruckByCode(string code)
        {
            return _truckRepository.GetTruckByCode(code);
        }

        public TruckDTO CreateTruck(TruckDTO truckDTO)
        {
            return _truckRepository.CreateTruck(truckDTO);
        }

        public void UpdateTruck(string code, TruckDTO truckDTO)
        {
            _truckRepository.UpdateTruck(code, truckDTO);
        }

        public void DeleteTruck(string code)
        {
            _truckRepository.DeleteTruck(code);
        }
    }
}
