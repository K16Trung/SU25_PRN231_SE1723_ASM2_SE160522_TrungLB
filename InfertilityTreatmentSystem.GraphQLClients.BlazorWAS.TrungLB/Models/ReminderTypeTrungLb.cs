using System;
using System.Collections.Generic;

namespace InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Models;

public partial class ReminderTypeTrungLb
{
    public int ReminderTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TreatmentReminderTrungLb> TreatmentReminderTrungLbs { get; set; } = new List<TreatmentReminderTrungLb>();
}

// Input DTO for GraphQL mutations (excludes navigation properties)
public partial class ReminderTypeInput
{
    public int ReminderTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public partial class ReminderTypeTrungLbsGraphQLResponse
{
    public List<ReminderTypeTrungLb> reminderTypeTrungLbs { get; set; } = new();
}

public partial class ReminderTypeTrungLbGraphQLResponse
{
    public ReminderTypeTrungLb reminderTypeByIds { get; set; } = new();
}

public partial class CreateReminderTypeGraphQLResponse
{
    public int createReminderTypeTrungLbs { get; set; }
}

public partial class UpdateReminderTypeGraphQLResponse
{
    public int updateReminderTypeTrungLbs { get; set; }
}

public partial class DeleteReminderTypeGraphQLResponse
{
    public bool deleteReminderTypeTrungLbs { get; set; }
}
