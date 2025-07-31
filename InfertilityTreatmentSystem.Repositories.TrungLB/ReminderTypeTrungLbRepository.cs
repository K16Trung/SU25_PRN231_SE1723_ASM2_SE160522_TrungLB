using InfertilityTreatmentSystem.Repositories.TrungLB.Basic;
using InfertilityTreatmentSystem.Repositories.TrungLB.DBContext;
using InfertilityTreatmentSystem.Repositories.TrungLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Repositories.TrungLB
{
    public class ReminderTypeTrungLbRepository : GenericRepository<ReminderTypeTrungLb>
    {
        public ReminderTypeTrungLbRepository() => _context ??= new Su25Prn231Se1723G2InfertilityTreatmentServiceContext();
        public ReminderTypeTrungLbRepository(Su25Prn231Se1723G2InfertilityTreatmentServiceContext context) => _context = context;
    }
}
