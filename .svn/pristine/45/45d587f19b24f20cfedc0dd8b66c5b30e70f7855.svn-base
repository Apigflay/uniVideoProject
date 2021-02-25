using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;

namespace ThirdAPI
{
    public class CultureModule : IHttpModule
    {
        private CultureInfo currentCulture;
        private CultureInfo currentUICulture;

        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += SetCurrentCulture;

            context.EndRequest += RecoverCulture;
        }

        private void SetCurrentCulture(object sender, EventArgs args)
        {

            currentCulture = Thread.CurrentThread.CurrentCulture;
            currentUICulture = Thread.CurrentThread.CurrentUICulture;

            HttpContextBase contextWrapper = new HttpContextWrapper(HttpContext.Current);

            RouteData routeData = RouteTable.Routes.GetRouteData(contextWrapper);

            if (routeData == null)
            {
                return;
            }

            object culture;
            try
            {
                if (routeData.Values.TryGetValue("lang", out culture))
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(culture.ToString());
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture.ToString());
                }
                else
                {
                    string lang = HttpContext.Current.Request.Headers["Accept-Language"];

                    Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                }
            }
            catch
            { }
        }

        private void RecoverCulture(object sender, EventArgs args)
        {
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
        }
    }
}