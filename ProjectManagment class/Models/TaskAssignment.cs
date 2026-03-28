using System;
using System.Collections.Generic;

namespace ProjectManagment_class.Models;

public partial class TaskAssignment : Entity
{
    public int AssignmentId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public DateOnly AssignedDate { get; set; }

    public bool IsLead { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
