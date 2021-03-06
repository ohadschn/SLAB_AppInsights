﻿using System;
using System.Diagnostics.Tracing;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;

// ReSharper disable once CheckNamespace
namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging
{
    public static class ApplicationInsightsSinkExtensions
    {
        /// <summary>
        /// Creates an event listener that logs using an <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="instrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// An event listener that uses <see cref="ApplicationInsightsSink" /> to log events.
        /// </returns>
        public static EventListener CreateListener(string instrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            var listener = new ObservableEventListener();
            listener.LogToApplicationInsights(instrumentationKey, telemetryInitializers);
            return listener;
        }

        /// <summary>
        /// Creates an event listener that logs using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// An event listener that uses <see cref="ApplicationInsightsSink" /> to log events.
        /// </returns>
        public static EventListener CreateListener(params ITelemetryInitializer[] telemetryInitializers)
        {
            var listener = new ObservableEventListener();
            listener.LogToApplicationInsights(telemetryInitializers);
            return listener;
        }

        /// <summary>
        /// Subscribes to an <see cref="IObservable{EventEntry}" /> using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="eventStream">The event stream. Typically this is an instance of <see cref="ObservableEventListener" />.</param>
        /// <param name="instrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// A subscription to the sink that can be disposed to unsubscribe the sink and dispose it, or to get access to the sink instance.
        /// </returns>
        public static SinkSubscription<ApplicationInsightsSink> LogToApplicationInsights(
            this IObservable<EventEntry> eventStream, String instrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            var sink = new ApplicationInsightsSink(instrumentationKey, telemetryInitializers);
            return new SinkSubscription<ApplicationInsightsSink>(eventStream.Subscribe(sink), sink);
        }

        /// <summary>
        /// Subscribes to an <see cref="IObservable{EventEntry}" /> using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="eventStream">The event stream. Typically this is an instance of <see cref="ObservableEventListener" />.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// A subscription to the sink that can be disposed to unsubscribe the sink and dispose it, or to get access to the sink instance.
        /// </returns>
        public static SinkSubscription<ApplicationInsightsSink> LogToApplicationInsights(
            this IObservable<EventEntry> eventStream, params ITelemetryInitializer[] telemetryInitializers)
        {
            var sink = new ApplicationInsightsSink(telemetryInitializers);
            return new SinkSubscription<ApplicationInsightsSink>(eventStream.Subscribe(sink), sink);
        }
    }
}