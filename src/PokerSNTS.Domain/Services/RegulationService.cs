using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class RegulationService : BaseService, IRegulationService
    {
        private readonly IRegulationRepository _regulationRepository;

        public RegulationService(IRegulationRepository regulationRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _regulationRepository = regulationRepository;
        }

        public async Task AddAsync(Regulation regulation)
        {
            var regulations = await GetAllAsync();

            if (regulations.Any(x => x.Description == regulation.Description))
                AddNotification("Já existe outro regulamento cadastrado com essa descrição.");

            if (ValidateEntity(regulation))
            {
                _regulationRepository.Add(regulation);

                if (!await CommitAsync())
                    AddNotification("Não foi possível cadastrar o regulamento.");
            }
        }

        public async Task UpdateAsync(Guid id, Regulation regulation)
        {
            var existingRegulation = await _regulationRepository.GetByIdAsync(id);

            if (existingRegulation == null)
                AddNotification("Regulamento não encontrado.");

            existingRegulation.Update(regulation.Description);
            if (ValidateEntity(existingRegulation))
            {
                _regulationRepository.Update(existingRegulation);

                if (!await CommitAsync())
                    AddNotification("Não foi possível atualizar o regulamento.");
            }
        }

        public async Task<IEnumerable<Regulation>> GetAllAsync()
        {
            return await _regulationRepository.GetAllAsync();
        }

        public async Task<Regulation> GetByIdAsync(Guid id)
        {
            return await _regulationRepository.GetByIdAsync(id);
        }
    }
}
