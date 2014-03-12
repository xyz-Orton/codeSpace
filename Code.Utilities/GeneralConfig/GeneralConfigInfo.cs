using System;
using System.Collections.Generic;
using System.Text;

namespace CPTP.Utilities
{
    /// <summary>
    /// 基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {

        #region 系统设置

        #region 字段
        private string m_logourl = "";//logo的URL地址
        private string m_copyrightinfo = "";//copyright信息
        private string m_interval = "";//上传预览文件转换间隔时间
        #endregion

        /// <summary>
        /// logo的URL地址
        /// </summary>
        public string LogoUrl
        {
            get { return m_logourl; }
            set { m_logourl = value; }
        }

        /// <summary>
        /// copyright信息
        /// </summary>
        public string CopyRightInfo
        {
            get { return m_copyrightinfo; }
            set { m_copyrightinfo = value; }
        }

        /// <summary>
        /// 上传预览文件转换间隔时间
        /// </summary>
        public string Interval
        {
            get { return m_interval; }
            set { m_interval = value; }
        }
        #endregion
    }
}
