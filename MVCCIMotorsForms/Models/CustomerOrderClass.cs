using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCCIMotorsForms.Models
{
    public class CustomerORderClass
    {
        public int StaffId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name ")]
        public string LastName { get; set; }
        [Display(Name = "SaleOrderID  ")]
        public int SaleOrderID { get; set; }
        [Display(Name = "OrderDate  ")]
        public DateTime OrderDate { get; set; }
    }
}