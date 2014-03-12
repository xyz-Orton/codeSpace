using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.IO;

namespace CPTP.Utilities
{
    /// <summary>
    /// 页面验证及处理类
    /// </summary>
    public class PageHelper
    {
        #region 服务器URL
        /// <summary>
        /// 服务器URL
        /// </summary>
        /// <returns></returns>
        public static string GetServerUrl()
        {
            return string.Format("http://{0}/", HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
        }
        #endregion

        #region 获得当前绝对路径 public static string GetMapPath(string strPath)
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        #region 返回URL中结尾的文件名 public static string GetUrlFilename(string url)
        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetUrlFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }
        #endregion

        #region 获取站点根目录URL public static string GetRootUrl(string forumPath)
        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl(string forumPath)
        {
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host.ToString(),
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 forumPath);
        }
        #endregion

        #region 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] userip = StringHelper.SplitString(ip, @".");

            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = StringHelper.SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                        return true;

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                            r++;
                        else
                            break;
                    }
                    else
                        break;
                }

                if (r == 4)
                    return true;
            }
            return false;
        }
        #endregion

        #region Cookie 相关操作

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();

            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpContext.Current.Request.Cookies[strName][key].ToString();

            return "";
        }

        #endregion

        #region 根据Url获得源文件内容 public static string GetSourceTextByUrl(string url)
        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Timeout = 20000;//20秒超时
            WebResponse response = request.GetResponse();

            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream);
            return sr.ReadToEnd();
        }
        #endregion

        #region 判断当前页面是否接收到了Post请求 public static bool IsPost()
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        #endregion

        #region 判断当前页面是否接收到了Get请求 public static bool IsGet()
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        #endregion

        #region 返回指定的服务器变量信息  public static string GetServerString(string strName)
        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
                return "";

            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }
        #endregion

        #region 返回上一个页面的地址 public static string GetUrlReferrer()
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;
        }
        #endregion

        #region 得到当前完整主机头 public static string GetCurrentFullHost()
        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());

            return request.Url.Host;
        }
        #endregion

        #region 得到主机头 public static string GetHost()
        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }
        #endregion

        #region 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在)) public static string GetRawUrl()
        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        #endregion

        #region 判断当前访问是否来自浏览器软件 public static bool IsBrowserGet()
        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region 判断是否来自搜索引擎链接 public static bool IsSearchEnginesGet()
        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;

            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region 获得当前完整Url地址 public static string GetUrl()
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        #endregion

        #region 获得指定Url参数的值 public static string GetQueryString(string strName)
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return GetQueryString(strName, false);
        }
        #endregion

        #region 获得指定Url参数的值 public static string GetQueryString(string strName, bool sqlSafeCheck)
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName] == "undefined")
                return "";

            if (sqlSafeCheck && !ValidationHelper.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.QueryString[strName];
        }
        #endregion

        #region 获得当前页面的名称 public static string GetPageName()
        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }
        #endregion

        #region 返回表单或Url参数的总个数 public static int GetParamCount()
        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }
        #endregion

        #region 获得指定表单参数的值 public static string GetFormString(string strName)
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            return GetFormString(strName, false);
        }
        #endregion

        #region 获得指定表单参数的值 public static string GetFormString(string strName, bool sqlSafeCheck)
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            if (sqlSafeCheck && !ValidationHelper.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.Form[strName];
        }
        #endregion

        #region 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值 public static string GetString(string strName)
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }
        #endregion

        #region 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值 public static string GetString(string strName, bool sqlSafeCheck)
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
                return GetFormString(strName, sqlSafeCheck);
            else
                return GetQueryString(strName, sqlSafeCheck);
        }
        #endregion

        #region 获得指定Url参数的int类型值 public static int GetQueryInt(string strName)
        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName)
        {
            return ConverterHelper.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }
        #endregion

        #region 获得指定Url参数的int类型值 public static int GetQueryInt(string strName, int defValue)
        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return ConverterHelper.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }
        #endregion

        #region 获得指定表单参数的int类型值 public static int GetFormInt(string strName, int defValue)
        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return ConverterHelper.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }
        #endregion

        #region 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值 public static int GetInt(string strName, int defValue)
        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
                return GetFormInt(strName, defValue);
            else
                return GetQueryInt(strName, defValue);
        }
        #endregion

        #region 获得指定Url参数的float类型值 public static float GetQueryFloat(string strName, float defValue)
        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return ConverterHelper.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }
        #endregion

        #region 获得指定表单参数的float类型值 public static float GetFormFloat(string strName, float defValue)
        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return ConverterHelper.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }
        #endregion

        #region 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值  public static float GetFloat(string strName, float defValue)
        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
                return GetFormFloat(strName, defValue);
            else
                return GetQueryFloat(strName, defValue);
        }
        #endregion

        #region 获得当前页面客户端的IP  public static string GetRealIP()
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns></returns>
        public static string GetRealIP()
        {
            return GetIP();
        }
        #endregion

        #region 获得当前页面客户端的IP public static string GetIP()
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !ValidationHelper.IsIP(result))
                return "127.0.0.1";

            return result;
        }
        #endregion

        #region 保存用户上传的文件 public static void SaveRequestFile(string path)
        /// <summary>
        /// 保存用户上传的文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }
        #endregion

        #region 得到当前网站的真实路径 public static string GetTrueForumPath()
        /// <summary>
        /// 得到当前网站的真实路径
        /// </summary>
        /// <returns></returns>
        public static string GetTrueForumPath()
        {
            string webPath = HttpContext.Current.Request.Path;
            if (webPath.LastIndexOf("/") != webPath.IndexOf("/"))
                webPath = webPath.Substring(webPath.IndexOf("/"), webPath.LastIndexOf("/") + 1);
            else
                webPath = "/";

            return webPath;
        }
        #endregion

        #region 页面不做缓存
        public static void NoCacheThisPage()
        {
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Today.AddDays(-1);
            HttpContext.Current.Response.CacheControl = "no-cache";
        }
        #endregion

        #region 获取文件流类型 public static string GetFileType(string type)
        /// <summary>
        /// 获取文件流类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFileType(string type)
        {
            switch (type.ToLower())
            {
                case "doc":
                    return "application/vnd.ms-word";
                case "xls":
                    return "application/vnd.ms-excel";
                case "txt":
                    return "application/vnd.text";
                default:
                    return "application/attachment";
            }
        }
        #endregion

        #region 向浏览器输入文件下载
        /// <summary>
        /// 向浏览器输入文件下载
        /// </summary>
        /// <param name="tempFilePath">文件路径</param>
        /// <param name="tempFileName">文件名</param>
        /// <param name="tempFileType">文件类别</param>
        public static void WriteResponFile(string tempFilePath, string tempFileName, string tempFileType)
        {
            if (!Directory.Exists(tempFilePath))
            {
                Directory.CreateDirectory(tempFilePath);
            }
            if (File.Exists(tempFilePath+tempFileName))
            {
                FileInfo file = new FileInfo(tempFilePath+tempFileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "GB2312";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(file.Name));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                HttpContext.Current.Response.ContentType = GetFileType(tempFileType);
                // 把文件流发送到客户端
                //此处需优化，采用分段返回
                HttpContext.Current.Response.WriteFile(file.FullName);
                HttpContext.Current.Response.Flush();
                //输入以后删除这个临时文件
                if (File.Exists(file.FullName))
                    File.Delete(file.FullName);
                // 停止页面的执行
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
