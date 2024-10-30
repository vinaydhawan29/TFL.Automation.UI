using System.Collections.Concurrent;
using System.Reflection;

namespace TFL.Automation.UI.Helpers
{
    public static class StringExtensions
    {

        private static readonly object reflectionLock = new();
        private static readonly ConcurrentDictionary<string, MethodInfo> methodInfos = new();

        private static MethodInfo CacheMethodInfo<T>(string name, Func<ParameterInfo[], bool> prameterCheck)
        {
            Type targetType = typeof(T);

            string typeKey = $"{targetType.FullName}+{name}";

            if (!methodInfos.ContainsKey(typeKey))
                lock (reflectionLock)
                    if (!methodInfos.ContainsKey(typeKey))
                    {
                        var methodInfo = targetType.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && prameterCheck(m.GetParameters()));

                        if (methodInfo != null)
                            while (!methodInfos.ContainsKey(typeKey) && !methodInfos.TryAdd(typeKey, methodInfo))
                                if (!Thread.Yield())
                                    Thread.Sleep(0);
                    }

            return methodInfos.ContainsKey(typeKey) ? methodInfos[typeKey] : null;
        }

        public static bool TryParse<T>(this string source, out T result, T @default = default)
        {
            result = @default;

            if (source is null) return false;

            if (typeof(T) == typeof(string))
            {
                result = (T)(object)source;

                return true;
            }

            if (typeof(T).IsEnum)
            {
                if (Enum.TryParse(typeof(T), source, true, out object enumValue))
                {
                    result = (T)enumValue;

                    return true;
                }

                return false;
            }

            if (typeof(T) == typeof(Uri))
            {
                try
                {
                    result = (T)(object)(new Uri(source.ToString()));

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            var tryParseMethodInfo = CacheMethodInfo<T>("TryParse", (parameters) => parameters[0].ParameterType == typeof(string));
            var parseMethodInfo = CacheMethodInfo<T>("Parse", (parameters) => parameters[0].ParameterType == typeof(string));

            bool parsed = false;
            Type targetType = typeof(T);

            if (tryParseMethodInfo != null)
            {
                object[] args = new object[] { source, default(T) };

                parsed = (bool)tryParseMethodInfo.Invoke(targetType, args);

                result = parsed ? (T)args[1] : default;
            }

            if (!parsed && parseMethodInfo != null)
            {
                try
                {
                    result = (T)parseMethodInfo.Invoke(targetType, new object[] { source });

                    parsed = true;
                }
                catch
                {
                    // suppress because we are trying to parse
                }
            }

            return parsed;
        }
    }
}
