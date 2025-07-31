using InfertilityTreatmentSystem.Repositories.TrungLB.Basic;
using InfertilityTreatmentSystem.Repositories.TrungLB.DBContext;
using InfertilityTreatmentSystem.Repositories.TrungLB.ModelExtensions;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Repositories.TrungLB
{
    public class TreatmentReminderTrungLbRepository : GenericRepository<TreatmentReminderTrungLb>
    {
        public TreatmentReminderTrungLbRepository() => _context ??= new Su25Prn231Se1723G2InfertilityTreatmentServiceContext();
        public TreatmentReminderTrungLbRepository(Su25Prn231Se1723G2InfertilityTreatmentServiceContext context) => _context = context;

        public async Task<List<TreatmentReminderTrungLb>> GetAllAsync()
        {
            var treatmentReminders = await _context.TreatmentReminderTrungLbs
                .Include(tr => tr.ReminderType)
                .ToListAsync();
            return treatmentReminders ?? new List<TreatmentReminderTrungLb>();
        }

        public async Task<PaginationResult<List<TreatmentReminderTrungLb>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            var treatmentReminders = await this.GetAllAsync();

            var totalItems = treatmentReminders.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            treatmentReminders = treatmentReminders.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<List<TreatmentReminderTrungLb>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = treatmentReminders
            };
        }

        public async Task<TreatmentReminderTrungLb?> GetByIdAsync(int id)
        {
            var treatmentReminder = await _context.TreatmentReminderTrungLbs
                .Include(tr => tr.ReminderType)
                .FirstOrDefaultAsync(tr => tr.ReminderId == id);
            return treatmentReminder;
        }

        public async Task<List<TreatmentReminderTrungLb>> SearchAsync(string? title, DateTime? reminderDate, int? reminderTypeId)
        {
            var query = _context.TreatmentReminderTrungLbs
                .Include(tr => tr.ReminderType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(tr => tr.Title != null && tr.Title.Contains(title));
            }

            if (reminderDate.HasValue)
            {
                query = query.Where(tr => tr.ReminderDate.HasValue && tr.ReminderDate.Value.Date == reminderDate.Value.Date);
            }

            if (reminderTypeId.HasValue)
            {
                query = query.Where(tr => tr.ReminderTypeId == reminderTypeId.Value);
            }

            var treatmentReminders = await query.ToListAsync();
            return treatmentReminders ?? new List<TreatmentReminderTrungLb>();
        }

        public async Task<List<TreatmentReminderTrungLb>> SearchAsync(SearchTreatmentReminderRequest searchRequest)
        {
            var query = _context.TreatmentReminderTrungLbs
                .Include(tr => tr.ReminderType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchRequest.Title))
            {
                query = query.Where(tr => tr.Title != null && tr.Title.Contains(searchRequest.Title));
            }

            if (searchRequest.ReminderDate.HasValue)
            {
                query = query.Where(tr => tr.ReminderDate.HasValue && tr.ReminderDate.Value.Date == searchRequest.ReminderDate.Value.Date);
            }

            if (searchRequest.IsSent.HasValue)
            {
                query = query.Where(tr => tr.IsSent == searchRequest.IsSent);
            }

            if (searchRequest.IsRecurring.HasValue)
            {
                query = query.Where(tr => tr.IsRecurring == searchRequest.IsRecurring);
            }

            if (!string.IsNullOrEmpty(searchRequest.PatientName))
            {
                query = query.Where(tr => tr.PatientName != null && tr.PatientName.Contains(searchRequest.PatientName));
            }

            if (!string.IsNullOrEmpty(searchRequest.RelatedDoctor))
            {
                query = query.Where(tr => tr.RelatedDoctor != null && tr.RelatedDoctor.Contains(searchRequest.RelatedDoctor));
            }

            if (searchRequest.ReminderTypeId.HasValue)
            {
                query = query.Where(tr => tr.ReminderTypeId == searchRequest.ReminderTypeId);
            }

            var treatmentReminders = await query.ToListAsync();
            return treatmentReminders ?? new List<TreatmentReminderTrungLb>();
        }

        public async Task<PaginationResult<List<TreatmentReminderTrungLb>>> SearchWithPagingAsync(
            string? title, DateTime? reminderDate, int? reminderTypeId, int currentPage, int pageSize)
        {
            var treatmentReminders = await SearchAsync(title, reminderDate, reminderTypeId);

            var totalItems = treatmentReminders.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            treatmentReminders = treatmentReminders.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<List<TreatmentReminderTrungLb>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = treatmentReminders
            };
        }

        public async Task<PaginationResult<List<TreatmentReminderTrungLb>>> SearchWithPagingAsync(SearchTreatmentReminderRequest searchRequest)
        {
            var treatmentReminders = await SearchAsync(searchRequest);

            var totalItems = treatmentReminders.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.PageSize.GetValueOrDefault());

            treatmentReminders = treatmentReminders.Skip((searchRequest.CurrentPage.GetValueOrDefault() - 1) * searchRequest.PageSize.GetValueOrDefault())
                .Take(searchRequest.PageSize.GetValueOrDefault()).ToList();

            return new PaginationResult<List<TreatmentReminderTrungLb>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = searchRequest.CurrentPage.GetValueOrDefault(),
                PageSize = searchRequest.PageSize.GetValueOrDefault(),
                Items = treatmentReminders
            };
        }

        // Override RemoveAsync to handle Entity Framework tracking issues
        public async Task<bool> RemoveAsync(TreatmentReminderTrungLb entity)
        {
            try
            {
                // Clear any existing tracking to avoid conflicts
                _context.ChangeTracker.Clear();
                
                // Find the entity in the current context to ensure proper tracking
                var trackedEntity = await _context.TreatmentReminderTrungLbs.FindAsync(entity.ReminderId);
                
                if (trackedEntity == null)
                {
                    return false;
                }
                
                // Remove the tracked entity
                _context.TreatmentReminderTrungLbs.Remove(trackedEntity);
                
                // Save changes
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Delete error in RemoveAsync: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw; // Re-throw to let the service layer handle it
            }
        }
    }
}