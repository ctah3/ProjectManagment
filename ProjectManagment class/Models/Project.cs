using System;
using System.Collections.Generic;

namespace ProjectManagment_class.Models;

public partial class Project : Entity
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }
    public ProjectStatusEnum Status { get; set; }
    public int ManagerId { get; set; }

    public virtual User Manager { get; set; } = null!;

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
