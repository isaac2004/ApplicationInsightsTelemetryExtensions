using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ApplicationInsightsTelemetryExtensions
{
    /// <summary>
    /// Processor scrubs all guids from requests. This is handy if Api Keys in the form of Guids are passed in the Http Request
    /// </summary>
    public class CleanGuidFromRequestProcessor : ITelemetryProcessor
    {
        private static Regex _guidRegex = new Regex(
    @"\b[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly ITelemetryProcessor _next;

        public CleanGuidFromRequestProcessor(ITelemetryProcessor next)
        {
            _next = next;
        }

        public void Process(ITelemetry item)
        {
            var telemetry = item as RequestTelemetry;
            if (telemetry != null)
            {
                // Regex-replace anythign that looks like a GUID
                telemetry.Name = _guidRegex.Replace(telemetry.Name, "*****");
                telemetry.Url = new Uri(_guidRegex.Replace(telemetry.Url.ToString(), "*****"));
            }

            _next.Process(item);
        }
    }
}
