using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Code.Utilities
{
    /// <summary>
    /// ����ת��
    /// </summary>
    public class ConverterHelper
    {
        /// <summary>
        /// string��ת��Ϊbool��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����bool���ͽ��</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
                return StrToBool(expression, defValue);

            return defValue;
        }

        /// <summary>
        /// string��ת��Ϊbool��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����bool���ͽ��</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                    return true;
                else if (string.Compare(expression, "false", true) == 0)
                    return false;
            }
            return defValue;
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// ������ת��ΪInt32����,ת��ʧ�ܷ���0
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int StrToInt(string str)
        {
            return StrToInt(str, 0);
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(str, defValue));
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float ObjectToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float ObjectToFloat(object strValue)
        {
            return ObjectToFloat(strValue.ToString(), 0);
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float StrToFloat(object strValue)
        {
            if ((strValue == null))
                return 0;

            return StrToFloat(strValue.ToString(), 0);
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
                return defValue;

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// string��ת��ΪDateTime��
        /// </summary>
        /// <param name="strValue">Ҫת����string��</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����datetime���ͽ����ת��ʧ��Ĭ�Ϸ��ص�ǰʱ��</returns>
        public static DateTime ObjectToDateTime(object expression, DateTime defValue)
        {
            if (expression != null)
            {
                try
                {
                    defValue = Convert.ToDateTime(expression);
                }
                catch (Exception)
                {

                    if (defValue == null)
                    {
                        defValue = DateTime.Now;
                    }
                }

            }
            return defValue;
        }

        /// <summary>
        /// ����ת��ΪDateTime��
        /// </summary>
        /// <param name="strValue">Ҫת���Ķ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����datetime���ͽ��</returns>
        public static DateTime StringToDateTime(object expression, DateTime defValue)
        {
            return ObjectToDateTime(expression, defValue);
        }

        /// <summary>
        /// ����ת��ΪDateTime��
        /// </summary>
        /// <param name="strValue">Ҫת���Ķ���</param>
        /// <returns>ת�����datetime���ͽ��,ת��ʧ��Ĭ�Ϸ��ص�ǰʱ��</returns>
        public static DateTime StringToDateTime(object expression)
        {
            return ObjectToDateTime(expression, DateTime.Now);
        }

        public static byte[] StringToByte(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// ת���ַ�����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToArray(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            StringBuilder sbStr = new StringBuilder();
            if (str.Contains(","))
            {
                string[] strArr = str.Split(',');
                for (int i = 0; i < strArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(sbStr.ToString()))
                        sbStr.Append(",");
                    sbStr.Append("'" + strArr[i] + "'");
                }
            }
            else
            {
                sbStr.Append("'" + str + "'");
            }

            return sbStr.ToString();
        }

        public static string StringToUniqueidentifier(string str)
        {
            string strValue = null;
            if (!string.IsNullOrEmpty(str))
                strValue = str;

            return strValue;
        }

        /// <summary>
        /// ���1λС��λ��
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DoubleToString(double value)
        {
            string result = string.Empty;
            result = value.ToString("#0.0");

            return result;
        }

        public static Array ConvertArrayToInt(Array srcArray)
        {
            if (srcArray == null)
                throw new ArgumentNullException("srcArray");
            int len = srcArray.Length;
            Array a = Array.CreateInstance(typeof(int), len);
            for (int i = 0; i < len; i++)
                a.SetValue(Convert.ToInt32(srcArray.GetValue(i)), i);
            return a;
        }

        public static string TrimString(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            else return str.Trim();
        }
    }
}
