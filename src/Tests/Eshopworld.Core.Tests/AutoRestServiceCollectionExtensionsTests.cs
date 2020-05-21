using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Eshopworld.Tests.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest;
using Moq;
using Xunit;

namespace Eshopworld.Core.Tests
{
    public class AutoRestServiceCollectionExtensionsTests
    {
        private readonly MyServiceProvider _provider;
        private readonly Uri _endpoint = new Uri("https://www.eshopworld.com");

        public AutoRestServiceCollectionExtensionsTests()
        {
            _provider = new MyServiceProvider();
            var httpFactoryMock = new Mock<IHttpClientFactory>();
            httpFactoryMock.Setup(f => f.CreateClient(string.Empty)).Returns(new HttpClient());

            // Register dependencies
            _provider.AddScoped<ServiceClientCredentials>(_ => new MyServiceClientCredentials());
            _provider.AddScoped(_ => httpFactoryMock.Object);
        }

        [Fact, IsUnit]
        public void AddAutoRestApi_WithValidClient_Succeed()
        {
            // Act
            var provider = _provider.AddAutoRestApi<IAuthoRestClient, AutoRestClient>(_endpoint);

            // Assert
            Assert.Equal(_provider, provider);

            var client = _provider.GetService<IAuthoRestClient>() as AutoRestClient;
            Assert.NotNull(client);
            Assert.Equal(_endpoint, client.BaseUri);
        }

        [Fact, IsUnit]
        public void AddAutoRestApi_WithValidClient_WithSpecificScope_Succeed()
        {
            // Act
            var provider = _provider.AddAutoRestApi<IAuthoRestClient, AutoRestClient>(_endpoint, ServiceLifetime.Singleton);

            // Assert
            Assert.Equal(_provider, provider);

            var (service, lifeTime) = _provider.GetServiceWithLifetime(typeof(IAuthoRestClient));
            var client = service as AutoRestClient;
            Assert.NotNull(client);
            Assert.Equal(_endpoint, client.BaseUri);
            Assert.Equal(ServiceLifetime.Singleton, lifeTime);
        }

        [Fact, IsUnit]
        public void AddAutoRestApi_WithInvalidInterface_Throws()
        {
            // Act
            void Add() => _provider.AddAutoRestApi<NoBaseUriAutoRestClient, AutoRestClient>(_endpoint);

            // Assert
            Assert.Throws<InvalidOperationException>(Add);
        }

        [Fact, IsUnit]
        public void AddAutoRestApi_WithAbstractImplementation_Throws()
        {
            // Act
            void Add() => _provider.AddAutoRestApi<IAuthoRestClient, AbstractAutoRestClient>(_endpoint);

            // Assert
            Assert.Throws<InvalidOperationException>(Add);
        }

        [Fact, IsUnit]
        public void AddAutoRestApi_WithInvalidConstructor_Throws()
        {
            // Act
            void Add() => _provider.AddAutoRestApi<IAuthoRestClient, NoCtorAutoRestClient>(_endpoint);

            // Assert
            Assert.Throws<InvalidOperationException>(Add);
        }

        [Fact, IsUnit]
        public void AddAutoRestApi_WithInvalidProperty_Throws()
        {
            // Act
            void Add() => _provider.AddAutoRestApi<IAuthoRestClient, NoBaseUriAutoRestClient>(_endpoint);

            // Assert
            Assert.Throws<InvalidOperationException>(Add);
        }
        
        #region TestClients

        private interface IAuthoRestClient { }

        private abstract class AbstractAutoRestClient : IAuthoRestClient { }

        private class NoCtorAutoRestClient: IAuthoRestClient { }

        private class NoBaseUriAutoRestClient : NoCtorAutoRestClient
        {
            public NoBaseUriAutoRestClient(ServiceClientCredentials credentials, HttpClient client, bool disposeClient) { }
        }

        private class AutoRestClient : NoBaseUriAutoRestClient
        {
            public Uri BaseUri { get; set; }

            public AutoRestClient(ServiceClientCredentials credentials, HttpClient client, bool disposeClient)
                : base(credentials, client, disposeClient) { }
        }

        #endregion

        #region Provider

        private class MyServiceClientCredentials : ServiceClientCredentials { }

        private class MyServiceProvider: IServiceCollection, IServiceProvider
        {
            #region Not needed

            public IEnumerator<ServiceDescriptor> GetEnumerator()
            {
                var services = Array.Empty<ServiceDescriptor>();
                foreach (var serviceDescriptor in services)
                {
                    yield return serviceDescriptor;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(ServiceDescriptor item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public bool Remove(ServiceDescriptor item)
            {
                throw new NotImplementedException();
            }

            public int Count { get; }
            public bool IsReadOnly { get; }

            public int IndexOf(ServiceDescriptor item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, ServiceDescriptor item)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            public ServiceDescriptor this[int index]
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            #endregion

            private readonly IDictionary<Type, ServiceDescriptor> _services;

            public MyServiceProvider()
            {
                _services = new Dictionary<Type, ServiceDescriptor>();
            }

            public void Add(ServiceDescriptor item)
            {
                if (!_services.ContainsKey(item.ServiceType))
                {
                    _services.Add(item.ServiceType, item);
                }
            }

            public object GetService(Type serviceType)
            {
                var (service, _) = GetServiceWithLifetime(serviceType);
                return service;
            }

            public (object Service, ServiceLifetime LifeTime) GetServiceWithLifetime(Type serviceType)
            {
                var service = _services[serviceType];

                if (service.ImplementationInstance != null)
                    return (service.ImplementationInstance, service.Lifetime);

                return (service.ImplementationFactory(this), service.Lifetime);
            }
        }

        #endregion
    }
}
