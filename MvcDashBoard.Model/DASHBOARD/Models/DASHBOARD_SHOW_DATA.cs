using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDashBoard.Model.DASHBOARD.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DASHBOARD_SHOW_DATA
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result {get;set;}
        public FN_DASHBOARD_SHOW_DATA_Result FN_DASHBOARD_SHOW_DATA_Result {get;set;}
        public List<FN_DASHBOARD_TIME_INTERVAL_QTY_Result> FN_DASHBOARD_TIME_INTERVAL_QTY_Result { get; set; }
        public BASICINFORMATION_DATA BASICINFORMATION_data { get; set; }
        public RUNNING_BASIC_INFORMATION RUNNING_BASIC_INFORMATION { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime DbServerDate { get; set; }
        //public SHIFT_CODE_BY_TIME SHIFT_CODE_BY_TIME { get; set; }
        public List<DEFECT_TOP> DEFECT_TOP { get; set; }

        public DASHBOARD_SHOW_DATA()
        {
            DbServerDate = DateTime.Now;
        }
    }

    public class TargetOutPut  // add by sunny 20180312
    {
        public int Total_Product_Qty { get; set; }
    }

    public class DEFECT_TOP
    {
        public string DEFECT_GROUP { get; set; }
        public string PART_NAME { get; set; }
        public int QTY { get; set; }
    }

    public class SHIFT_CODE_BY_TIME
    {
        public string FACTORY_CD { get; set; }
        public string SHIFT_CODE { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TRX_DATE { get; set; }
    }

    public class RUNNING_BASIC_INFORMATION
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string FACTORY_CD { get; set; }
        public string GARMENT_TYPE { get; set; }
        public int REFRESH_INTERVAL { get; set; }
        public string LINE_CODE { get; set; }
        public string SHIFT_CODE { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TRX_DATE { get; set; }
        public string Only1Line { get; set; }
        public string LANGUAGE { get; set; }
        public int HR_REFRESH_INTERVAL { get; set; }
        public int HR_MAX_TIME_DIFFERENCE { get; set; }
        public int TARGET_TOTAL_QTY { get; set; }
        public decimal TARGET_WORK_HOUR { get; set; }
        public string DISPLAY_TARGET2_DEFECT { get; set; }
        public string USE_LINE_TARGET2 { get; set; }
        public string CHANGESHIFT { get; set; }

        public RUNNING_BASIC_INFORMATION()
        {
            FACTORY_CD = "NO";
            GARMENT_TYPE = "W";
            REFRESH_INTERVAL = 5;
            LINE_CODE = "";
            SHIFT_CODE = "";
            LINE_CODE= "";
            TRX_DATE = DateTime.Now;
            Only1Line="Y";
            LANGUAGE = "EN";
            HR_REFRESH_INTERVAL = 20; //分钟
            HR_MAX_TIME_DIFFERENCE = 2;//小时
            //DISPLAY_TARGET2_DEFECT = "N";
            //USE_LINE_TARGET2 = "N";
            CHANGESHIFT = "N";
        }
    }

    public class BASICINFORMATION_DATA
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string FACTORY_CD { get; set; }
        public string GARMENT_TYPE { get; set; }
        public int REFRESH_INTERVAL { get; set; }
        public string LINE_CODE1 { get; set; }
        public string SHIFT_CODE1 { get; set; }
        public string LINE_CODE2 { get; set; }
        public string SHIFT_CODE2 { get; set; }
        public string LINE_CODE3 { get; set; }
        public string SHIFT_CODE3 { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SHOWED_DATE3 { get; set; }
        public string LANGUAGE { get; set; }
        public int HR_REFRESH_INTERVAL { get; set; }
        public int HR_MAX_TIME_DIFFERENCE { get; set; }
        public int TARGET_TOTAL_QTY { get; set; }
        public decimal TARGET_WORK_HOUR { get; set; }
        public string DISPLAY_TARGET2_DEFECT { get; set; }
        public string USE_LINE_TARGET2 { get; set; }


        public BASICINFORMATION_DATA()
        {
            FACTORY_CD = "";
            GARMENT_TYPE = "W";
            REFRESH_INTERVAL = 5;
            LINE_CODE1 = "";
            SHIFT_CODE1 = "";
            LINE_CODE2 = "";
            SHIFT_CODE2 = "";
            SHIFT_CODE3 = "";
            LINE_CODE3 = "";
            SHOWED_DATE3 = DateTime.Now;
            LANGUAGE = "EN";
            HR_REFRESH_INTERVAL = 60; //分钟
            HR_MAX_TIME_DIFFERENCE = 2;//小时
            TARGET_TOTAL_QTY = 1000;
            TARGET_WORK_HOUR = 10;
            DISPLAY_TARGET2_DEFECT = "N";
            USE_LINE_TARGET2 = "N";
        }
    }

 }
