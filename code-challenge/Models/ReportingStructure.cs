using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge.Models
{
    public class ReportingStructure
    {

        [Key]
        public String EmployeeId { get; set; }
   
        public Employee Employee { get; set; }

        public int  NumberOfReports { get; set; }
    }
}
