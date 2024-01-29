using FluentValidation;
using TrucksWebApi.Infrastructure.Interfaces;
using TrucksWebApi.Models;

namespace TrucksWebApi.Validators
{
    public class TruckValidator : AbstractValidator<TruckDTO>
    {
        private readonly ITruckRepository _truckRepository;

        public TruckValidator(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;

            RuleFor(x => x.Code).NotEmpty().Matches(@"^[a-zA-Z0-9]*$").WithMessage("Code must be alphanumeric and cannot be empty.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid status value.");
            RuleFor(x => x.Status).Must((dto, status) => StatusChangeIsValid(dto)).WithMessage("Invalid status change.");
        }

        private bool StatusChangeIsValid(TruckDTO dto)
        {
            // Out Of Service status can always be set
            if (dto.Status == TruckStatus.OutOfService)
                return true;

            var existingTruck = _truckRepository.GetTruckByCode(dto.Code);
            if (existingTruck == null)
            {
                return false;
            }

            // Other statuses can be set if current status is OutOfService
            if (existingTruck.Status == TruckStatus.OutOfService)
                return true;

            // Valid status transitions
            var validTransitions = new Dictionary<TruckStatus, TruckStatus>
            {
                { TruckStatus.Loading, TruckStatus.ToJob },
                { TruckStatus.ToJob, TruckStatus.AtJob },
                { TruckStatus.AtJob, TruckStatus.Returning },
                { TruckStatus.Returning, TruckStatus.Loading } // Can start Loading again after Returning
            };

            if (validTransitions.TryGetValue(existingTruck.Status, out TruckStatus allowedNextStatus))
            {
                return dto.Status == allowedNextStatus;
            }

            return false;
        }
    }
}
