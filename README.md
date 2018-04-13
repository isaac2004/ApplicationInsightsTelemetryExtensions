# ApplicationInsightsTelemetryExtensions

## Overview

Application Insights has the ability to extend the telemetry collection processs with `TelemetryInitializers` and `TelemetryProcessors`

<https://docs.microsoft.com/en-us/azure/application-insights/app-insights-api-filtering-sampling>

These extensions allow you to better manage your telemetry by adding to it, or filtering out unwanted collection. This collection is a set of Telemetry Processors and Initializers useful to extend Telemetry Collection with Application Insights.

## Installation

1. Add a reference to the [NuGet package](https://www.nuget.org/packages/ApplicationInsightsTelemetryExtensions/):
    ```console
    dotnet add package ApplicationInsightsTelemetryExtensions
    ```

1. Restore NuGet package:
    ```console
    dotnet restore

## Acknowledgements

Some of these I came up with myself, others I collected from some amazing blog posts.

* [Maarten Balliauw - Appliction Telemetry Processors](https://blog.maartenballiauw.be/post/2017/01/31/application-insights-telemetry-processors.html)
* [GÉRALD BARRÉ - Application Insights - Track HTTP Referer](https://www.meziantou.net/2017/08/07/application-insights-track-http-referer)
* [cmendible/VersionTelemetryInitializer.cs](https://gist.github.com/cmendible/0c333ea93d94ddbbf600)
* [Paris Polyzos - Tracking request headers in Application Insights](https://ppolyzos.com/2017/01/29/tracking-request-headers-in-application-insights/)