using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Music.Core.Components
{
    public static class RegexComponent
    {
        /// <summary>
        /// 判断是否汉字
        /// </summary>
        public static bool IsChinese(string value)
        {
            Regex rg = new Regex("^[\u4e00-\u9fa5]$");
            return rg.IsMatch(value);
        }

        /// <summary>
        /// 判断是否数字
        /// </summary>
        public static bool IsNumber(string value)
        {
            Regex rg = new Regex("^[0-9]$");
            return rg.IsMatch(value);
        }

        /// <summary>
        /// 判断是否字母
        /// </summary>
        public static bool IsWord(string value)
        {
            Regex rg = new Regex("^[a-zA-Z]$");
            return rg.IsMatch(value);
        }


        /// <summary>
        /// 判断是否汉字或数字或英文单词
        /// </summary>
        public static bool IsChineseOrNumberOrWord(string value)
        {
            Regex rg = new Regex("^[\u4e00-\u9fa5a-zA-Z0-9]$");
            return rg.IsMatch(value);
        }

        public static bool IsValidEmailAddress(this string email)
        {
            Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            return regex.IsMatch(email);

        }

        public static bool IsValidURL(this string url)
        {
            Regex regex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

            return regex.IsMatch(url);

        }

        public static bool OnlyCharacters(this string character)
        {
            Regex regex = new Regex(@"^.[A-Za-z]+$");

            return regex.IsMatch(character);

        }

        public static bool OnlyNumber(this string number)
        {
            Regex regex = new Regex(@"^.[0-9]*$");

            return regex.IsMatch(number);

        }

        public static bool IsValidDate(this string date)
        {   //验证日期类型为yyyy-MM-dd

            Regex regex = new Regex(@"^((((19|20)(([02468][048])|([13579][26]))-02-29))|((20[0-9][0-9])|(19[0-9][0-9]))-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30)))))$");

            return regex.IsMatch(date);

        }

        public static bool IsValidDateTime(this string dateTime)
        {   
            //验证日期类型为yyyy-MM-dd hh:mm:ss
            Regex regex = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$");

            return regex.IsMatch(dateTime);

        }

        public static bool IsValidUSPhone(this string phone)
        {
            Regex regex = new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");

            return regex.IsMatch(phone);

        }

        public static bool IsValidUSZipCode(this string zipcode)
        {
            Regex regex = new Regex(@"\d{5}(-\d{4})?");

            return regex.IsMatch(zipcode);

        }

        public static bool IsValidKorean(this string korean)
        {
            Regex regex = new Regex(@"^.[\uac00-\ud7af\u1100-\u11FF\u3130-\u318f]+$");

            return regex.IsMatch(korean);

        }

        public static bool IsValidCNMobile(this string mobile)
        {
            Regex regex = new Regex(@"^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[0-9]\d{8}|18[0-9]\d{8}");

            return regex.IsMatch(mobile);

        }


        public static bool IsValidCNPhone(this string phone)
        {
            Regex regex = new Regex(@"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}");

            return regex.IsMatch(phone);

        }

        public static bool IsValidCNZipCode(this string zipcode)
        {
            Regex regex = new Regex(@"\d{6}");

            return regex.IsMatch(zipcode);

        }



        public static bool IsValidCNID(this string ID)
        {   //验证身份证是否为15位或18位

            Regex regex = new Regex(@"d{18}|d{15}");

            return regex.IsMatch(ID);

        }
    }
}
