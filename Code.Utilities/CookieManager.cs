using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPTP.Utilities
{
    public class CookieManager
    {
        #region Public Is cookie exist
        /// <summary>
        /// Is cookie exist
        /// </summary>
        /// <param name="CookieName">Cookie name</param>
        /// <returns>Is exist</returns>
        public static bool IsExist(string CookieName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (CurrentContext.Request.Cookies[CookieName] == null)
            {
                //If cookie is null
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Public Add new cookie
        /// <summary>
        ///  Add new cookie
        /// </summary>
        /// <param name="CookieName">Cookie name</param>
        /// <param name="CookieObject">Cookie content</param>
        public static void SetObject(string CookieName, string CookieContent)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (CookieContent != "" || CookieContent != null)
            {
                CurrentContext.Response.Cookies.Add(new System.Web.HttpCookie(CookieName, CookieContent));
            }
        }
        #endregion

        #region Public Add new cookie with Expire
        /// <summary>
        ///  Add new cookie
        /// </summary>
        /// <param name="CookieName">Cookie name</param>
        /// <param name="CookieObject">Cookie content</param>
        public static void SetObject(string CookieName, string CookieContent, int ExpireDays)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (CookieContent != "" || CookieContent != null)
            {
                CurrentContext.Response.Cookies.Add(new System.Web.HttpCookie(CookieName, CookieContent));
                CurrentContext.Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(ExpireDays);
            }
        }
        #endregion

        #region Public Get cookie object
        /// <summary>
        /// Get cookie object
        /// </summary>
        /// <param name="CookieName">Cookie name</param>
        public static object GetObject(string CookieName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (IsExist(CookieName) == true)
            {
                return CurrentContext.Request.Cookies[CookieName].Value;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Public Delete cookie
        /// <summary>
        /// Delete cookie
        /// </summary>
        /// <param name="CookieName">Cookie name</param>
        public static void DeleteObject(string CookieName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (IsExist(CookieName) == true)
            {
                CurrentContext.Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region Public Clear Cookie
        /// <summary>
        /// Clear Cookie
        /// </summary>
        public static void ClearObjects()
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            CurrentContext.Response.Cookies.Clear();
        }
        #endregion
    }
}
