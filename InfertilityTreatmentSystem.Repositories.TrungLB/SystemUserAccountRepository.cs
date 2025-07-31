using InfertilityTreatmentSystem.Repositories.TrungLB.Basic;
using InfertilityTreatmentSystem.Repositories.TrungLB.DBContext;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Repositories.TrungLB
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() => _context ??= new Su25Prn231Se1723G2InfertilityTreatmentServiceContext();
        public SystemUserAccountRepository(Su25Prn231Se1723G2InfertilityTreatmentServiceContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccountAsync(string username, string password)
        {
            try
            {
                // Debug: Log what we're searching for
                Console.WriteLine($"Searching for user: '{username}' with password: '{password}'");
                
                // First, get all users to debug
                var allUsers = await _context.SystemUserAccounts.ToListAsync();
                Console.WriteLine($"Total users in database: {allUsers.Count}");
                
                foreach (var u in allUsers)
                {
                    Console.WriteLine($"User: '{u.UserName}', Password: '{u.Password}', IsActive: {u.IsActive}");
                }

                // Direct match
                var user = await _context.SystemUserAccounts
                    .FirstOrDefaultAsync(x => x.UserName == username && x.Password == password && x.IsActive == true);

                if (user != null)
                {
                    Console.WriteLine($"Found user: {user.UserName}");
                    return user;
                }

                Console.WriteLine("User not found with exact match");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw new Exception($"Database error during authentication: {ex.Message}", ex);
            }
        }
    }
}