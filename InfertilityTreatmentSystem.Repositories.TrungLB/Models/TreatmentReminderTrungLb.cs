using System;
using System.Collections.Generic;

namespace InfertilityTreatmentSystem.Repositories.TrungLB.Models;

public partial class TreatmentReminderTrungLb
{
    public int ReminderId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public DateTime? ReminderDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsSent { get; set; }

    public bool? IsRecurring { get; set; }

    public string? PatientName { get; set; }

    public string? RelatedDoctor { get; set; }

    public int? ReminderTypeId { get; set; }

    public virtual ReminderTypeTrungLb? ReminderType { get; set; }
}
