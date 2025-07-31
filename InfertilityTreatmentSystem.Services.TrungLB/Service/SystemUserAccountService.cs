using InfertilityTreatmentSystem.Repositories.TrungLB;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service
{
    public class SystemUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SystemUserAccountService() => _unitOfWork ??= new UnitOfWork();

        public async Task<SystemUserAccount> GetAccountAsync(string username, string password)
        {
            return await _unitOfWork.SystemUserAccountRepository.GetUserAccountAsync(username, password);
        }
    }
}
