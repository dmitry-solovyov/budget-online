using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BudgetOnline.Web.Infrastructure.Security;

namespace BudgetOnline.Web.Infrastructure.Modules
{
    public class CookieLocalizationModule : IHttpModule
    {
        public IMembershipHelper MembershipHelper { get; set; }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            InitMembershipHelper();

            context.BeginRequest += ContextBeginRequest;
        }

        private void ContextBeginRequest(object sender, EventArgs e)
        {
            InitMembershipHelper();

            var cookie = HttpContext.Current.Request.Cookies["lang"];
            var culture = MembershipHelper.GetCulture();

            if (cookie != null && culture.Name != cookie.Value)
            {
                culture = new CultureInfo(cookie.Value);
                MembershipHelper.SetCulture(culture);
            }

            if (culture != null)
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        private void InitMembershipHelper()
        {
            if (MembershipHelper == null)
                MembershipHelper = DependencyResolver.Current.GetService<IMembershipHelper>();
        }
    }
}