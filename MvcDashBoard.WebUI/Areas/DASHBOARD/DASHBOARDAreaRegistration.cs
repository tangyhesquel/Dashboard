using System.Web.Mvc;

namespace MvcDashBoard.WebUI.Areas.DASHBOARD
{
    public class DASHBOARDAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DASHBOARD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DASHBOARD_default",
                "DASHBOARD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}