using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Models;

public partial class TreatmentReminderTrungLb
{
    public int ReminderId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string? Title { get; set; }

    [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
    public string? Message { get; set; }

    [Required(ErrorMessage = "Reminder date is required")]
    public DateTime? ReminderDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsSent { get; set; } = false;

    public bool IsRecurring { get; set; } = false;

    [Required(ErrorMessage = "Patient name is required")]
    [StringLength(100, ErrorMessage = "Patient name cannot exceed 100 characters")]
    public string? PatientName { get; set; }

    [StringLength(100, ErrorMessage = "Related doctor name cannot exceed 100 characters")]
    public string? RelatedDoctor { get; set; }

    public int? ReminderTypeId { get; set; }

    public virtual ReminderTypeTrungLb? ReminderType { get; set; }
}

// Input DTO for GraphQL mutations (excludes navigation properties)
public partial class TreatmentReminderInput
{
    public int ReminderId { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public DateTime? ReminderDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsSent { get; set; }
    public bool IsRecurring { get; set; }
    public string? PatientName { get; set; }
    public string? RelatedDoctor { get; set; }
    public int? ReminderTypeId { get; set; }
}

public partial class TreatmentReminderTrungLbsGraphQLResponse
{
    public List<TreatmentReminderTrungLb> treatmentReminderTrungLbs { get; set; } = new();
}

public partial class TreatmentReminderTrungLbGraphQLResponse
{
    public TreatmentReminderTrungLb treatmentReminderByIds { get; set; } = new();
}

public partial class CreateTreatmentReminderGraphQLResponse
{
    public int createTreatmentReminderTrungLbs { get; set; }
}

public partial class UpdateTreatmentReminderGraphQLResponse
{
    public int updateTreatmentReminderTrungLbs { get; set; }
}

public partial class DeleteTreatmentReminderGraphQLResponse
{
    public bool deleteTreatmentReminderTrungLbs { get; set; }
}

// Add search and paging models
public partial class SearchTreatmentReminderRequest
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

public partial class PaginationResult<T> where T : class
{
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public T Items { get; set; } = default!;
}

public partial class SearchTreatmentReminderTrungLbWithPagingsGraphQLResponse
{
    public List<TreatmentReminderTrungLb> searchTreatmentReminderTrungLbWithPagings { get; set; } = new();
}