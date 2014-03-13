using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.IO;

namespace Code.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// 计算MD5值
        /// </summary>
        /// <param name="s">原始字符串</param>
        /// <returns>MD5字符串</returns>
        public static string ToMd5(this string s)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").ToLower();
        }

        /// <summary>
        /// 将字符串转化为数字(转换失败返回0)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>数字</returns>
        public static int ToInt32(this string s)
        {
            int result;
            if (!Int32.TryParse(s, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转化为数字(转换失败返回0)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>数字</returns>
        public static long ToInt64(this string s)
        {
            long result;
            if (!Int64.TryParse(s, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转化为数字(转换失败返回0)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>数字</returns>
        public static double ToDouble(this string s)
        {
            double result;
            if (!Double.TryParse(s, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转化为数字(转换失败返回0)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>数字</returns>
        public static float ToFloat(this string s)
        {
            float result;
            if (!Single.TryParse(s, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转化为数字(转换失败返回0)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>数字</returns>
        public static byte ToByte(this string s)
        {
            byte result;
            if (!Byte.TryParse(s, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转化为GUID(转换失败返回Guid.Empty)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>GUID</returns>
        public static Guid ToGuid(this string s)
        {
            Guid result;
            if (!Guid.TryParse(s, out result))
            {
                result = Guid.Empty;
            }
            return result;
        }

        /// <summary>
        /// 将GUID转换为32字节小写字符串
        /// </summary>
        /// <param name="guid">guid值</param>
        /// <returns>转换过的字符串</returns>
        public static string ToGuidString(this Guid guid)
        {
            return guid.ToString("N");
        }

        /// <summary>
        /// 将字符串转化为bool(转换失败返回false)
        /// </summary>
        /// <param name="s">字符串值</param>
        /// <returns>bool>
        public static bool ToBoolean(this string s)
        {
            bool result;
            if (!Boolean.TryParse(s, out result))
            {
                try
                {
                    result = Convert.ToBoolean(s);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        private static readonly string _isNumberPattern = "^\\d+$";
        /// <summary>
        /// 检测字符串是否为数字
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNumber(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s, _isNumberPattern);
        }

        /// <summary>
        /// 判断字符串是否为英文字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>判定结果</returns>
        public static bool IsLetter(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s, "^[a-z|A-Z]+$");
        }

        public static string ToBooleanString(this bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// 将对象转化为Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="instanse">对象本身</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString<T>(this T instanse)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    js.WriteObject(ms, instanse);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(ms);
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return String.Empty;
            }
        }



        /// <summary>
        /// 将字符串转化为JSON对象，如果转换失败，返回default(T)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="s">字符串</param>
        /// <returns>转换值</returns>
        public static T ToJsonObject<T>(this string s)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(ms);
                    sw.Write(s);
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)js.ReadObject(ms);
                }
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// json字符串转为List集合
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回List集合</returns>
        public static List<T> JsonStringToList<T>(this string jsonStr)
        {
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                List<T> list = jsSerializer.Deserialize<List<T>>(jsonStr);
                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将byte转为bool值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this byte value)
        {
            if (value == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 将int转为bool值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this int value)
        {
            if (value == 0)
                return false;
            else
                return true;
        }

        public static string Left(this string s, int num, string appendString = "...")
        {
            if (String.IsNullOrWhiteSpace(s))
                return String.Empty;
            if (s.Length > num)
                return s.Substring(0, num) + appendString;
            else
                return s;
        }

        public static bool IsInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替(一般用于英文、符号)
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            if (string.IsNullOrEmpty(p_SrcString) == true)
            {
                return string.Empty;
            }

            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                        return "";
                    else
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }

                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                        nRealLength = p_Length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 移除HTML代码
        /// </summary>
        /// <param name="htmlString">要移除的字符串</param>
        /// <returns></returns>
        public static string RemoveHtml(this string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString))
            {
                return htmlString;
            }
            //删除脚本
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
            //htmlString = Regex.Replace(htmlString,@"<A>.*</A>","");
            //htmlString = Regex.Replace(htmlString,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");
            htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlString.Replace("<", "");
            htmlString.Replace(">", "");
            htmlString.Replace("\r\n", "");
            //htmlString=HttpContext.Current.Server.HtmlEncode(htmlString).Trim();
            return htmlString;
        }

        /// <summary>
        /// 去除文本里的所有空格、tab、换行、新行
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns></returns>
        public static string RemoveSpaceWrap(this string s)
        {
            if (string.IsNullOrEmpty(s) == true)
            {
                return string.Empty;
            }
            s = Regex.Replace(s, @"\s", "");
            return s;
        }

        public static string HandleSpecialChars(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(inputString) == false)
            {
                char[] old = inputString.ToCharArray();
                for (int i = 0; i < old.Length; i++)
                {
                    char c = old[i];
                    switch (c)
                    {
                        case '[':
                            {
                                sb.Append("[[]");
                                break;
                            }
                        case '\'':
                            {
                                sb.Append("\'\'");
                                break;
                            }
                        case '%':
                            {
                                sb.Append("[%]");
                                break;
                            }
                        case '_':
                            {
                                sb.Append("[_]");
                                break;
                            }
                        case '^':
                            {
                                sb.Append("[^]");
                                break;
                            }
                        default:
                            {
                                sb.Append(c); break;
                            }
                    }
                }

            }
            return sb.ToString();
        }
    }
}
