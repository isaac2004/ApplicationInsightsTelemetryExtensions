using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationInsightsTelemetryExtensions
{
    public class RefererTelemetryInitializer : ITelemetryInitializer
    {
        private const string HeaderNameDefault = "Referer";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefererTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null) throw new System.ArgumentNullException(nameof(httpContextAccessor));

            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry != null && telemetry is RequestTelemetry)
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext != null && httpContext.Request.Headers.TryGetValue(HeaderNameDefault, out var value))
                {
                    telemetry.Context.Properties["Referer"] = value.ToString();
                }
            }
        }
    }
}
