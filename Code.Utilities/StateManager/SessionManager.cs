using System;
using System.Collections.Generic;
using System.Text;

namespace Code.Utilities
{
    /// <summary>
    /// Session manager
    /// </summary>
    public class SessionManager
    {
        #region Public Is session exist
        /// <summary>
        /// Is session exist
        /// </summary>
        /// <param name="SessionName">Session name</param>
        /// <returns>Is exist</returns>
        public static bool IsExist(string SessionName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (CurrentContext.Session[SessionName] == null)
            {
                //If session is null
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Public Add new session
        /// <summary>
        ///  Add new session
        /// </summary>
        /// <param name="SessionName">Session name</param>
        /// <param name="SessionObject">Session content</param>
        public static void SetObject(string SessionName, object SessionObject)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (SessionObject != null)
            {
                CurrentContext.Session[SessionName] = SessionObject;
            }
        }
        #endregion

        #region Public Get session object
        /// <summary>
        /// Get session object
        /// </summary>
        /// <param name="SessionName">Session name</param>
        public static object GetObject(string SessionName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (IsExist(SessionName) == true)
            {
                return CurrentContext.Session[SessionName];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Public Delete session
        /// <summary>
        /// Delete session
        /// </summary>
        /// <param name="SessionName">Session name</param>
        public static void DeleteObject(string SessionName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (IsExist(SessionName) == true)
            {
                CurrentContext.Session.Remove(SessionName);
            }
        }
        #endregion

        #region Public End Session
        /// <summary>
        /// End Session
        /// </summary>
        public static void Abandon()
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            CurrentContext.Session.Abandon();
        }
        #endregion
    }
}
