using MvcDashBoard.BLL.DASHBOARD;
using MvcDashBoard.Model.DASHBOARD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDashBoard.WebUI.Areas.DASHBOARD.Controllers
{
    public class DASHBOARDLINESHIFTSETTINGController : Controller
    {
        // GET: DASHBOARD/DASHBOARDLINESHIFTSETTING
        public ActionResult Index()
        {
              return View();
        }

        public ActionResult DASHBOARDLINESHIFTSETTINGView()
        {
            ViewData["FACTORY_CD"] = Request["FACTORY_CD"];

            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(Request["FACTORY_CD"]);
            ViewData["FACTORY_CD_L"] = DashboardBLL.Get_FactoryCD_List("%");
            ViewData["SHIFT_CODE_L"] = DashboardBLL.Get_SHIFT_CODE_List("%");
            ViewData["SHIFT_CODE"] = Request["SHIFT_CODE"];

            string preferredLang = Request["LANGUAGE"];
            //preferredLang = "CN";
            IEnumerable<OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> data = DashboardBLL.PROC_GET_DASHBOARD_FORM_DEFINITION("DASHBOARDLINESHIFT", preferredLang);
            foreach (OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION item in data)
            {
                ViewData[item.CONTROLLER_ID.ToString()] = (item.CONTROLLER_DESC == null ? "" : item.CONTROLLER_DESC.ToString());
            }


            return View();
        }

        public ActionResult InquirySHIFTData(string FACTORY_CD,string SHIFT_CODE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.InquirySHIFTData(FACTORY_CD, SHIFT_CODE));
        }

        public ActionResult UpdateShiftData(int SEQNO, string FACTORY_CD, string SHIFT_CODE, string SHIFT_DESC, int SHIFT_SEQ, string SHIFT_FROM, string SHIFT_TO, decimal TIME_INTERVAL, string ACTIVE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.UpdateShiftData(SEQNO,FACTORY_CD, SHIFT_CODE, SHIFT_DESC, SHIFT_SEQ, SHIFT_FROM, SHIFT_TO,TIME_INTERVAL, ACTIVE));
        }

        public ActionResult DeleteSHIFTData(string FACTORY_CD, string SHIFT_CODE, string SEQNO)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.DeleteSHIFTData(FACTORY_CD, SHIFT_CODE, SEQNO));
        }


    }
}