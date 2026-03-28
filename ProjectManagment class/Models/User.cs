using System;
using System.Collections.Generic;

namespace ProjectManagment_class.Models;

public partial class User : Entity
{
    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Bio { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
