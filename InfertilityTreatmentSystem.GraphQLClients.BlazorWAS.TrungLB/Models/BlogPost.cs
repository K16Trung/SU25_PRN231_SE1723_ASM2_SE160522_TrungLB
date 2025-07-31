using System;
using System.Collections.Generic;

namespace InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Models;

public partial class BlogPost
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Summary { get; set; }

    public string? AuthorName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsPublished { get; set; }

    public int? ViewCount { get; set; }

    public int? LikeCount { get; set; }

    public int? BlogTypeId { get; set; }

    public virtual BlogType? BlogType { get; set; }
}
