using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

//namespace MvcDashBoard.WebUI.Models
 namespace MvcDashBoard.Model.DASHBOARD.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string DBStr)
            : base(DBStr, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("GEG_DBCONN");
        }

        //public System.Data.Entity.DbSet<MvcDashBoard.WebUI.Areas.DASHBOARD.Models.FN_DASHBOARD_SHOW_DATA_Result> FN_DASHBOARD_SHOW_DATA_Result { get; set; }
        public System.Data.Entity.DbSet<MvcDashBoard.Model.DASHBOARD.Models.FN_DASHBOARD_SHOW_DATA_Result> FN_DASHBOARD_SHOW_DATA_Result { get; set; }

        public System.Data.Entity.DbSet<MvcDashBoard.Model.DASHBOARD.Models.DASHBOARD_EMPLOYEE_ATTENDTIME> DASHBOARD_EMPLOYEE_ATTENDTIME { get; set; }

        public System.Data.Entity.DbSet<MvcDashBoard.Model.DASHBOARD.Models.BASICINFORMATION_DATA> BASICINFORMATION_DATA { get; set; }

    }
}