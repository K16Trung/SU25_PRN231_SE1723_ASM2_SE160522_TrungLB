using System;
using System.Collections.Generic;

namespace InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Models;

public partial class BlogType
{
    public int BlogTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}
