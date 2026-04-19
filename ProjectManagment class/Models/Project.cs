using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment_class.Models;

public partial class Project
{
    public int ProjectId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name ="Назва")]
    public string ProjectName { get; set; } = null!;
    [Display(Name = "Опис")]
    public string Description { get; set; } = null!;
    [Display(Name = "Дата початку")]
    public DateTime? DateStart { get; set; }
    [Display(Name = "Дата завершення")]
    public DateTime? DateEnd { get; set; }
    [Display(Name = "Менеджер")]
    public int ManagerId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Менеджер")]
    public virtual User Manager { get; set; } = null!;
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
