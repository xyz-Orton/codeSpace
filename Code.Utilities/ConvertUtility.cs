using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CPTP.Utilities
{
    /// <summary>
    /// 表示定义一组条件并确定指定对象是否能把 String 类型的字符串转换为特定的类型
    /// </summary>
    /// <typeparam name="T">返回的实体对象的类型</typeparam>
    /// <param name="value">待转换的字符串</param>
    /// <param name="returnEntity">返回的实体对象</param>
    /// <returns>如果 value 符合由此委托表示的方法中定义的转换条件，则为 true；否则为 false。</returns>
    public delegate bool TryConvertPredicate<T>(string value, out T returnEntity);

    public class ConvertUtility
    {
        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为整型的集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<decimal> ParseTypeToDecimalBySplitWithComma(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new List<decimal>();
            }
            List<decimal> result = new List<decimal>();
            string[] sourceItems = source.Split(',');
            decimal itemValue;
            foreach (string item in sourceItems)
            {
                if (decimal.TryParse(item, out itemValue))
                {
                    result.Add(itemValue);
                }
            }
            return result;
        }

        public static List<Guid> ParseTypeToGuidBySplitWithComma(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new List<Guid>();
            }

            List<Guid> result = new List<Guid>();
            string[] sourceItems = source.Split(',');
            Guid itemValue;
            foreach (string item in sourceItems)
            {
                if (Guid.TryParse(item, out itemValue))
                {
                    result.Add(itemValue);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串动态强制转换成对应类型的集合，如果格式不正确或转换失败，将抛出异常
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<T> ParseTypeBySplitWithComma<T>(string source) where T : struct
        {
            Type t = typeof(T);
            bool isBasicValueType = t == typeof(byte) || t == typeof(short) || t == typeof(int) || t == typeof(long) || t == typeof(float) || t == typeof(double) || t == typeof(DateTime) || t == typeof(decimal);
            bool isGuidValueType = t == typeof(Guid);
            if (!(isBasicValueType || isGuidValueType))
            {
                throw new ArgumentException("泛型参数错误！只能是 byte、short、int、long、float、double、DateTime、decimal 这些基本类型或 Guid 类型。");
            }
            string[] sourceItems = source.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<T> result = new List<T>();
            if (isBasicValueType)
            {
                MethodInfo methodInfo = t.GetMethod("Parse", new Type[] { typeof(string) });
                foreach (string item in sourceItems)
                {
                    //T objT = Activator.CreateInstance<T>();   //也可以实例化，把这个实例化后的对象 传到 Invoke 方法的第一个参数
                    result.Add((T)methodInfo.Invoke(null /* 这里要求 T 类型的实例，由于是 Stuct 类型，所以不需要实例化 */, new object[] { item }));
                }
            }
            else if (isGuidValueType)
            {
                ConstructorInfo constructorInfo = t.GetConstructor(new Type[] { typeof(string) });
                foreach (string item in sourceItems)
                {
                    result.Add((T)constructorInfo.Invoke(new object[] { item }));
                }
            }
            return result;
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 int 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<int> ParseTypeBySplitWithCommaTo_Int(string source)
        {
            return ParseTypeBySplitWithComma<int>(source, int.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 short 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<short> ParseTypeBySplitWithCommaTo_Short(string source)
        {
            return ParseTypeBySplitWithComma<short>(source, short.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 float 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<float> ParseTypeBySplitWithCommaTo_Float(string source)
        {
            return ParseTypeBySplitWithComma<float>(source, float.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 double 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<double> ParseTypeBySplitWithCommaTo_Double(string source)
        {
            return ParseTypeBySplitWithComma<double>(source, double.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 DateTime 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<DateTime> ParseTypeBySplitWithCommaTo_DateTime(string source)
        {
            return ParseTypeBySplitWithComma<DateTime>(source, DateTime.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 bool 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<bool> ParseTypeBySplitWithCommaTo_Bool(string source)
        {
            return ParseTypeBySplitWithComma<bool>(source, bool.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 byte 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<byte> ParseTypeBySplitWithCommaTo_Byte(string source)
        {
            return ParseTypeBySplitWithComma<byte>(source, byte.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 char 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<char> ParseTypeBySplitWithCommaTo_Char(string source)
        {
            return ParseTypeBySplitWithComma<char>(source, char.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 decimal 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<decimal> ParseTypeBySplitWithCommaTo_Decimal(string source)
        {
            return ParseTypeBySplitWithComma<decimal>(source, decimal.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 long 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<long> ParseTypeBySplitWithCommaTo_Long(string source)
        {
            return ParseTypeBySplitWithComma<long>(source, long.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 uint 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<uint> ParseTypeBySplitWithCommaTo_Uint(string source)
        {
            return ParseTypeBySplitWithComma<uint>(source, uint.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 ushort 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<ushort> ParseTypeBySplitWithCommaTo_Ushort(string source)
        {
            return ParseTypeBySplitWithComma<ushort>(source, ushort.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 ulong 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<ulong> ParseTypeBySplitWithCommaTo_Ulong(string source)
        {
            return ParseTypeBySplitWithComma<ulong>(source, ulong.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为 Guid 集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        public static List<Guid> ParseTypeBySplitWithCommaTo_Guid(string source)
        {
            return ParseTypeBySplitWithComma<Guid>(source, Guid.TryParse);
        }

        /// <summary>
        /// 根据用英文逗号连接的字符串，尝试着转换为相应类型的集合。错误的值将会被忽略。
        /// </summary>
        /// <param name="source">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <param name="tryConvertPredicate">需要转换对应类型的字符串，多个用逗号分隔</param>
        /// <returns>集合</returns>
        private static List<TResult> ParseTypeBySplitWithComma<TResult>(string source, TryConvertPredicate<TResult> tryConvertPredicate)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new List<TResult>();
            }
            List<TResult> result = new List<TResult>();
            string[] sourceItems = source.Split(',');
            TResult itemValue;
            foreach (string item in sourceItems)
            {
                if (tryConvertPredicate(item, out itemValue))
                {
                    result.Add(itemValue);
                }
            }
            return result;
        }

        /// <summary>
        /// 分数转换为显示字符串
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static string ScoreDoubleToString(double? score)
        {
            if (score.HasValue)
            {
                try
                {
                    return score.Value.ToString("f1");
                }
                catch (FormatException ex)
                {
                    return "-";
                }
            }
            else
            {
                return "-";
            }
        }
    }
}
