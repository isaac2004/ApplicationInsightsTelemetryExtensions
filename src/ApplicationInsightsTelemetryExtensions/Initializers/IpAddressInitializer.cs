using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System;

namespace ApplicationInsightsTelemetryExtensions
{
    /// <summary>
    /// Initializer will add IP Address onto Application Insights Request Tracking
    /// Be careful as it pertains to GDPR and collecting IP Address
    /// </summary>
    public class IpAddressInitializer : ITelemetryInitializer
    {
        private IHttpContextAccessor _httpContextAccessor;
        public IpAddressInitializer(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null) throw new System.ArgumentNullException(nameof(httpContextAccessor));

            _httpContextAccessor = httpContextAccessor;
        }
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry != null && telemetry is RequestTelemetry)
            {
                var requestTelemetry = telemetry as RequestTelemetry;
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    requestTelemetry.Context.Location.Ip = httpContext.Connection.RemoteIpAddress.ToString();
                }
            }
        }
    }
}
