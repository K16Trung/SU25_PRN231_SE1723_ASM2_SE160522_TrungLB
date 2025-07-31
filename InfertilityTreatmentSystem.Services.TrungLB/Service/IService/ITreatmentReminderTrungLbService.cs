using InfertilityTreatmentSystem.Repositories.TrungLB.ModelExtensions;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service.IService
{
    public interface ITreatmentReminderTrungLbService
    {
        #region Read Data

        Task<List<TreatmentReminderTrungLb>> GetAllAsync();
        Task<PaginationResult<List<TreatmentReminderTrungLb>>> GetAllWithPagingAsync(int currentPage, int pageSize);
        Task<TreatmentReminderTrungLb?> GetByIdAsync(int id);
        Task<PaginationResult<List<TreatmentReminderTrungLb>>> SearchWithPagingAsync(
            string? title, DateTime? reminderDate, int? reminderTypeId, int currentPage, int pageSize);
        Task<PaginationResult<List<TreatmentReminderTrungLb>>> SearchWithPagingAsync(SearchTreatmentReminderRequest searchRequest);

        #endregion

        #region Write Data

        Task<int> CreateAsync(TreatmentReminderTrungLb reminder);
        Task<int> UpdateAsync(TreatmentReminderTrungLb reminder);
        Task<bool> DeleteAsync(int id);

        #endregion
    }
}