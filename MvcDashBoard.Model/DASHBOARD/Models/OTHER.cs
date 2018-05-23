using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDashBoard.Model.DASHBOARD.Models
{
    public class OTHER
    {
      public partial class PROC_GET_DASHBOARD_FORM_DEFINITION
      {
        [Key]
        public string CONTROLLER_ID { get; set; }
        public string CONTROLLER_DESC { get; set; }
      }
    }
}
