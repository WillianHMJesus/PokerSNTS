using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class RegulationService : IRegulationService
    {
        private readonly IRegulationRepository _regulationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notification;

        public RegulationService(IRegulationRepository regulationRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notification)
        {
            _regulationRepository = regulationRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<bool> Add(Regulation regulation)
        {
            var validationResult = regulation.Validate();
            if(validationResult.IsValid)
            {
                _regulationRepository.Add(regulation);

                return await _unitOfWork.Commit();
            }

            _notification.HandleNotification(validationResult);

            return false;
        }

        public async Task<bool> Update(Guid id, Regulation regulation)
        {
            var existingRegulation = await _regulationRepository.GetById(id);
            if (existingRegulation == null) _notification.HandleNotification("DomainValidation", "Regulamento não encontrado");

            if(!_notification.HasNotification())
            {
                existingRegulation.Update(regulation.Description);
                var validationResult = existingRegulation.Validate();
                if (validationResult.IsValid)
                {
                    _regulationRepository.Update(existingRegulation);

                    return await _unitOfWork.Commit();
                }

                _notification.HandleNotification(validationResult);
            }

            return false;
        }

        public async Task<IEnumerable<Regulation>> GetAll()
        {
            return await _regulationRepository.GetAll();
        }
    }
}
