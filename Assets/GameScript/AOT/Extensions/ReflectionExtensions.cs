using System;
using System.Reflection;

namespace Game
{
    public static class ReflectionExtensions
    {
        public static Type GetFieldType(this MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }
            throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo");
        }

        public static void SetValue(this MemberInfo memberInfo, object obj, object value)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(obj, value);
                return;
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(obj, value);
                return;
            }
            throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo");
        }

        public static object GetValue(this MemberInfo memberInfo, object obj)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(obj);
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(obj);
            }
            throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo");
        }
    }
}
