using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace MvcDashBoard.Model.DASHBOARD.Models
{
    public class EMPLOYEE_ATTENDTIME
    {
        public List<DASHBOARD_EMPLOYEE_ATTENDTIME> EMPLOYEE_ATTENDTIME_list { get; set; }
        public EMPLOYEE_ATTENDTIME_QUERY_CRITERIA EMPLOYEE_ATTENDTIME_Query_Criteria { get; set; }
    }

    public class EMPLOYEE_ATTENDTIME_QUERY_CRITERIA
    {
        public string FACTORY_CD { get; set; }
        public string PRODUCTION_LINE_CD { get; set; }
        public string SHIFT { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> TRX_DATE { get; set; }
    }

    public class DashBoard_Insert_Data  //自定义类，用以存储数据
    {
        public string vv_Employee { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime vv_CardTime { get; set; }
    }

    public class GetEmployeeCheckTimeResult1 //GetEmployeeCheckTimeResponse  //自定义类，用以存储数据
    {
         //[XmlAttribute]

         //public string EmployeeCheckTime{get;set;}
         //[XmlElement]
         public string EmployeeCode { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
         public Nullable<System.DateTime> CheckTime { get; set; }

    }

    public class DASHBOARD_ATTENDANCE_LOG
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        //public int SEQNO { get; set; }
        public string FACTORY_CD { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public Nullable<System.DateTime> FROM_TIME { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public Nullable<System.DateTime> TO_TIME { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public Nullable<System.DateTime> CREATE_TIME { get; set; }
        public string PRODUCTION_LINE_CD { get; set; }
        public string STATUS { get; set; }
    }







}
