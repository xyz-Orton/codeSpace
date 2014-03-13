using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Code.Utilities
{
    public class JsPageResult : ActionResult
    {
        protected static readonly string DEFAULT_RESPONSE_PAGE_TEMPLATE = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n<title>{0}</title>\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n{1}\r\n//]]>\r\n</script>\r\n</head><body></body></html>";

        public JsPageResult(string script)
            : this(script, null)
        {
        }

        public JsPageResult(string script, string pageTitle)
        {
            this.Script = script;
            this.PageTitle = pageTitle;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/html";
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            response.Write(this.GetHtmlPageCode(context));
        }

        protected virtual string GetHtmlPageCode(ControllerContext context)
        {
            return string.Format(DEFAULT_RESPONSE_PAGE_TEMPLATE, this.PageTitle, this.Script);
        }

        public virtual Encoding ContentEncoding { get; set; }

        public virtual string PageTitle { get; set; }

        public virtual string Script { get; set; }
    }
}
