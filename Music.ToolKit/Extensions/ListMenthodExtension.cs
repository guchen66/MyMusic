using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Music.ToolKit.Extensions
{
    public static class ListMethodExtension
    {
        public static int MyCount<T>(this IEnumerable<T> list)
        {
            int sum = 0;
            var e = list.GetEnumerator();//枚举数
            while (e.MoveNext())
            {
                sum++;
            }
            return sum;
        }
        //string的扩展方法
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }
        // list的扩展方法
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> func)
        {
            foreach (var item in source)
            {
                func(item);
            }
        }
        //C#本质论抄的
        public static IEnumerable<string> Lines(this StreamReader source)
        {
            if (source == null)
            {
                throw new ArgumentException("source");
            }
            string line;
            while ((line = source.ReadLine()) != null)
            {
                yield return line;
            }
        }
        //字符串扩展方法
        /// <summary>
        /// 如何判断字符串是数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInt(this string s)
        {

            return s.Trim().All(a => char.IsNumber(a));
        }


        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string Reverse(this string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        // 3. 判断字符串中是否包含指定的子字符串（不区分大小写）
        public static bool ContainsIgnoreCase(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // 4. 将字符串按指定长度进行截断，并添加省略号
        public static string Truncate(this string str, int maxLength)
        {
            if (str.Length <= maxLength)
                return str;
            else
                return str.Substring(0, maxLength) + "...";
        }

        // 5. 将字符串转换为标题格式（每个单词首字母大写）
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        // 6. 移除字符串中的所有空格字符
        public static string RemoveWhitespace(this string str)
        {
            return new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        // 7. 获取字符串的单词数量
        public static int WordCount(this string str)
        {
            return str.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        // 8. 判断字符串是否为有效的邮箱地址
        public static bool IsValidEmail(this string str)
        {
            return Regex.IsMatch(str, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        }

        // 9. 将字符串转换为指定编码的字节数组
        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        // 10. 去除字符串中的 HTML 标签
        public static string StripHtmlTags(this string str)
        {
            return Regex.Replace(str, "<.*?>", string.Empty);
        }


        // 1. 获取列表中的最大值
        public static T MaxValue<T>(this List<T> list) where T : IComparable<T>
        {
            if (list.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            T max = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].CompareTo(max) > 0)
                    max = list[i];
            }

            return max;
        }

        // 2. 获取列表中的最小值
        public static T MinValue<T>(this List<T> list) where T : IComparable<T>
        {
            if (list.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            T min = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].CompareTo(min) < 0)
                    min = list[i];
            }

            return min;
        }

        // 3. 判断列表是否包含指定的元素
        public static bool Contains<T>(this List<T> list, T item)
        {
            return list.IndexOf(item) != -1;
        }

        // 4. 在列表中查找满足指定条件的第一个元素
        public static T Find<T>(this List<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list)
            {
                if (predicate(item))
                    return item;
            }

            return default;
        }

        // 5. 将列表中的元素转换为新的列表
        public static List<TResult> ConvertAll<T, TResult>(this List<T> list, Func<T, TResult> converter)
        {
            List<TResult> result = new List<TResult>(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(converter(list[i]));
            }

            return result;
        }

        // 6. 统计列表中满足指定条件的元素个数
        public static int Count<T>(this List<T> list, Func<T, bool> predicate)
        {
            int count = 0;
            foreach (var item in list)
            {
                if (predicate(item))
                    count++;
            }

            return count;
        }

        // 7. 删除列表中满足指定条件的元素
        public static void RemoveAll<T>(this List<T> list, Func<T, bool> predicate)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                    list.RemoveAt(i);
            }
        }

        // 8. 随机打乱列表中的元素顺序
        public static void Shuffle<T>(this List<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // 9. 对列表中的元素进行排序（默认按升序）
        public static void Sort<T>(this List<T> list, Comparison<T> comparison)
        {
            list.Sort(comparison);
        }

        // 10. 判断两个列表是否相等（元素顺序也相同）
        public static bool SequenceEqual<T>(this List<T> list1, List<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(list1[i], list2[i]))
                    return false;
            }

            return true;
        }

    }
}
