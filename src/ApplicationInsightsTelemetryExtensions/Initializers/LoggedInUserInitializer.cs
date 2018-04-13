using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System;

namespace ApplicationInsightsTelemetryExtensions
{
    /// <summary>
    /// Initializer will add signed in user identity name to Application Insights Request Tracking for authenticated events
    /// </summary>
    public class LoggedInUserInitializer : ITelemetryInitializer
    {
        private IHttpContextAccessor _httpContextAccessor;
        public LoggedInUserInitializer(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null) throw new System.ArgumentNullException(nameof(httpContextAccessor));

            _httpContextAccessor = httpContextAccessor;
        }
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry != null && telemetry is RequestTelemetry)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null && httpContext.User.Identity.IsAuthenticated == true && httpContext.User.Identity.Name != null)
                {
                    telemetry.Context.User.AuthenticatedUserId = httpContext.User.Identity.Name;
                }
            }
        }
    }
}
