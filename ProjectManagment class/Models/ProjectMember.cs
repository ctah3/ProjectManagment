using System;
using System.Collections.Generic;

namespace ProjectManagment_class.Models;

public partial class ProjectMember : Entity
{
    public int ProjectMembersId { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public DateOnly JoinDate { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
