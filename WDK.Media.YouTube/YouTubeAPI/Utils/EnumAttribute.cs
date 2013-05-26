using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YouTubeAPI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DisplayName"></param>
        public EnumAttribute(string DisplayName)
        {
            this.DisplayName = DisplayName;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class EnumToString
    {
        public static string Get(Type enumType, object value)
        {
            MemberInfo memberInfo = enumType.GetField(value.ToString());
            EnumAttribute attribute = (EnumAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(EnumAttribute));
            return attribute != null ? attribute.DisplayName : String.Empty;
        }
    }
}
