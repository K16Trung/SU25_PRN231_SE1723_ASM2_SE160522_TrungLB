using System;
using System.Collections.Generic;

namespace InfertilityTreatmentSystem.Repositories.TrungLB.Models;

public partial class ReminderTypeTrungLb
{
    public int ReminderTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TreatmentReminderTrungLb> TreatmentReminderTrungLbs { get; set; } = new List<TreatmentReminderTrungLb>();
}
