using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Repositories.TrungLB.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
    public class SearchTreatmentReminderRequest : SearchRequest
    {
        public string? Title { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool? IsSent { get; set; }
        public bool? IsRecurring { get; set; }
        public string? PatientName { get; set; }
        public string? RelatedDoctor { get; set; }
        public int? ReminderTypeId { get; set; }
    }
}
