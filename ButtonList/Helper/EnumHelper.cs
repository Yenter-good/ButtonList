using System;
using System.ComponentModel;
using System.Reflection;

namespace ButtonList.Helper
{
    internal static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举值的 Description 特性值。
        /// 如果没有定义 Description 特性，则返回枚举值的名称。
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>Description 特性的值或枚举值的名称</returns>
        internal static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            // 1. 获取枚举值的类型
            Type type = value.GetType();

            // 2. 获取枚举值的字段信息
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // 3. 从字段信息中获取 DescriptionAttribute 特性
            T[] attributes = (T[])fieldInfo.GetCustomAttributes(typeof(T), false);

            // 4. 如果特性存在，则返回其 Description 值，否则返回枚举值的名称
            if (attributes.Length > 0)
            {
                return attributes[0];
            }
            else
            {
                return null;
            }
        }
    }
}