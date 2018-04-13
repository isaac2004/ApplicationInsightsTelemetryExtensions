using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Net.Http;


namespace ApplicationInsightsTelemetryExtensions
{
    /// <summary>
    /// Initializer will add Post and Put Request bodies onto Application Insights Request Tracking
    /// Supports JSON and Form
    /// </summary>
    public class RequestBodyInitializer : ITelemetryInitializer
    {
        private IHttpContextAccessor _httpContextAccessor;
        public RequestBodyInitializer(IHttpContextAccessor httpContextAccessor)
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
                if ((httpContext.Request.Method == HttpMethod.Post.ToString() || httpContext.Request.Method == HttpMethods.Put.ToString())
                    && httpContext.Request.Body.CanRead)
                {
                    try
                    {
                        httpContext.Request.EnableRewind();
                        string bodyContent = new StreamReader(httpContext.Request.Body).ReadToEnd();
                        httpContext.Request.Body.Position = 0;
                        requestTelemetry.Properties.Add("body", bodyContent);
                    }
                    catch (ObjectDisposedException) { }
                }
            }
        }
    }
}
