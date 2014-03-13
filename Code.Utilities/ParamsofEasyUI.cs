using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Code.Utilities
{
    public class ParamsofEasyUI
    {
        public ParamsofEasyUI() { }
        public static string RequstForm(string name)
        {
            return (HttpContext.Current.Request.Form[name] == null ? string.Empty : HttpContext.Current.Request.Form[name].ToString().Trim());
        }
        public static string RequstString(string sParam)
        {
            return (HttpContext.Current.Request[sParam] == null ? string.Empty : HttpContext.Current.Request[sParam].ToString().Trim());
        }
        public static int RequstInt(string sParam)
        {
            int iValue;
            string sValue = RequstString(sParam);
            int.TryParse(sValue, out iValue);
            return iValue;
        }
        public static string order
        {
            get { return RequstString("order"); }
        }
        public static string sort
        {
            get { return RequstString("sort"); }
        }
        public static int page
        {
            get { return RequstInt("page"); }
        }
        public static int rows
        {
            get { return RequstInt("rows"); }
        }
    }
}
