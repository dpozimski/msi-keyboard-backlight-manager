using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class AnalyticsInterceptor : IInterceptor
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsInterceptor(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public void Intercept(IInvocation invocation)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            invocation.Proceed();
            stopwatch.Stop();

            var key = $"Interceptor_{invocation.Method.Name}";
            var parameters = MapParameters(invocation.Arguments, invocation.Method.GetParameters()).ToDictionary(x => x.Key, x => x.Value?.ToString());
            var metrics = new Dictionary<string, double>()
            {
                { "Elapsed", stopwatch.ElapsedMilliseconds }
            };

            _analyticsService.TrackEvent(key, parameters, metrics);
        }

        private IEnumerable<KeyValuePair<string, string>> MapParameters(object[] arguments, ParameterInfo[] getParameters)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                yield return new KeyValuePair<string, string>(getParameters[i].Name, JsonConvert.SerializeObject(arguments[i]));
            }
        }
    }
}
