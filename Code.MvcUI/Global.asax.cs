using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Code.MvcUI.Models;

namespace Code.MvcUI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            this.AcquireRequestState += new EventHandler(MvcApplication_AcquireRequestState);
        }

        public void MvcApplication_AcquireRequestState(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            if (application.Context.Handler is MvcHandler)
            {
                string strLoginUrl = application.Context.Request.Url.ToString();

                //检测是否已登录
                UserData user = UserData.InitData();
                if (!strLoginUrl.Contains("/Account/LogOn") && user == null)
                {
                    application.Context.Response.Write("<script language='javascript'>top.location.href='/Account/LogOn'</script>");
                    application.Context.Response.End();
                }
            }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}