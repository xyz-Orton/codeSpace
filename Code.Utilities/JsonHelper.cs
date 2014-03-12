using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CPTP.Utilities
{

    public delegate void FormatField(FormatEventArgs args);

    #region 格式化数据实体
    /// <summary>
    /// 格式化数据参数
    /// </summary>
    public class FormatEventArgs : EventArgs
    {
        private string columnName;

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }
        private object value;

        /// <summary>
        /// 数据
        /// </summary>
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool discardSource = false;

        /// <summary>
        /// 是否丢弃源数(默认false不丢弃)，如 UserId,丢弃后 只有UserName, 否则UserId和UserName都会进行转换
        /// </summary>
        public bool DiscardSource
        {
            get { return discardSource; }
            set { discardSource = value; }
        }
        //private bool formatCancel = true;
        ///// <summary>
        ///// 是否取消当前格式化
        ///// </summary>
        //public bool FormatCancel
        //{
        //    get { return formatCancel; }
        //    set { formatCancel = value; }
        //}

        private string result;
        /// <summary>
        /// 最终结果
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }


    }

    #endregion

    /// <summary>
    /// 转Json辅助类
    /// </summary>
    public class JsonHelper
    {
        //格式化数据参数
        private static FormatEventArgs formatArgs = new FormatEventArgs();
        /// <summary>
        /// 表示无数据的DataGrid
        /// </summary>
        public const string EmptyDataGrid = " {\"total\":0,\"rows\":[]}";

        #region DataTable转JSON格式

        /// <summary>
        /// DataTable转JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dt, string[] converts, FormatField action)
        {

            if (dt.Rows.Count == 0)
            {

                return "{}";
            }

            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                JsonHelper.ToJson(sb, row, converts, action);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        ///DataTable 转JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dt, FormatField action)
        {

            return JsonHelper.ToJson(dt, new string[] { "*" }, action);
        }

        /// <summary>
        ///DataTable 转JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dt)
        {

            return JsonHelper.ToJson(dt, null);
        }
        #endregion

        #region  DataRow转JSON格式
        /// <summary>
        /// DataRow转JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToJson(DataRow row, FormatField action)
        {
            return JsonHelper.ToJson(new StringBuilder(), row, new string[] { "*" }, action);
        }
        /// <summary>
        /// DataRow转JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToJson(DataRow row, string[] converts, FormatField action)
        {
            return JsonHelper.ToJson(new StringBuilder(), row, converts, action);
        }

        /// <summary>
        /// DataRow转JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static string ToJson(StringBuilder sb, DataRow row, string[] converts, FormatField action)
        {

            sb.Append("{");
            for (int j = 0; j < row.Table.Columns.Count; j++)
            {
                string colName = row.Table.Columns[j].ColumnName;
                if (converts == null || converts.Length == 0)
                {
                    continue;
                }
                if (converts[0] == "*" || converts.Contains(colName))
                {
                    formatArgs.ColumnName = colName;
                    formatArgs.Value = row[j];
                    formatArgs.DiscardSource = false;
                    formatArgs.Result = null;

                    if (action != null)
                    {
                        action.Invoke(formatArgs);

                        if (!string.IsNullOrEmpty(formatArgs.Result))
                        {
                            sb.Append(formatArgs.Result);
                            sb.Append(",");
                        }
                    }

                    if (!formatArgs.DiscardSource)
                    {
                        JsonHelper.ToJson(sb, colName, row[j].ToString());
                        sb.Append(",");
                    }
                }
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region 字符串拼装为JSON格式
        /// <summary>
        /// 字符串拼装为JSON格式
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ToJson(StringBuilder sb, string key, string value)
        {
            sb.Append("\"");
            sb.Append(key);
            sb.Append("\":\"");
            sb.Append(JsonHelper.JsonCharFilter(value));
            sb.Append("\"");

            return sb.ToString();
        }

        /// <summary>
        /// 字符串拼装为JSON格式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson(string key, string value)
        {
            return string.Format("\"{0}\":\"{1}\"", key, JsonHelper.JsonCharFilter(value));
        }

        #endregion

        #region datagrid json
        /// <summary>
        /// 转 easyui datagrid json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="total"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToDataGridJson(DataTable dt, int total, FormatField action)
        {
            return ToDataGridJson(dt, new string[] { "*" }, total, action);
        }


        /// <summary>
        /// 转 easyui datagrid json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="total"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToDataGridJson(DataTable dt, string[] converts, int total, FormatField action)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return JsonHelper.EmptyDataGrid;
            }

            string jsonData = JsonHelper.ToJson(dt, converts, action);
            jsonData = "{\"total\":" + total + ",\"rows\":[" + jsonData + "]}";
            return jsonData;
        }

        #endregion

        #region Combobox json
        /// <summary>
        /// 转  Combobox json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToComboboxJson(DataTable dt, FormatField action)
        {
            return JsonHelper.ToComboboxJson(dt, new string[] { "*" }, action);
        }

        /// <summary>
        /// 转  Combobox json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="converts"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ToComboboxJson(DataTable dt, string[] converts, FormatField action)
        {
            return "[" + JsonHelper.ToJson(dt, converts, action) + "]";
        }
        #endregion

        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\r\n", "<br>");
            sourceStr = sourceStr.Replace("\"", "“");
            sourceStr = sourceStr.Replace("'", "’");
            sourceStr = sourceStr.Replace("\r", " ");
            sourceStr = sourceStr.Replace("\f", " ");
            sourceStr = sourceStr.Replace("\n", " ");
            //sourceStr = sourceStr.Replace(" ", "");
            sourceStr = sourceStr.Replace("\t", "   ");
            // sourceStr = sourceStr.Replace(":", "：");
            sourceStr = sourceStr.Replace("\\", "/");
            sourceStr = sourceStr.Replace("\v", " ");
            sourceStr = sourceStr.Replace("—", "-");
            return sourceStr;
        }
    }
}
