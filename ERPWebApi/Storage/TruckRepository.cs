using TrucksWebApi.Infrastructure.Interfaces;
using TrucksWebApi.Models;

namespace TrucksWebApi.Infrastructure
{
    public class TruckRepository : ITruckRepository
    {
        private readonly List<TruckDTO> _trucks = new List<TruckDTO>();

        public IEnumerable<TruckDTO> GetTrucks()
        {
            return _trucks;
        }

        public TruckDTO GetTruckByCode(string code)
        {
            return _trucks.Find(t => t.Code == code);
        }

        public TruckDTO CreateTruck(TruckDTO truckDTO)
        {
            _trucks.Add(truckDTO);
            return truckDTO;
        }

        public void UpdateTruck(string code, TruckDTO truckDTO)
        {
            var truckIndex = _trucks.FindIndex(t => t.Code == code);
            if (truckIndex != -1)
                _trucks[truckIndex] = truckDTO;
        }

        public void DeleteTruck(string code)
        {
            var truck = _trucks.Find(t => t.Code == code);
            if (truck != null)
                _trucks.Remove(truck);
        }
    }
}
