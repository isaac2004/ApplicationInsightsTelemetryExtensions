using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
/// <summary>
/// Processor filters out requests for robots.txt, which can skew request numbers if not filtered in reporting
/// </summary>
public class BotRequestTracking : ITelemetryProcessor
{
    public BotRequestTracking(ITelemetryProcessor next)
    {
        Next = next;
    }

    private ITelemetryProcessor Next { get; set; }

    public void Process(ITelemetry item)
    {
        var request = item as RequestTelemetry;
        if (request != null)
        {

            if (request.Name.ToLower().Contains("robots.txt"))
            {
                return;
            }
        }

        Next.Process(item);
    }
}