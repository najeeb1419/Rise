using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class Designation
    {
        [Key]
        public int DesignationID { get; set; }
        public string Name { get; set; }
        public int TotalLeavesDays { get; set; }
        public string companyid { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}