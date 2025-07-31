using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using InfertilityTreatmentSystem.Services.TrungLB.Service;

namespace InfertilityTreatmentSystem_GraphQLAPIServices_TrungLB.GraphQLs
{
    public class Mutations
    {
        private readonly IServiceProviders _serviceProviders;
        public Mutations(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public async Task<int> CreateTreatmentReminderTrungLbs(TreatmentReminderTrungLb request)
        {
            try
            {
                var result = await _serviceProviders.TreatmentReminderTrungLbService.CreateAsync(request);
                return result;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
        public async Task<int> UpdateTreatmentReminderTrungLbs(TreatmentReminderTrungLb request)
        {
            try
            {
                var result = await _serviceProviders.TreatmentReminderTrungLbService.UpdateAsync(request);
                return result;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public async Task<bool> DeleteTreatmentReminderTrungLbs(int id)
        {
            try
            {
                Console.WriteLine($"Server: Attempting to delete treatment reminder with ID: {id}");
                var result = await _serviceProviders.TreatmentReminderTrungLbService.DeleteAsync(id);
                Console.WriteLine($"Server: Delete operation result: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server: Error deleting treatment reminder: {ex.Message}");
                throw; // Re-throw to expose the actual error
            }
        }

        public async Task<int> CreateReminderTypeTrungLbs(ReminderTypeTrungLb request)
        {
            try
            {
                var result = await _serviceProviders.ReminderTypeTrungLbService.CreateAsync(request);
                return result;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public async Task<int> UpdateReminderTypeTrungLbs(ReminderTypeTrungLb request)
        {
            try
            {
                var result = await _serviceProviders.ReminderTypeTrungLbService.UpdateAsync(request);
                return result;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public async Task<bool> DeleteReminderTypeTrungLbs(int id)
        {
            try
            {
                var result = await _serviceProviders.ReminderTypeTrungLbService.DeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
