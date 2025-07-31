using InfertilityTreatmentSystem.Repositories.TrungLB;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using InfertilityTreatmentSystem.Services.TrungLB.Service.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service
{
    public class ReminderTypeTrungLbService : IReminderTypeTrungLbService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReminderTypeTrungLbService() => _unitOfWork ??= new UnitOfWork();

        public ReminderTypeTrungLbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReminderTypeTrungLb>> GetAllAsync()
        {
            return await _unitOfWork.ReminderTypeRepository.GetAllAsync();
        }

        public async Task<ReminderTypeTrungLb?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ReminderTypeRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(ReminderTypeTrungLb reminderType)
        {
            return await _unitOfWork.ReminderTypeRepository.CreateAsync(reminderType);
        }

        public async Task<int> UpdateAsync(ReminderTypeTrungLb reminderType)
        {
            return await _unitOfWork.ReminderTypeRepository.UpdateAsync(reminderType);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _unitOfWork.ReminderTypeRepository.GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            return await _unitOfWork.ReminderTypeRepository.RemoveAsync(item);
        }
    }
}