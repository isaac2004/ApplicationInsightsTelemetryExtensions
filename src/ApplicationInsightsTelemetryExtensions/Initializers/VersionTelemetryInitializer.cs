using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ApplicationInsightsTelemetryExtensions.Initializers
{
    /// <summary>
    /// Version TelemetryInitializer
    /// </summary>
    public class VersionTelemetryInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Initializes properties of the specified 
        /// <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry" /> object.
        /// </summary>
        /// <param name="telemetry">the telemetry channel</param>
        public void Initialize(ITelemetry telemetry)
        {
            // Application Version 
            telemetry.Context.Component.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
