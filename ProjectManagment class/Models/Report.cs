using System;
using System.Collections.Generic;

namespace ProjectManagment_class.Models;

public partial class Report : Entity
{
    public int ReportId { get; set; }

    public int AssignmentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Content { get; set; } = null!;

    public virtual TaskAssignment Assignment { get; set; } = null!;
}
