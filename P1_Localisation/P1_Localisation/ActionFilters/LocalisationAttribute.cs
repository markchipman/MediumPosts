using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace P1_Localisation.ActionFilters {
    public class LocalisationAttribute : ActionFilterAttribute {
        private const string LangParam = "lang";
        private const string CookieName = "roomer.CurrentUICulture";

        // List of allowed languages in this app (to speed up check)
        private const string Cultures = "en-us mk-mk";

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            // Try getting culture from URL first
            var culture = (string)filterContext.RouteData.Values[LangParam];

            // If not provided, or the culture does not match the list of known cultures, try cookie or browser setting
            if (string.IsNullOrEmpty(culture) || !Cultures.Contains(culture.ToLower())) {
                // load the culture info from the cookie
                var cookie = filterContext.HttpContext.Request.Cookies[CookieName];
                if (cookie != null) {
                    // set the culture by the cookie content
                    culture = cookie.Value;
                } else {
                    // set the culture by the location if not specified
                    culture = filterContext.HttpContext.Request.UserLanguages[0];
                }
                // set the lang value into route data
                filterContext.RouteData.Values[LangParam] = culture;
            }

            // Keep the part up to the "-" as the primary language
            filterContext.RouteData.Values[LangParam] = culture;

            // Set the language - ignore specific culture for now
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);

            // save the locale into cookie (full locale)
            var cookie2 = new HttpCookie(CookieName, culture) { Expires = DateTime.Now.AddYears(1) };
            filterContext.HttpContext.Response.SetCookie(cookie2);

            // Pass on to normal controller processing
            base.OnActionExecuting(filterContext);
        }
    }
}