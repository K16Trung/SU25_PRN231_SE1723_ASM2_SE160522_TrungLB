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
            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(x => x.Email == username && x.Password == password && x.IsActive);

            return await _context.SystemUserAccounts
                .FirstOrDefaultAsync(x => x.UserName == username && x.Password == password && x.IsActive == true);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(x => x.Phone == username && x.Password == password);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(x => x.EmployeeCode == username && x.Password == password);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(x => x.UserName == username && x.Password == password && x.IsActive);
        }
    }
}