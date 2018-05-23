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
    public class BASICINFORMATIONDATAController : Controller
    {

        static string FACTORY_CD_Param;
        static string LINE_CODE_Param;

        //private MESDEVEntities db1 = new MESDEVEntities();
        //private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult BasicDataSave(string FACTORY_CD, string GARMENT_TYPE, string REFRESH_INTERVAL, string LINE_CODE1, string SHIFT_CODE1, string LINE_CODE2, string SHIFT_CODE2, string LINE_CODE3, string SHIFT_CODE3, string SHOWED_DATE3, string LANGUAGE, string HR_MAX_TIME_DIFFERENCE, string HR_REFRESH_INTERVAL, int TARGET_TOTAL_QTY, decimal TARGET_WORK_HOUR, string DISPLAY_TARGET2_DEFECT, string USE_LINE_TARGET2)
        {
            COMMONPROCESS COMMONPROCESS = new COMMONPROCESS(FACTORY_CD_Param, LINE_CODE_Param);
            string s;
            s = COMMONPROCESS.Write_BASICINFORMATION_File(FACTORY_CD, GARMENT_TYPE, REFRESH_INTERVAL, LINE_CODE1, SHIFT_CODE1, LINE_CODE2, SHIFT_CODE2, LINE_CODE3, SHIFT_CODE3, SHOWED_DATE3, LANGUAGE, HR_MAX_TIME_DIFFERENCE, HR_REFRESH_INTERVAL, TARGET_TOTAL_QTY, TARGET_WORK_HOUR, DISPLAY_TARGET2_DEFECT, USE_LINE_TARGET2);
            //Response.Redirect("~/DASHBOARD/DASHBOARDSHOWDATA/DASHBOARDSHOWView");
            return Content(s);
        }
        public ActionResult BASICINFORMATIONView(string FACTORY_CD, string LINE_CODE)
        {
            FACTORY_CD_Param = FACTORY_CD;
            LINE_CODE_Param = LINE_CODE;

            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            string preferredLang;
        
            ViewData["FACTORY_CD"] = DashboardBLL.Get_FactoryCD_List("%");
            ViewData["GARMENT_TYPE"] = DashboardBLL.Get_GARMENT_TYPE_List();
            ViewData["LINE_CD"] = DashboardBLL.Get_LINECD_List("%", "%");
            ViewData["SHIFT_CODE"] = DashboardBLL.Get_SHIFT_CODE_List("%");
            COMMONPROCESS ReadAndWrite_File = new COMMONPROCESS(FACTORY_CD_Param, LINE_CODE_Param);
            BASICINFORMATION_DATA BASICINFORMATION_DATA = ReadAndWrite_File.Read_BASICINFORMATION_File();
            preferredLang = BASICINFORMATION_DATA.LANGUAGE;                
            //if (string.IsNullOrEmpty(preferredLang) || string.IsNullOrWhiteSpace(preferredLang))
            //Request.("LANGUAGE");

            IEnumerable<MvcDashBoard.Model.DASHBOARD.Models.OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> data = DashboardBLL.PROC_GET_DASHBOARD_FORM_DEFINITION("BASICINFORMATION", preferredLang);
            foreach (MvcDashBoard.Model.DASHBOARD.Models.OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION item in data)
            {
                ViewData[item.CONTROLLER_ID.ToString()] = (item.CONTROLLER_DESC == null ? "" : item.CONTROLLER_DESC.ToString());
            }
            return View(BASICINFORMATION_DATA);
        }

        //public ActionResult BASICDATAView()
        //{
        //    DASHBOARDBLL DashboardBLL = new DASHBOARDBLL();
        //    string preferredLang;

        //    ViewData["FACTORY_CD"] = DashboardBLL.Get_FactoryCD_List("%");
        //    ViewData["GARMENT_TYPE"] = DashboardBLL.Get_GARMENT_TYPE_List();
        //    ViewData["LINE_CD"] = DashboardBLL.Get_LINECD_List("%", "%");
        //    ViewData["SHIFT_CODE"] = DashboardBLL.Get_SHIFT_CODE_List("%");
        //    COMMONPROCESS ReadAndWrite_File = new COMMONPROCESS(FACTORY_CD_Param,LINE_CODE_Param);
        //    BASICINFORMATION_DATA BASICINFORMATION_DATA = ReadAndWrite_File.Read_BASICINFORMATION_File();
        //    preferredLang = BASICINFORMATION_DATA.LANGUAGE;
        //    //if (string.IsNullOrEmpty(preferredLang) || string.IsNullOrWhiteSpace(preferredLang))
        //    //Request.("LANGUAGE");

        //    IEnumerable<MvcDashBoard.Model.DASHBOARD.Models.OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> data = DashboardBLL.PROC_GET_DASHBOARD_FORM_DEFINITION("BASICINFORMATION", preferredLang);
        //    foreach (MvcDashBoard.Model.DASHBOARD.Models.OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION item in data)
        //    {
        //        ViewData[item.CONTROLLER_ID.ToString()] = (item.CONTROLLER_DESC == null ? "" : item.CONTROLLER_DESC.ToString());
        //    }
        //    return View(BASICINFORMATION_DATA);
        //}













        // GET: DASHBOARD/BASICINFORMATION_DATA
        //public ActionResult Index()
        //{
        //    return View(db.BASICINFORMATION_DATA.ToList());
        //}


        //// GET: DASHBOARD/BASICINFORMATION_DATA/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BASICINFORMATION_DATA bASICINFORMATION_DATA = db.BASICINFORMATION_DATA.Find(id);
        //    if (bASICINFORMATION_DATA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bASICINFORMATION_DATA);
        //}

        //// GET: DASHBOARD/BASICINFORMATION_DATA/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DASHBOARD/BASICINFORMATION_DATA/Create
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FACTORY_CD,GARMENT_TYPE,REFRESH_TIME,LINE_CODE1,SHIFT_CODE1,LINE_CODE2,SHIFT_CODE2")] BASICINFORMATION_DATA bASICINFORMATION_DATA)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BASICINFORMATION_DATA.Add(bASICINFORMATION_DATA);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(bASICINFORMATION_DATA);
        //}

        //// GET: DASHBOARD/BASICINFORMATION_DATA/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BASICINFORMATION_DATA bASICINFORMATION_DATA = db.BASICINFORMATION_DATA.Find(id);
        //    if (bASICINFORMATION_DATA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bASICINFORMATION_DATA);
        //}

        //// POST: DASHBOARD/BASICINFORMATION_DATA/Edit/5
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FACTORY_CD,GARMENT_TYPE,REFRESH_TIME,LINE_CODE1,SHIFT_CODE1,LINE_CODE2,SHIFT_CODE2")] BASICINFORMATION_DATA bASICINFORMATION_DATA)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bASICINFORMATION_DATA).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(bASICINFORMATION_DATA);
        //}

        //// GET: DASHBOARD/BASICINFORMATION_DATA/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BASICINFORMATION_DATA bASICINFORMATION_DATA = db.BASICINFORMATION_DATA.Find(id);
        //    if (bASICINFORMATION_DATA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bASICINFORMATION_DATA);
        //}

        //// POST: DASHBOARD/BASICINFORMATION_DATA/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    BASICINFORMATION_DATA bASICINFORMATION_DATA = db.BASICINFORMATION_DATA.Find(id);
        //    db.BASICINFORMATION_DATA.Remove(bASICINFORMATION_DATA);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
