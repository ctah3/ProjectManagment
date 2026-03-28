using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment_class.Models
{
    internal class Status : Entity
    {
        public ProjectStatusEnum statusType { get; set; }
    }
}
