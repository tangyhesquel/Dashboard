//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace MvcDashBoard.WebUI.Models
//{
//    public partial class DASHBOARD_Data
//    {
//        [Key]
//        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
//        public string factory_cd { get; set; }
//        public string line { get; set; }
//        public Nullable<System.DateTime> trsdate { get; set; }
//        public string shift_cd { get; set; }
//        public int HeadCount { get; set; }
//        public string GONO { get; set; }
//        public decimal SAH { get; set; }
//        public int ACTUAL_QTY { get; set; }
//        public int GOOD_GARMENT_QTY { get; set; }
//        public string WORKMANSHIP_DEFECT_QTY { get; set; }
//        public int INSPECT_QTY { get; set; }
//        public int Target_QTY { get; set; }
//        public decimal Eff { get; set; }
//        public decimal Fpy { get; set; }

//    }
//}


////[Table("ACCESSORY_HOLD_REASON")]
////public partial class ACCESSORY_HOLD_REASON
////{
////    [Key]
////    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
////    public int ACCESSORY_HOLD_REASON_ID { get; set; }
////    public int ACCESSORY_PROD_TRACKING_ID { get; set; }
////    public Nullable<int> HOLD_REASON_ID { get; set; }
////    public Nullable<System.DateTime> START_HOLD_DATE { get; set; }
////    public Nullable<System.TimeSpan> START_HOLD_TIME { get; set; }
////    public Nullable<System.DateTime> END_HOLD_DATE { get; set; }
////    public Nullable<System.TimeSpan> END_HOLD_TIME { get; set; }
////    public string HELD_BY { get; set; }
////    public Nullable<int> HELD_QUANTITY { get; set; }
////}