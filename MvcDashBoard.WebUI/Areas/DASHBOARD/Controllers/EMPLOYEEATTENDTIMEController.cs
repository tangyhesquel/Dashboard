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

namespace MvcDashBoard.WebUI.Areas.DASHBOARD.Controllers
{
    public class EMPLOYEEATTENDTIMEController : Controller
    {
       // private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult InquiryAttendTime(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            return Content(DashboardBLL.Get_DASHBOARD_EMPLOYEE_ATTENDTIME(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE));
        }

        public ActionResult EMPLOYEEATTENDTIMEView(string FACTORY_CD, String LINE_CODE, string SHIFT_CODE, DateTime? TRX_DATE)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);

            ViewData["FACTORY_CD"] = DashboardBLL.Get_FactoryCD_List("%");
            ViewData["GARMENT_TYPE"] = DashboardBLL.Get_GARMENT_TYPE_List();
            ViewData["LINE_CD"] = DashboardBLL.Get_LINECD_List("%", "%");
            ViewData["SHIFT_CODE"] = DashboardBLL.Get_SHIFT_CODE_List("%");

            EMPLOYEE_ATTENDTIME EMPLOYEE_ATTENDTIME = new EMPLOYEE_ATTENDTIME();

            List<DASHBOARD_EMPLOYEE_ATTENDTIME> DASHBOARD_EMPLOYEE_ATTENDTIME = DashboardBLL.Get_DASHBOARD_EMPLOYEE_ATTENDTIME_List(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE);

            EMPLOYEE_ATTENDTIME_QUERY_CRITERIA EMPLOYEE_ATTENDTIME_QUERY_CRITERIA = new EMPLOYEE_ATTENDTIME_QUERY_CRITERIA();
            EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.FACTORY_CD = FACTORY_CD;
            EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.PRODUCTION_LINE_CD = LINE_CODE;
            EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.SHIFT = SHIFT_CODE;
            EMPLOYEE_ATTENDTIME_QUERY_CRITERIA.TRX_DATE = TRX_DATE;

            EMPLOYEE_ATTENDTIME.EMPLOYEE_ATTENDTIME_list = DASHBOARD_EMPLOYEE_ATTENDTIME;
            EMPLOYEE_ATTENDTIME.EMPLOYEE_ATTENDTIME_Query_Criteria = EMPLOYEE_ATTENDTIME_QUERY_CRITERIA;

            string preferredLang =Request["LANGUAGE"];
            IEnumerable<OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION> data = DashboardBLL.PROC_GET_DASHBOARD_FORM_DEFINITION("EMPLOYEEATTENDTIME", preferredLang);
            foreach (OTHER.PROC_GET_DASHBOARD_FORM_DEFINITION item in data)
            {
                ViewData[item.CONTROLLER_ID.ToString()] = (item.CONTROLLER_DESC == null ? "" : item.CONTROLLER_DESC.ToString());
            }

            return View(EMPLOYEE_ATTENDTIME);
        }

        public ActionResult UPDATE_ATTENDTIME(string FACTORY_CD,string SEQNO, String SHIFT, string TRX_DATE, string PRODUCTION_LINE_CD)
        {
            DASHBOARDBLL DashboardBLL = new DASHBOARDBLL(FACTORY_CD);
            string result;
            result= DashboardBLL.UPDATE_ATTENDTIME(SEQNO, SHIFT, TRX_DATE, PRODUCTION_LINE_CD);
            return Content(result);
        }

        

        //public ActionResult UPDATEATTENDTIME(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME = db.DASHBOARD_EMPLOYEE_ATTENDTIME.Find(id);
        //    if (dASHBOARD_EMPLOYEE_ATTENDTIME == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        // POST: DASHBOARD/EMPLOYEE_ATTENDTIME/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UPDATEATTENDTIME([Bind(Include = "SEQNO,FACTORY_CD,EMPLOYEE_NO,ATTEND_DATE,CREATE_TIME,SHIFT,TRX_DATE,PRODUCTION_LINE_CD")] DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dASHBOARD_EMPLOYEE_ATTENDTIME).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        // GET: DASHBOARD/EMPLOYEE_ATTENDTIME
        //public ActionResult Index()
        //{
        //    return View(db.DASHBOARD_EMPLOYEE_ATTENDTIME.ToList());
        //}

        //// GET: DASHBOARD/EMPLOYEE_ATTENDTIME/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME = db.DASHBOARD_EMPLOYEE_ATTENDTIME.Find(id);
        //    if (dASHBOARD_EMPLOYEE_ATTENDTIME == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        //// GET: DASHBOARD/EMPLOYEE_ATTENDTIME/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DASHBOARD/EMPLOYEE_ATTENDTIME/Create
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SEQNO,FACTORY_CD,EMPLOYEE_NO,ATTEND_DATE,CREATE_TIME,SHIFT,TRX_DATE,PRODUCTION_LINE_CD")] DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DASHBOARD_EMPLOYEE_ATTENDTIME.Add(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        

        //// GET: DASHBOARD/EMPLOYEE_ATTENDTIME/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME = db.DASHBOARD_EMPLOYEE_ATTENDTIME.Find(id);
        //    if (dASHBOARD_EMPLOYEE_ATTENDTIME == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        //// POST: DASHBOARD/EMPLOYEE_ATTENDTIME/Edit/5
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SEQNO,FACTORY_CD,EMPLOYEE_NO,ATTEND_DATE,CREATE_TIME,SHIFT,TRX_DATE,PRODUCTION_LINE_CD")] DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dASHBOARD_EMPLOYEE_ATTENDTIME).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        //// GET: DASHBOARD/EMPLOYEE_ATTENDTIME/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME = db.DASHBOARD_EMPLOYEE_ATTENDTIME.Find(id);
        //    if (dASHBOARD_EMPLOYEE_ATTENDTIME == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dASHBOARD_EMPLOYEE_ATTENDTIME);
        //}

        //// POST: DASHBOARD/EMPLOYEE_ATTENDTIME/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DASHBOARD_EMPLOYEE_ATTENDTIME dASHBOARD_EMPLOYEE_ATTENDTIME = db.DASHBOARD_EMPLOYEE_ATTENDTIME.Find(id);
        //    db.DASHBOARD_EMPLOYEE_ATTENDTIME.Remove(dASHBOARD_EMPLOYEE_ATTENDTIME);
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
