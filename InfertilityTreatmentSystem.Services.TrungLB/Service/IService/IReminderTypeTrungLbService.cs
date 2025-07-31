using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service.IService
{
    public interface IReminderTypeTrungLbService
    {
        Task<int> CreateAsync(ReminderTypeTrungLb entity);
        Task<int> UpdateAsync(ReminderTypeTrungLb entity);
        Task<bool> DeleteAsync(int id);
        public Task<List<ReminderTypeTrungLb>> GetAllAsync();
        Task<ReminderTypeTrungLb?> GetByIdAsync(int id);
    }
}
