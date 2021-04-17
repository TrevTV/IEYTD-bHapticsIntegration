using System;
using System.Reflection;
using Harmony;

namespace BHapticsSupport
{
    public static class ReflectionTools
    {
        public static T GetValueFromObject<T>(object obj, string field)
            => (T)AccessTools.Field(obj.GetType(), field).GetValue(obj);

        public static FieldInfo GetFieldInfoFromObject(object obj, string field)
            => AccessTools.Field(obj.GetType(), field);
    }
}
