using InfertilityTreatmentSystem.Repositories.TrungLB;
using InfertilityTreatmentSystem.Repositories.TrungLB.ModelExtensions;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using InfertilityTreatmentSystem.Services.TrungLB.Service.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service
{
    public class TreatmentReminderTrungLbService : ITreatmentReminderTrungLbService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TreatmentReminderTrungLbService() => _unitOfWork ??= new UnitOfWork();
        public TreatmentReminderTrungLbService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        #region Read Data

        public async Task<List<TreatmentReminderTrungLb>> GetAllAsync()
        {
            return await _unitOfWork.TreatmentReminderRepository.GetAllAsync();
        }

        public async Task<PaginationResult<List<TreatmentReminderTrungLb>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            return await _unitOfWork.TreatmentReminderRepository.GetAllWithPagingAsync(currentPage, pageSize);
        }

        public async Task<TreatmentReminderTrungLb?> GetByIdAsync(int id)
        {
            return await _unitOfWork.TreatmentReminderRepository.GetByIdAsync(id);
        }

        public async Task<List<TreatmentReminderTrungLb>> SearchAsync(string? title, DateTime? reminderDate, int? reminderTypeId)
        {
            return await _unitOfWork.TreatmentReminderRepository.SearchAsync(title, reminderDate, reminderTypeId);
        }

        public async Task<PaginationResult<List<TreatmentReminderTrungLb>>> SearchWithPagingAsync(
            string? title, DateTime? reminderDate, int? reminderTypeId, int currentPage, int pageSize)
        {
            return await _unitOfWork.TreatmentReminderRepository.SearchWithPagingAsync(
                title, reminderDate, reminderTypeId, currentPage, pageSize);
        }

        public async Task<List<TreatmentReminderTrungLb>> SearchAsync(SearchTreatmentReminderRequest searchRequest)
        {
            return await _unitOfWork.TreatmentReminderRepository.SearchAsync(searchRequest);
        }

        public async Task<PaginationResult<List<TreatmentReminderTrungLb>>> SearchWithPagingAsync(SearchTreatmentReminderRequest searchRequest)
        {
            return await _unitOfWork.TreatmentReminderRepository.SearchWithPagingAsync(searchRequest);
        }

        #endregion

        #region Write Data

        public async Task<int> CreateAsync(TreatmentReminderTrungLb reminder)
        {
            return await _unitOfWork.TreatmentReminderRepository.CreateAsync(reminder);
        }

        public async Task<int> UpdateAsync(TreatmentReminderTrungLb reminder)
        {
            return await _unitOfWork.TreatmentReminderRepository.UpdateAsync(reminder);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _unitOfWork.TreatmentReminderRepository.GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            return await _unitOfWork.TreatmentReminderRepository.RemoveAsync(item);
        }

        #endregion
    }
}