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

namespace PcsWebService
{
    /// <summary>
    /// 摘要说明
    /// </summary>
    [WebService(Namespace = "PcsWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GETPCSDATAWebService : System.Web.Services.WebService
    {
        public class PCS_NaturalAccount  //自定义类，用以存储数据
        {
            public string vv_sEntity;
            public string vv_sBusinessUnt;
            public string vv_sCostCenter;
        }

        [WebMethod(Description = "Esker Project Interface with PCS System:INT_PCS_01")]
        public DataTable Get_INT_PCS_01(string v_sEntity, string v_sBusinessUnt, string v_sCostCenter)
        {
            v_sEntity = v_sEntity.Trim();
            v_sBusinessUnt = v_sBusinessUnt.Trim();
            v_sCostCenter = v_sCostCenter.Trim();
            if (v_sEntity == "") { v_sEntity = "%"; } else { v_sEntity = "%" + v_sEntity + "%"; }
            if (v_sBusinessUnt == "") { v_sBusinessUnt = "%"; } else { v_sBusinessUnt = "%" + v_sBusinessUnt + "%"; }
            if (v_sCostCenter == "") { v_sCostCenter = "%"; } else { v_sCostCenter = "%" + v_sCostCenter + "%"; }

            //DataSet ds1 = SqlHelper.ExecuteDataSet("select * from int_pcs_02", parameters);
            DataSet ds = SqlHelper.ExecuteSqlReader("select * from int_pcs_01 where entity_id like '" + v_sEntity + "'  and business_unit_id like '" + v_sBusinessUnt + "'  and cost_center_id like '" + v_sCostCenter + "'", "Department");
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        [WebMethod(Description = "Esker Project Interface with PCS System:INT_PCS_02")]
        public DataTable Get_INT_PCS_02(string v_sEntity,string v_sBusinessUnt,string v_sCostCenter)
        {
            //SqlParameter[] parameters =
            //    {
            //        new SqlParameter("@v_sEntity", SqlDbType.VarChar),
            //        new SqlParameter("@v_sBusinessUnt", SqlDbType.VarChar),
            //        new SqlParameter("@v_sCostCenter", SqlDbType.VarChar)
            //    };
            //    parameters[0].Value = v_sEntity;
            //    parameters[1].Value = v_sBusinessUnt;
            //    parameters[2].Value = v_sCostCenter;

                v_sEntity = v_sEntity.Trim();
                v_sBusinessUnt = v_sBusinessUnt.Trim();
                v_sCostCenter = v_sCostCenter.Trim();
                if (v_sEntity=="") {v_sEntity="%";}else{v_sEntity="%"+v_sEntity+"%";}
                if (v_sBusinessUnt=="") {v_sBusinessUnt="%";}else{v_sBusinessUnt="%"+v_sBusinessUnt+"%";}
                if (v_sCostCenter=="") {v_sCostCenter="%";}else{v_sCostCenter="%"+v_sCostCenter+"%";}

                //DataSet ds1 = SqlHelper.ExecuteDataSet("select * from int_pcs_02", parameters);
                DataSet ds = SqlHelper.ExecuteSqlReader("select * from int_pcs_02 where entity like '" + v_sEntity + "'  and business_unit like '" + v_sBusinessUnt + "'  and cost_center like '" + v_sCostCenter + "'","NaturalAccount");
                if (ds.Tables.Count > 0)
                {
                        return ds.Tables[0];
                }
                    else
                {
                        return null;
                 }
           }
    }

 }
