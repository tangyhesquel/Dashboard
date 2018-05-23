using System;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;

namespace DashBoardWebService
{
    /// <summary>
    /// 摘要说明
    /// </summary>
    [WebService(Namespace = "DashBoardWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DashBoardInsertWebService : System.Web.Services.WebService
    {
        public class DashBoard_Insert_Data  //自定义类，用以存储数据
        {
            public string vv_Factory;
            public string vv_Employee;
            public DateTime vv_CardTime;
         }

        [WebMethod(Description = "Esker Project Interface with DashBoard System:DashBoard_01")]
        public void DashBoard_01(string v_Factory,string v_Employee, DateTime v_CardTime)
        {
            string SQL;
            v_Employee = v_Employee.Trim();
            SQL = "insert into DASHBOARD_EMPLOYEE_ATTENDTIME(FACTORY_CD,EMPLOYEE_NO,ATTEND_DATE,CREATE_TIME) values ('" + v_Factory + "','" + v_Employee + "','" + v_CardTime + "',getdate())";
            SqlHelper.ExecuteNonQuery("TEXT", SQL);
        }

        [WebMethod(Description = "Esker Project Interface with DashBoard System:DashBoard_02")]
        public void DashBoard_02(List<DashBoard_Insert_Data> Insert_Data)
        {
             foreach (DashBoard_Insert_Data insert_data in Insert_Data)
             {
                string v_Factory;
                string v_Employee;
                string SQL;
                DateTime v_CardTime;
                v_Factory = insert_data.vv_Factory;
                v_Employee = insert_data.vv_Employee;
                v_CardTime = insert_data.vv_CardTime;
                v_Employee = v_Employee.Trim();
                SQL = "insert into DASHBOARD_EMPLOYEE_ATTENDTIME(FACTORY_CD,EMPLOYEE_NO,ATTEND_DATE,CREATE_TIME) values ('" + v_Factory + "','" + v_Employee + "','" + v_CardTime + "',getdate())";
                SqlHelper.ExecuteNonQuery("TEXT", SQL);
             }
        }

        //[WebMethod(Description = "Esker Project Interface with PCS System:INT_PCS_02")]
        //public DataTable Get_INT_PCS_02(string v_sEntity, string v_sBusinessUnt, string v_sCostCenter)
        //{
        //    v_sEntity = v_sEntity.Trim();
        //    v_sBusinessUnt = v_sBusinessUnt.Trim();
        //    v_sCostCenter = v_sCostCenter.Trim();
        //    if (v_sEntity == "") { v_sEntity = "%"; } else { v_sEntity = "%" + v_sEntity + "%"; }
        //    if (v_sBusinessUnt == "") { v_sBusinessUnt = "%"; } else { v_sBusinessUnt = "%" + v_sBusinessUnt + "%"; }
        //    if (v_sCostCenter == "") { v_sCostCenter = "%"; } else { v_sCostCenter = "%" + v_sCostCenter + "%"; }

        //    //DataSet ds1 = SqlHelper.ExecuteDataSet("select * from int_pcs_02", parameters);
        //    DataSet ds = SqlHelper.ExecuteSqlReader("select * from int_pcs_02 where entity like '" + v_sEntity + "'  and business_unit like '" + v_sBusinessUnt + "'  and cost_center like '" + v_sCostCenter + "'", "NaturalAccount");
        //    if (ds.Tables.Count > 0)
        //    {
        //        return ds.Tables[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }

}
