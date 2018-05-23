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
    public class GETPCSDATAWebService1 : System.Web.Services.WebService
    {
        public class PCS_NaturalAccount1  //自定义类，用以存储数据
        {
            public string vv_sEntity;
            public string vv_sBusinessUnt;
            public string vv_sCostCenter;
        }

        [WebMethod(Description = "Esker Project Interface with PCS System:INT_PCS")]
        public DataSet Get_INT_PCS(string v_sEntity, string v_sBusinessUnt, string v_sCostCenter)
        {
            //string AdServer;
            //string errorMsg;

            //////////////string yesorno;
            //////////////AdServer = "LDAP://gfg1.esquel.com:3268/DC=esquel,DC=com";
            ////////////////AdServer = "LDAP://192.168.4.119:3268/DC=esquel,DC=com";
            ////////////////AdServer = "LDAP://192.168.152.2:3268/DC=esquel,DC=com";
            ////////////////AdServer = "LDAP://192.168.155.13:3268/DC=esquel,DC=com";
            ////////////////AdServer = "LDAP://10.253.1.61:3268/DC=esquel,DC=com";
            //////////////Esquel.Util.AD.ADHelper adhelper = new Esquel.Util.AD.ADHelper(AdServer);
            //////////////bool isflag = adhelper.ValidateUser("tangyh", "tht0923!", out errorMsg);
            //////////////if (!isflag)
            //////////////{
            //////////////    yesorno = "yes";
            //////////////}
            //////////////else
            //////////////{
            //////////////    yesorno = "no";
            //////////////}

            //////////////AdServer = "LDAP://192.168.4.119:3268/DC=esquel,DC=com";
            //////////////adhelper =  new Esquel.Util.AD.ADHelper(AdServer);
            //////////////isflag = adhelper.ValidateUser("tangyh", "tht0923!", out errorMsg);
            //////////////if (!isflag)
            //////////////{
            //////////////    yesorno = "yes";
            //////////////}
            //////////////else
            //////////////{
            //////////////    yesorno = "no";
            //////////////}

            //////////////AdServer = "LDAP://192.168.152.2:3268/DC=esquel,DC=com";
            //////////////adhelper = new Esquel.Util.AD.ADHelper(AdServer);
            //////////////isflag = adhelper.ValidateUser("tangyh", "tht0923!", out errorMsg);
            //////////////if (!isflag)
            //////////////{
            //////////////    yesorno = "yes";
            //////////////}
            //////////////else
            //////////////{
            //////////////    yesorno = "no";
            //////////////}

            //////////////AdServer = "LDAP://192.168.155.13:3268/DC=esquel,DC=com";
            //////////////adhelper = new Esquel.Util.AD.ADHelper(AdServer);
            //////////////isflag = adhelper.ValidateUser("tangyh", "tht0923!", out errorMsg);
            //////////////if (!isflag)
            //////////////{
            //////////////    yesorno = "yes";
            //////////////}
            //////////////else
            //////////////{
            //////////////    yesorno = "no";
            //////////////}

            //////////////AdServer = "LDAP://10.253.1.61:3268/DC=esquel,DC=com";
            //////////////adhelper = new Esquel.Util.AD.ADHelper(AdServer);
            //////////////isflag = adhelper.ValidateUser("tangyh", "tht0923!", out errorMsg);
            //////////////if (!isflag)
            //////////////{
            //////////////    yesorno = "yes";
            //////////////}
            //////////////else
            //////////////{
            //////////////    yesorno = "no";
            //////////////}

            v_sEntity = v_sEntity.Trim();
            v_sBusinessUnt = v_sBusinessUnt.Trim();
            v_sCostCenter = v_sCostCenter.Trim();
            if (v_sEntity == "") { v_sEntity = "%"; } else { v_sEntity = "%" + v_sEntity + "%"; }
            if (v_sBusinessUnt == "") { v_sBusinessUnt = "%"; } else { v_sBusinessUnt = "%" + v_sBusinessUnt + "%"; }
            if (v_sCostCenter == "") { v_sCostCenter = "%"; } else { v_sCostCenter = "%" + v_sCostCenter + "%"; }

            //DataSet ds1 = SqlHelper.ExecuteDataSet("select * from int_pcs_02", parameters);
            DataSet ds = new DataSet();
            SqlHelper.ExecuteSqlReader(ds,"select * from int_pcs_01 where entity_id like '" + v_sEntity + "'  and business_unit_id like '" + v_sBusinessUnt + "'  and cost_center_id like '" + v_sCostCenter + "'", "Department");
            SqlHelper.ExecuteSqlReader(ds,"select * from int_pcs_02 where entity like '" + v_sEntity + "'  and business_unit like '" + v_sBusinessUnt + "'  and cost_center like '" + v_sCostCenter + "'", "NaturalAccount");
            return ds;
        }
    }

}
