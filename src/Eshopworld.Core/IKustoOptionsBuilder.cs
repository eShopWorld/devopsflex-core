using System;
using System.Reflection;

namespace Eshopworld.Core
{
    public interface IKustoClusterBuilder
    {
        /// <summary>
        /// Configures the Kusto connection details to be used while streaming to Kusto.
        /// </summary>
        /// <param name="engine">The name of the Kusto engine that we are targeting.</param>
        /// <param name="region">The region of the Kusto engine that we are targeting.</param>
        /// <param name="database">The Kusto database in the engine, that we are targeting.</param>
        /// <param name="tenantId">The tenant ID for the subscription where the Kusto engine is.</param>
        /// <returns>Fluent API chain. Call Build() at the end.</returns>
        IKustoBufferedOptionsBuilder WithCluster(string engine, string region, string database, string tenantId);
    }

    public interface IKustoBufferedOptionsBuilder : IKustoOptionsBuilder
    {
        /// <summary>
        /// Configures the buffering options for Queued ingestion.
        /// </summary>
        /// <param name="options">The options to use for queued ingestion.</param>
        /// <returns>Fluent API chain. Call Build() at the end.</returns>
        IKustoOptionsBuilder WithBufferOptions(BufferedClientOptions options);
    }

    public interface IKustoOptionsBuilder
    {
        /// <summary>
        /// Registers an assembly for Kusto ingestion.
        ///     It will scan all the types, in the assembly, that inherit from <see cref="TelemetryEvent"/> and register them.
        /// </summary>
        /// <param name="assembly">The assembly that we want to scan types for Kusto ingestion.</param>
        /// <returns>Fluent API chain. Call Build() at the end.</returns>
        IKustoOptionsTypeBuilder RegisterAssembly(Assembly assembly);

        /// <summary>
        /// Registers <typeparamref name="T"/> type for Kusto ingestion.
        /// </summary>
        /// <typeparam name="T">The type we're registering for Kusto ingestion</typeparam>
        /// <returns>Fluent API chain. Call Build() at the end.</returns>
        IKustoOptionsTypeBuilder RegisterType<T>() where T : TelemetryEvent;

        /// <summary>
        /// Register configured message types and ingestion strategies. 
        /// </summary>
        /// <param name="onMessageSent">Callback invoked after messages have been sent to Kusto</param>
        void Build(Action<long> onMessageSent = null);
    }

    public interface IKustoOptionsTypeBuilder
    {
        /// <summary>
        /// Use the Queued ingestion client for the types we just registered.
        /// </summary>
        /// <returns>Fluent API chain. Call Build() at the end.</returns>
        IKustoOptionsBuilder WithQueuedClient();

        /// <summary>
        /// Use the Direct ingestion client for the types we just registered.
        /// </summary>
        /// <returns>Fluent API chain. Call Build() at the end.</returns>
        IKustoOptionsBuilder WithDirectClient();
    }

    public class BufferedClientOptions
    {
        /// <summary>
        /// Local app buffer ingestion time buffer. Defaults to 1 sec interval
        /// </summary>
        public TimeSpan IngestionInterval { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Max buffer size before flush
        /// </summary>
        public int BufferSizeItems { get; set; } = 100;

        /// <summary>
        /// Flush immediately from Kusto aggregator
        /// </summary>
        public bool FlushImmediately { get; set; } = true;
    }
}