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
    public class RegulationService : BaseService, IRegulationService
    {
        private readonly IRegulationRepository _regulationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotificationHandler _notifications;

        public RegulationService(IRegulationRepository regulationRepository,
            IUnitOfWork unitOfWork,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _regulationRepository = regulationRepository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public async Task<Regulation> AddAsync(Regulation regulation)
        {
            if(ValidateEntity(regulation))
            {
                _regulationRepository.Add(regulation);
                if (await _unitOfWork.CommitAsync()) return regulation;
            }

            return null;
        }

        public async Task<Regulation> UpdateAsync(Guid id, Regulation regulation)
        {
            var existingRegulation = await _regulationRepository.GetByIdAsync(id);
            if (existingRegulation == null)
            {
                _notifications.HandleNotification("DomainValidation", "Regulamento não encontrado");
                return null;
            }

            existingRegulation.Update(regulation.Description);
            if(ValidateEntity(existingRegulation))
            {
                _regulationRepository.Update(existingRegulation);
                if (await _unitOfWork.CommitAsync()) return existingRegulation;
            }

            return null;
        }

        public async Task<IEnumerable<Regulation>> GetAllAsync()
        {
            return await _regulationRepository.GetAllAsync();
        }
    }
}
