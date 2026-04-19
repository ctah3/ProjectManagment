using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment_class.Models;

public partial class User
{
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "ФІО")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Біографія")]
    public string? Bio { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Номер Телефону")]
    public string Phone { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Електрона пошта")]
    public string Email { get; set; } = null!;
  
    public int UserId { get; set; }
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
