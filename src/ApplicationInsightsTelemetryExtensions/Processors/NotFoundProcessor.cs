using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApplicationInsightsTelemetryExtensions.Processors
{
    /// <summary>
    /// Processor filters out requests that return a 404
    /// </summary>
   public class NotFoundProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }

        public NotFoundProcessor(ITelemetryProcessor next)
        {
            this.Next = next;
        }

        public void Process(ITelemetry item)
        {
            var request = item as RequestTelemetry;

            if (request != null &&
            request.ResponseCode.Equals(((int)HttpStatusCode.NotFound).ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // To filter out an item, just terminate the chain:
                return;
            }
            // Send everything else:
            this.Next.Process(item);
        }
    }
}
