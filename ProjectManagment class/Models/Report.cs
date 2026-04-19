using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment_class.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int AssignmentId { get; set; }

    [Display(Name = "Дата створення")]
    public DateTime CreatedAt { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Зміст")]
    public string Content { get; set; } = null!;
    [Display(Name = "Назначення")]
    public virtual TaskAssignment Assignment { get; set; } = null!;
}
