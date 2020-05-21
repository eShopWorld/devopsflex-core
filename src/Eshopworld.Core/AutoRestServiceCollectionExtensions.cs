using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest;

namespace Eshopworld.Core
{
    /// <summary>
    /// Provide extensions methods for <see cref="IServiceCollection"/> to work with AutoRest clients.
    /// </summary>
    public static class AutoRestServiceCollectionExtensions
    {
        private const string BaseUriPropertyName = "BaseUri";

        /// <summary>
        /// Register a client generated with AutoRest.
        /// The client is authenticated using the registered <see cref="ServiceClientCredentials"/>.
        /// </summary>
        /// <typeparam name="TInt">The client interface</typeparam>
        /// <typeparam name="TImpl">The client implementation</typeparam>
        /// <param name="services">The service collection</param>
        /// <param name="baseAddress">The endpoint to target with the client</param>
        /// <param name="serviceLifetime">The lifetime of the client</param>
        /// <returns>The instance of ServiceCollection to chain calls.</returns>
        public static IServiceCollection AddAutoRestApi<TInt, TImpl>(this IServiceCollection services,
            Uri baseAddress, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TInt : class
            where TImpl : TInt
        {
            services.AddHttpClient();

            // Check interface
            var intType = typeof(TInt);
            if (!intType.IsInterface)
            {
                throw new InvalidOperationException($"Type {intType.Name} is not an interface.");
            }
            
            // Check implementation
            var implType = typeof(TImpl);
            if (implType.IsInterface || implType.IsAbstract)
            {
                throw new InvalidOperationException($"Type {implType.Name} is not a concrete class.");
            }

            // Check constructor
            var ctorTypes = new[] {typeof(ServiceClientCredentials), typeof(HttpClient), typeof(bool)};
            var ctor = implType.GetConstructor(ctorTypes);
            if (ctor == null)
            {
                throw new InvalidOperationException($"Type {implType.Name} does not have expected constructor: ({nameof(ServiceClientCredentials)}, {nameof(HttpClient)}, {nameof(Boolean)})");
            }

            // Check BaseUri property
            var baseUriProperty = implType.GetProperty(BaseUriPropertyName);
            if (baseUriProperty == null || baseUriProperty.PropertyType != typeof(Uri))
            {
                throw new InvalidOperationException($"Type {implType.Name} does not have expected property {BaseUriPropertyName} or type {nameof(Uri)}.");
            }

            services.Add(new ServiceDescriptor(
                intType,
                serviceProvider =>
                {
                    var clientCredentials = serviceProvider.GetRequiredService<ServiceClientCredentials>();
                    var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient();

                    // Create API
                    var parameters = new object[]
                    {
                        clientCredentials,
                        httpClient,
                        false
                    };
                    var api = (TImpl)ctor.Invoke(parameters);
                    baseUriProperty.SetValue(api, baseAddress);
                    return api;
                },
                serviceLifetime
            ));

            return services;
        }
    }
}
