using System;
using System.Collections.Generic;
using System.Text;

namespace GTA.SystemFramework.StateManager
{
    /// <summary>
    /// Cache Manager
    /// </summary>
    public class CacheManager
    {
        #region Public Is cache exist
        /// <summary>
        /// Is cache exist
        /// </summary>
        /// <param name="CacheName">Cache name</param>
        /// <returns>Is exist</returns>
        public static bool IsExist(string CacheName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (CurrentContext.Cache[CacheName] == null)
            {
                //If cache is null
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Public Add new cache
        /// <summary>
        /// Add new cache
        /// </summary>
        /// <param name="CacheName">Cache name</param>
        /// <param name="CacheObject">Cache object</param>
        public static void SetObject(string CacheName, object CacheObject)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (CacheObject != null)
            {
                CurrentContext.Cache[CacheName] = CacheObject;
            }
        }
        #endregion

        #region Public Get cache object
        /// <summary>
        /// Get cache object
        /// </summary>
        /// <param name="CacheName">Cache name</param>
        public static object GetObject(string CacheName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (IsExist(CacheName) == true)
            {
                return CurrentContext.Cache[CacheName];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Public Delete cache
        /// <summary>
        /// Delete cache
        /// </summary>
        /// <param name="CacheName">Cache name</param>
        public static void DeleteObject(string CacheName)
        {
            //Get context
            System.Web.HttpContext CurrentContext = System.Web.HttpContext.Current;

            if (IsExist(CacheName) == true)
            {
                CurrentContext.Cache.Remove(CacheName);
            }
        }
        #endregion
    }
}
