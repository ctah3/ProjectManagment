using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment_class.Models;

public partial class Task
{
    public int TaskId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Назва")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Опис")]
    public string Description { get; set; } = null!;

    [Display(Name = "Пріорітет")]
    public int Priority { get; set; }

    [Display(Name = "Дедлайн")]
    public DateTime Deadline { get; set; }
    [Display(Name = "Проєкт")]
    public int ProjectId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Проєкт")]
    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
