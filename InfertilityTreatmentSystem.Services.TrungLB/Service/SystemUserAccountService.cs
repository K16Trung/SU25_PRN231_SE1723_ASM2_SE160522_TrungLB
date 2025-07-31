using InfertilityTreatmentSystem.Repositories.TrungLB;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service
{
    public interface ISystemUserAccountService
    {
        Task<SystemUserAccount> GetAccountAsync(string username, string password);
    }

    public class SystemUserAccountService : ISystemUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SystemUserAccountService() => _unitOfWork ??= new UnitOfWork();

        public async Task<SystemUserAccount> GetAccountAsync(string username, string password)
        {
            try
            {
                return await _unitOfWork.SystemUserAccountRepository.GetUserAccountAsync(username, password);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception($"Authentication failed: {ex.Message}", ex);
            }
        }
    }
}
