using InfertilityTreatmentSystem.Repositories.TrungLB.ModelExtensions;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using InfertilityTreatmentSystem.Services.TrungLB.Service;

namespace InfertilityTreatmentSystem_GraphQLAPIServices_TrungLB.GraphQLs
{
    public class Queries
    {
        private readonly IServiceProviders _serviceProviders;
        public Queries(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public async Task<List<TreatmentReminderTrungLb>> GetTreatmentReminderTrungLbs()
        {
            try
            {
                var result = await _serviceProviders.TreatmentReminderTrungLbService.GetAllAsync();
                return result ?? new List<TreatmentReminderTrungLb>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TreatmentReminderTrungLb> GetTreatmentReminderByIds(int id)
        {
            try
            {
                var result = await _serviceProviders.TreatmentReminderTrungLbService.GetByIdAsync(id);
                return result ?? new TreatmentReminderTrungLb();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TreatmentReminderTrungLb>> SearchTreatmentReminderTrungLbWithPagings(SearchTreatmentReminderRequestInput request)
        {
            try
            {
                // Convert input to repository model
                var searchRequest = new SearchTreatmentReminderRequest
                {
                    CurrentPage = request.CurrentPage,
                    PageSize = request.PageSize,
                    Title = request.Title,
                    ReminderDate = request.ReminderDate,
                    IsSent = request.IsSent,
                    IsRecurring = request.IsRecurring,
                    PatientName = request.PatientName,
                    RelatedDoctor = request.RelatedDoctor,
                    ReminderTypeId = request.ReminderTypeId
                };

                var result = await _serviceProviders.TreatmentReminderTrungLbService.SearchWithPagingAsync(searchRequest);
                return result.Items ?? new List<TreatmentReminderTrungLb>();
            }
            catch (Exception ex)
            {
            }
            return new List<TreatmentReminderTrungLb>();
        }

        public async Task<List<ReminderTypeTrungLb>> GetReminderTypeTrungLbs()
        {
            try
            {
                var result = await _serviceProviders.ReminderTypeTrungLbService.GetAllAsync();
                return result ?? new List<ReminderTypeTrungLb>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ReminderTypeTrungLb> GetReminderTypeByIds(int id)
        {
            try
            {
                var result = await _serviceProviders.ReminderTypeTrungLbService.GetByIdAsync(id);
                return result ?? new ReminderTypeTrungLb();
            }
            catch (Exception ex)
            {
            }
            return new ReminderTypeTrungLb();
        }

        // Authentication queries
        public async Task<SystemUserAccount?> AuthenticateUser(string username, string password)
        {
            try
            {
                // Use the existing authentication method
                var user = await _serviceProviders.SystemUserAccountService.GetAccountAsync(username, password);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    // GraphQL Input Types
    public class SearchTreatmentReminderRequestInput
    {
        public int? CurrentPage { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? Title { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool? IsSent { get; set; }
        public bool? IsRecurring { get; set; }
        public string? PatientName { get; set; }
        public string? RelatedDoctor { get; set; }
        public int? ReminderTypeId { get; set; }
    }
}
