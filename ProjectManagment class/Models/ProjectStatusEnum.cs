using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment_class.Models
{
    public enum ProjectStatusEnum
    {
        [Display(Name = "Active")]
        Active,

        [Display(Name = "Closed")]
        Closed
    }
}
