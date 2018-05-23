using MvcDashBoard.Model.DASHBOARD.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System.Text;
using MvcDashBoard.Model;
using MvcDashBoard.Model.DASHBOARD;


namespace MvcDashBoard.DAL.DASHBOARD
{
   public class DashboardDAL
    {
       public ApplicationDbContext db
        {
            get;
            set;
        }
       public string DBStr;
       public DashboardDAL(string FACTORY_CD)
        {
            DBStr = FACTORY_CD + "_DBCONN";
            db = new ApplicationDbContext(DBStr);
        }

       public List<FN_DASHBOARD_TIME_INTERVAL_QTY_Result> Get_DashBoard_Time_Interval_Qty(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE,int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR)
       {
           try
           {
               string query = "select * from dbo.FN_DASHBOARD_TIME_INTERVAL_QTY('" + FactoryCd + "','" + Line + "','" + SHIFT + "','" + TRX_DATE + "'," + TARGET_TOTAL_QTY + "," + TARGET_WORK_HOUR + ")";
               return db.Database.SqlQuery<FN_DASHBOARD_TIME_INTERVAL_QTY_Result>(query).ToList();
               //db.Database.ExecuteSqlCommand(query);
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<DEFECT_TOP> Get_DEFECT_TOP(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE)
       {
           try
           {
               string query = "select top 3 DEFECT_GROUP,PART_NAME,QTY from dbo.FN_DASHBOARD_DEFECT_TOP3('" + FactoryCd + "','" + Line + "','" + SHIFT + "','" + TRX_DATE + "') ORDER BY QTY desc";
               return db.Database.SqlQuery<DEFECT_TOP>(query).ToList();
               //db.Database.ExecuteSqlCommand(query);
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public string PROC_DASHBOARD_GET_PRODUCTION_QTY(string FactoryCd, String Line)
       {
                   using (TransactionScope ts = new TransactionScope())
                   {
                       try
                       {
                           string query = "exec PROC_DASHBOARD_GET_PRODUCTION_QTY  '" + FactoryCd + "','" + Line + "'";
                           db.Database.CommandTimeout = 60 * 1000;
                           db.Database.ExecuteSqlCommand(query);
                           ts.Complete();
                       }
                       catch (Exception ex)
                       {
                           return ex.ToString();
                       }
                       finally
                       {
                           ts.Dispose();
                       }
                       return "successfully";
                   }
       }

       public string PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE,string Only1Line)
       {
           using (TransactionScope ts = new TransactionScope())
           {
               try
               {
                   string query = "exec PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME '" + FactoryCd + "','" + Line + "','" + SHIFT + "','" + TRX_DATE + "','" + Only1Line + "'";
                   db.Database.ExecuteSqlCommand(query);
                   ts.Complete();
               }
               catch (Exception ex)
               {
                   return ex.ToString();
               }
               finally
               {
                   ts.Dispose();
               }
               return "successfully";
           }
       }


       public FN_DASHBOARD_SHOW_DATA_Result Get_DashBoard_Data(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE, int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR)
       { 
           try
           {
               string query = "select * from dbo.FN_DASHBOARD_SHOW_DATA('" + FactoryCd + "','" + Line + "','" + SHIFT + "','" + TRX_DATE + "',"+TARGET_TOTAL_QTY+","+TARGET_WORK_HOUR+")";
               return db.Database.SqlQuery<FN_DASHBOARD_SHOW_DATA_Result>(query).Single();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result Get_DashBoard_Previous_Data(string FactoryCd, String Line, string SHIFT, DateTime? TRX_DATE, int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR)
       {
           try
           {
               string query = "select * from dbo.FN_DASHBOARD_SHOW_PREVIOUS_DATA('" + FactoryCd + "','" + Line + "','" + SHIFT + "','" + TRX_DATE + "'," + TARGET_TOTAL_QTY + "," + TARGET_WORK_HOUR + ")";
               return db.Database.SqlQuery<FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result>(query).Single();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<FACTORY_CD_DATA> Get_FactoryCD_List(string factorycd)
       {
           try
           {
               string query = "select FACTORY_ID as FACTORY_CD from FN_DASHBOARD_FACTORY('" + factorycd + "')";
               return db.Database.SqlQuery<FACTORY_CD_DATA>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<PRODUCTION_LINE_CD_DATA> Get_LINECD_List(string factorycd, string linecd)
       {
           try
           {
               string query = "select PRODUCTION_LINE_CD from FN_DASHBOARD_LINE('" + factorycd + "','" + linecd + "')";
               return db.Database.SqlQuery<PRODUCTION_LINE_CD_DATA>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<GARMENT_TYPE_DATA> Get_GARMENT_TYPE_List()
       {
           try
           {
               string query = "select GARMENT_TYPE from FN_DASHBOARD_GARMENT_TYPE()";
               return db.Database.SqlQuery<GARMENT_TYPE_DATA>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<SHIFT_CODE_DATA> Get_SHIFT_CODE_List(string factorycd)
       {
           try
           {
               string query = "select SHIFT_CODE from FN_SHIFT_CODE('" + factorycd + "')";
               return db.Database.SqlQuery<SHIFT_CODE_DATA>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<DASHBOARD_EMPLOYEE_ATTENDTIME> Get_DASHBOARD_EMPLOYEE_ATTENDTIME_List(string FACTORY_CD, String LINE, string SHIFT, DateTime? TRX_DATE)
       {
           try
           {
               string query;
               query = "";
               query = query + " select * from DASHBOARD_EMPLOYEE_ATTENDTIME with (nolock) ";
               query = query + " where Factory_cd='" + FACTORY_CD + "'";
               query = query + " and TRX_DATE='" + TRX_DATE + "'";
               query = query + " and SHIFT='" + SHIFT + "'";
               query = query + " and PRODUCTION_LINE_CD='" + LINE + "'";
               return db.Database.SqlQuery<DASHBOARD_EMPLOYEE_ATTENDTIME>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<DASHBOARD_LINE_SHIFT_SETTING> InquirySHIFTData(string FACTORY_CD, string SHIFT)
       {
           try
           {
               string query;
               query = "";
               query = query + " select * from DASHBOARD_LINE_SHIFT_SETTING with (nolock) ";
               query = query + " where (ACTIVE='Y' or isnull(ACTIVE,'')='') and Factory_cd='" + FACTORY_CD + "'";
               query = query + " and SHIFT_CODE='" + SHIFT + "' order by SHIFT_SEQ desc";
               return db.Database.SqlQuery<DASHBOARD_LINE_SHIFT_SETTING>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public string DeleteSHIFTData(string FACTORY_CD, string SHIFT, string SEQNO)
       {
           try
           {
               string query;
               query = "";
               query = query + " delete from DASHBOARD_LINE_SHIFT_SETTING ";
               query = query + " where  Factory_cd='" + FACTORY_CD + "'";
               query = query + " and SHIFT_CODE='" + SHIFT + "'";
               query = query + " and SEQNO=" +  int.Parse(SEQNO) ;
               db.Database.ExecuteSqlCommand(query);
               return "successfully";
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public string UpdateShiftData(int SEQNO,string FACTORY_CD, string SHIFT_CODE, string SHIFT_DESC, int SHIFT_SEQ, string SHIFT_FROM, string SHIFT_TO, decimal TIME_INTERVAL,string ACTIVE)
       {
           try
           {
               string query;
               DateTime date1;
               DateTime date2;
               date1 = DateTime.ParseExact(SHIFT_FROM, "HH:mm:ss", null);
               date2 = DateTime.ParseExact(SHIFT_TO, "HH:mm:ss", null);

               query = "exec PROC_DASHBOARD_UPDATESHIFTDATE " + SEQNO;
               query = query +",'"+FACTORY_CD;
               query = query + "','" + SHIFT_CODE;
               query = query + "','" + SHIFT_DESC;
               query = query + "'," + SHIFT_SEQ;
               query = query + ",'" + SHIFT_FROM;
               query = query + "','" + SHIFT_TO;
               query = query + "'," + TIME_INTERVAL;
               query = query + ",'" + ACTIVE;
               query = query + "'";
               db.Database.ExecuteSqlCommand(query);
               return "successfully";
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }


       public string UPDATE_ATTENDTIME(string SEQNO, String SHIFT, string TRX_DATE, string PRODUCTION_LINE_CD)
       {
           try
           {
               string query;
               DateTime date1;
               date1 = DateTime.ParseExact(TRX_DATE, "yyyy-MM-dd", null);
               query = "";
               query = query + " update DASHBOARD_EMPLOYEE_ATTENDTIME ";
               query = query + " set SHIFT='" + SHIFT + "',TRX_DATE='" + date1 + "',PRODUCTION_LINE_CD='" + PRODUCTION_LINE_CD + "'";
               query = query + " where SEQNO=" + SEQNO;
               db.Database.ExecuteSqlCommand(query);
               return "successfully";
           }
           catch (Exception ex)
           {
               return "not successfully";
           }
           finally
           {
           }
       }

       
       public string INSERT_ATTENDTIME(List<DASHBOARD_EMPLOYEE_ATTENDTIME> DASHBOARD_EMPLOYEE_attendtime)
       {
           string query; 
           using (TransactionScope ts = new TransactionScope())
           {
               try
               {
                   query = " begin " + "\n\r";
                   query = query + " begin transaction " + "\n\r";
                   foreach (DASHBOARD_EMPLOYEE_ATTENDTIME temp in DASHBOARD_EMPLOYEE_attendtime)
                   {
                       //db.Set<DASHBOARD_EMPLOYEE_ATTENDTIME>().Add(temp);
                       query = query + " insert into DASHBOARD_EMPLOYEE_ATTENDTIME (FACTORY_CD,EMPLOYEE_NO,ATTEND_DATE,HRIS_CREATE_TIME,CREATE_TIME) values (";
                       query = query + "'" + temp.FACTORY_CD + "',";
                       query = query + "'" + temp.EMPLOYEE_NO + "',";
                       query = query + "'" + temp.ATTEND_DATE + "',";
                       query = query + "'" + temp.HRIS_CREATE_TIME + "',";
                       query = query + "getdate())" + "\n\r";

                   }
                   query = query + " commit transaction " + "\n\r";
                   query = query + " end " + "\n\r";
                   db.Database.ExecuteSqlCommand(query);
                   //db.SaveChanges();
                   ts.Complete();
               }
               catch (Exception e)
               {
                   return e.ToString();
               }
               finally
               {
                   ts.Dispose();
               }

               return "successfully";
           }
       }

       public SHIFT_CODE_BY_TIME SHIFT_CODE_BY_TIME(string FACTORY_CD, string SHIFT, DateTime? TRX_DATE)
       {
               try
               {
                   string query;
                   query = "";
                   query = query + " select * from GET_SHIFT_CODE_BY_TIME('" + FACTORY_CD + "',null,null)";
                   return db.Database.SqlQuery<SHIFT_CODE_BY_TIME>(query).Single();
               }
               catch (Exception ex)
               {
                   return null;
               }
               finally
               {
               }
       }

       public SHIFT_CODE_BY_TIME SHIFT_CODE_BY_LINE_AND_TIME(string FACTORY_CD, string LINE,string SHIFT1,string SHIFT2, DateTime? TRX_DATE)
       {
           try
           {
               string query;
               query = "";
               query = query + " select * from GET_SHIFTCODE_BY_LINECODE_AND_TIME('" + FACTORY_CD +"','"+LINE+"','"+SHIFT1+"','"+SHIFT2+"',null)";
               return db.Database.SqlQuery<SHIFT_CODE_BY_TIME>(query).Single();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public TargetOutPut GET_TARGET_OUTPUT(string FACTORY_CD, string LINE_CODE, string PLAN_DATE)
       {
           try
           {
               string query;
               query = string.Format(@"
                            select isnull(convert(int,sum(A.PLAN_QTY)),0)  as Total_Product_Qty from FR_JO_DAILY_PLAN_DATA A LEFT join CP_FR_PRODUCT_LINE_MAPPING B
                             on A.SEWING_LINE=B.FR_SEWING_LINE_CD 
                             WHERE isnull(b.MES_PRODUCT_LINE_CD,a.SEWING_LINE)='{0}' and A.PLAN_DATE='{1}' AND A.FACTORY='{2}'
                           ", LINE_CODE, PLAN_DATE, FACTORY_CD);
              // return db.Database.SqlQuery<TargetOutPut>(query).Single();
               TargetOutPut kk = null;
               kk = db.Database.SqlQuery<TargetOutPut>(query).Single();
               return kk;

           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public IEnumerable<OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> PROC_GET_DASHBOARD_FORM_DEFINITION(string formName, string lang)
       {
           try
           {
               string query = "EXEC dbo.PROC_GET_DASHBOARD_FORM_DEFINITION @FormName = '" + formName + "', @lang = '" + lang + "'";
               IEnumerable<OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> data = db.Database.SqlQuery<OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION>(query);

               return data;
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public List<DASHBOARD_ATTENDANCE_LOG> Get_DASHBOARD_ATTENDANCE_LOG_LIST(string factorycd, string status)
       {
           try
           {
               string query = "select * from DASHBOARD_ATTENDANCE_LOG Where FACTORY_CD='" + factorycd + "' and isnull(STATUS,'N')='"+status+"')";
               return db.Database.SqlQuery<DASHBOARD_ATTENDANCE_LOG>(query).ToList();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public DASHBOARD_ATTENDANCE_LOG GET_DASHBOARD_ATTENDANCE_LOG(string factorycd, string linecd)
       {
           try
           {
               string query = "select * from FN_DASHBOARD_ATTENDANCE_LOG('" + factorycd + "','"+ linecd + "')";
               return db.Database.SqlQuery<DASHBOARD_ATTENDANCE_LOG>(query).Single();
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
           }
       }

       public string INSERT_DASHBOARD_ATTENDANCE_LOG(string factorycd, DateTime fromtime, DateTime totime, DateTime createtime, string linecd, string status)
       {
           using (TransactionScope ts = new TransactionScope())
           {
               try
               {
                   //DASHBOARD_ATTENDANCE_LOG DASHBOARD_ATTENDANCE_Log = new DASHBOARD_ATTENDANCE_LOG();
                   //DASHBOARD_ATTENDANCE_Log.FACTORY_CD = factorycd;
                   //DASHBOARD_ATTENDANCE_Log.FROM_TIME = fromtime;
                   //DASHBOARD_ATTENDANCE_Log.TO_TIME = totime;
                   //DASHBOARD_ATTENDANCE_Log.PRODUCTION_LINE_CD = linecd;
                   //DASHBOARD_ATTENDANCE_Log.CREATE_TIME = createtime;
                   //DASHBOARD_ATTENDANCE_Log.STATUS = status;
                   //db.Set<DASHBOARD_ATTENDANCE_LOG>().Add(DASHBOARD_ATTENDANCE_Log);
                   //db.SaveChanges();
                   //ts.Complete();

                   string query;
                   query = "";
                   query = query + " exec PROC_DASHBOARD_ATTENDANCE_LOG_INSERT " ;
                   query = query + " @FACTORY_CD='" + factorycd + "',";
                   query = query + " @FROM_TIME='" + fromtime + "',";
                   query = query + " @TO_TIME='" + totime + "',";
                   query = query + " @PRODUCTION_LINE_CD='" + linecd + "',";
                   query = query + " @STATUS='" + status + "'";
      
                   db.Database.ExecuteSqlCommand(query);
                   ts.Complete();
                   return "successfully";
               }
               catch (Exception e)
               {
                   return e.ToString();
               }
               finally
               {
                   ts.Dispose();
               }
           }
       }



       public string INSERT_DASHBOARD_ATTENDANCE_LOG222(string factorycd, DateTime fromtime, DateTime totime, DateTime createtime, string linecd, string status)
       {
           using (TransactionScope ts = new TransactionScope())
           {
               try
               {
                   //DASHBOARD_ATTENDANCE_LOG DASHBOARD_ATTENDANCE_Log = new DASHBOARD_ATTENDANCE_LOG();
                   //DASHBOARD_ATTENDANCE_Log.FACTORY_CD = factorycd;
                   //DASHBOARD_ATTENDANCE_Log.FROM_TIME = fromtime;
                   //DASHBOARD_ATTENDANCE_Log.TO_TIME = totime;
                   //DASHBOARD_ATTENDANCE_Log.PRODUCTION_LINE_CD = linecd;
                   //DASHBOARD_ATTENDANCE_Log.CREATE_TIME = createtime;
                   //DASHBOARD_ATTENDANCE_Log.STATUS = status;
                   //db.Set<DASHBOARD_ATTENDANCE_LOG>().Add(DASHBOARD_ATTENDANCE_Log);
                   //db.SaveChanges();
                   //ts.Complete();

                   string query;
                   query = "";
                   query = query + " exec PROC_DASHBOARD_ATTENDANCE_LOG_INSERT ";
                   query = query + " @FACTORY_CD='" + factorycd + "',";
                   query = query + " @FROM_TIME='" + fromtime + "',";
                   query = query + " @TO_TIME='" + totime + "',";
                   query = query + " @PRODUCTION_LINE_CD='" + linecd + "',";
                   query = query + " @STATUS='" + status + "'";

                   db.Database.ExecuteSqlCommand(query);
                   return "successfully";
               }
               catch (Exception e)
               {
                   return e.ToString();
               }
               finally
               {
                   ts.Dispose();
               }
           }
       }

       //public string GETEMPLOYEEATTENDTIMEINSERT(string FACTORY_CD, String LINE, string SHIFT, DateTime? TRX_DATE)
       //{
       //    try
       //    {
       //        string query;
       //        DateTime date1;
       //        date1 = DateTime.ParseExact(TRX_DATE, "yyyy-MM-dd", null);
       //        query = "";
       //        query = query + " update DASHBOARD_EMPLOYEE_ATTENDTIME ";
       //        query = query + " set SHIFT='" + SHIFT + "',TRX_DATE='" + date1 + "',PRODUCTION_LINE_CD='" + PRODUCTION_LINE_CD + "'";
       //        query = query + " where SEQNO=" + SEQNO;
       //        db.Database.ExecuteSqlCommand(query);
       //        return "successfully";
       //    }
       //    catch (Exception ex)
       //    {
       //        return "not successfully";
       //    }
       //    finally
       //    {
       //    }
       //}


       public TargetOutPut GET_TARGET_OUTPUT(string p1, string LINE_CODE, char p2)
       {
           throw new NotImplementedException();
       }
    }
}
