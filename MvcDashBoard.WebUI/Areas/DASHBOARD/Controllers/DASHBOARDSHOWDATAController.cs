using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcDashBoard.Model.DASHBOARD.Models;
using MvcDashBoard.BLL.DASHBOARD;
using MvcDashBoard.Common;

namespace MvcDashBoard.WebUI.Areas.DASHBOARD.Controllers
{
    public class DASHBOARDSHOWDATAController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DASHBOARD/FN_DASHBOARD_SHOW_DATA_Result

        public ActionResult TESTView()
        {
            return View();
        }

        public ActionResult TEST2View()
        {
            return View();
        }

        public ActionResult TEST3View()
        {
            return View();
        }

        public ActionResult DASHBOARDSHOWView()
        //public ActionResult DASHBOARDSHOWView(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE, string REFRESH)
        {
            ViewData["FACTORY_CD_Param"] = Request["factory"];
            ViewData["LINE_CODE_Param"] = Request["line"];
            string FACTORY_CD = Request["factory"];
            string LINE_CODE = Request["line"];
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(Request["factory"]);
            DASHBOARD_SHOW_DATA DASHBOARD_SHOW_data = new DASHBOARD_SHOW_DATA();
            DASHBOARD_SHOW_data = DashboardBLL.Get_DashBoard_Data_List(FACTORY_CD, LINE_CODE,null, null, null); //(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE, REFRESH);
            if (DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION == null)
            {
                return View();
            }
            string preferredLang = DASHBOARD_SHOW_data.RUNNING_BASIC_INFORMATION.LANGUAGE;
            IEnumerable<OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> data = DashboardBLL.PROC_GET_DASHBOARD_FORM_DEFINITION("DASHBOARDSHOW", preferredLang);
            foreach (MvcDashBoard.Model.DASHBOARD.Models.OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION item in data)
            {
                ViewData[item.CONTROLLER_ID.ToString()] = (item.CONTROLLER_DESC == null ? "" : item.CONTROLLER_DESC.ToString());
            }
            return View(DASHBOARD_SHOW_data);
        }

        public ActionResult DASHBOARDSHOWInquiry(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.DASHBOARDSHOWInquiry(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE));
        }

        public ActionResult PROC_DASHBOARD_GET_PRODUCTION_QTY(string FACTORY_CD, String LINE_CODE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.PROC_DASHBOARD_GET_PRODUCTION_QTY(FACTORY_CD, LINE_CODE));
        }

        public ActionResult PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE, string ONLY1LINE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(FACTORY_CD, LINE_CODE,SHIFT_CODE,TRX_DATE,ONLY1LINE));
        }

        public ActionResult GETEMPLOYEEATTENDTIMEINSERT(string FACTORY_CD, String LINE_CODE, string HR_MAX_TIME_DIFFERENCE, string HR_REFRESH_INTERVAL)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            string result = DashboardBLL.GETEMPLOYEEATTENDTIMEINSERT(FACTORY_CD, LINE_CODE, HR_MAX_TIME_DIFFERENCE, HR_REFRESH_INTERVAL);
            return Content(result);
        }
    }
}
