using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using DevOpsFlex.Core;
using DevOpsFlex.Tests.Core;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public class DiscoveryExtensionsTest
{
    public class GetConnectors
    {
        [Fact, IsUnit]
        public void Test_GetSinglePull()
        {
            var result = new[] { GetType().Assembly }.GetConnectors<IPullTelemetry>().ToList();
            result.Should().ContainSingle(c => c.GetType() == typeof(TestPullConnector));
        }

        [Fact, IsUnit]
        public void Test_GetSinglePush()
        {
            var result = new[] { GetType().Assembly }.GetConnectors<IPushTelemetry>().ToList();
            result.Should().ContainSingle(c => c.GetType() == typeof(TestPushConnector));
        }

        [Fact, IsUnit]
        public void Ensure_GetSinglePush_InvalidCtor_Throws()
        {
            using (var ms = new MemoryStream())
            {
                var code = CSharpSyntaxTree.ParseText($@"
                    using System;
                    using DevOpsFlex.Core;

                    namespace {nameof(DevOpsFlex)}.{nameof(DiscoveryExtensionsTest)}.InvalidConnector
                    {{
                        public class {nameof(DiscoveryExtensionsTest)}InvalidConnector  : IPushTelemetry
                        {{
                            public {nameof(DiscoveryExtensionsTest)}InvalidConnector(string badParam)
                            {{
                            }}

                            public IObservable<BbEvent> Connect()
                            {{
                                throw new NotImplementedException();
                            }}
                        }}
                    }}");


                string refs = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") as string;

                List<MetadataReference> references = new List<MetadataReference>();
                foreach (var path in refs.Split(Path.PathSeparator))
                {
                    references.Add(MetadataReference.CreateFromFile(path));
                }

                var compilation = CSharpCompilation.Create(
                    $"{nameof(DevOpsFlex)}.{nameof(DiscoveryExtensionsTest)}.InvalidConnector",
                    new[] { code },
                    references,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));



                var compilationResult = compilation.Emit(ms);
                Assert.True(compilationResult.Success, $"Assembly generation failed, inspect {nameof(compilationResult)}.Diagnostics");

                ms.Seek(0, SeekOrigin.Begin);
                var asm = Assembly.Load(ms.ToArray());

                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                Action act = () => new[] { asm }.GetConnectors<IPushTelemetry>().ToList();
                act.ShouldThrow<InvalidOperationException>();
            }
        }
    }
}

public class TestPullConnector : IPullTelemetry
{
    public IObserver<BbEvent> Connect()
    {
        return Observer.Create<BbEvent>(e => { });
    }
}

public class TestPushConnector : IPushTelemetry
{
    public IObservable<BbEvent> Connect()
    {
        return Observable.Empty<BbEvent>();
    }
}
