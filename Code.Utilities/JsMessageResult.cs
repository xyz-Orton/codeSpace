using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace CPTP.Utilities
{
    public class JsMessageResult : JsPageResult
    {
        public JsMessageResult()
            : this(null, null, null)
        {
        }

        public JsMessageResult(string message)
            : this(message, null, null)
        {
        }

        public JsMessageResult(string message, string redirectToUrl)
            : this(message, redirectToUrl, null)
        {
        }

        public JsMessageResult(string message, string redirectToUrl, string pageTitle)
            : base(string.Empty, pageTitle)
        {
            this.Message = message;
            this.RedirectToUrl = redirectToUrl;
        }

        protected override string GetHtmlPageCode(ControllerContext context)
        {
            string str = string.Format("alert(\"{0}\");", HttpUtility.JavaScriptStringEncode(this.Message));
            string redirectToUrl = this.RedirectToUrl;
            if (!string.IsNullOrEmpty(redirectToUrl))
            {
                if (this.EnableUrlDecode)
                {
                    redirectToUrl = HttpUtility.UrlDecode(redirectToUrl);
                }
                if ((this.EnableInspactIsLocalUrl && !context.RequestContext.HttpContext.Request.IsUrlLocalToHost(redirectToUrl)) && !string.IsNullOrEmpty(this.RedirectToUrlOnToUrlIsNotLocalUrl))
                {
                    redirectToUrl = this.RedirectToUrlOnToUrlIsNotLocalUrl;
                }
                str = str + string.Format("window.location.href = \"{0}\";", HttpUtility.JavaScriptStringEncode(redirectToUrl));
            }
            return string.Format(JsPageResult.DEFAULT_RESPONSE_PAGE_TEMPLATE, this.PageTitle, str ?? string.Empty);
        }

        public bool EnableInspactIsLocalUrl { get; set; }

        public bool EnableUrlDecode { get; set; }

        public string Message { get; set; }

        public string RedirectToUrl { get; set; }

        public string RedirectToUrlOnToUrlIsNotLocalUrl { get; set; }
    }
}
