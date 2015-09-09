using System;
using System.Linq;
using NUnit.Framework;

namespace IDT.VersionedXmlSerialization.Tests.TestCases
{
    class ProxyInterfaceTypeBuilderTests
    {
        [Test]
        public void CreateProxyTypeTest()
        {
            Type interfaceType = typeof(ITestMessageObjectWInterfaces1);

            var interfaceTypeBuilder = new ProxyInterfaceTypeBuilder();
            Type newClassType = interfaceTypeBuilder.GetProxyType(interfaceType);

            Assert.False(newClassType.IsInterface);
            Assert.True(newClassType.GetInterfaces().Contains(interfaceType));
        }

        [Test]
        public void CreateProxyTypeTwiceTest()
        {
            Type interfaceType = typeof(ITestMessageObjectWInterfaces1);

            var interfaceTypeBuilder = new ProxyInterfaceTypeBuilder();
            Type newClassType = interfaceTypeBuilder.GetProxyType(interfaceType);
            Type newClassType2 = interfaceTypeBuilder.GetProxyType(interfaceType);

            Assert.False(newClassType.IsInterface);
            Assert.True(newClassType.GetInterfaces().Contains(interfaceType));
            Assert.That(newClassType, Is.EqualTo(newClassType2));
        }

        [Test]
        public void ThrowsExceptionIfNotAnInterfaceTest()
        {
            Type interfaceType = typeof(TestMessageObjectNoInterface);

            var interfaceTypeBuilder = new ProxyInterfaceTypeBuilder();

            Assert.Throws<ArgumentException>(
                () =>
                interfaceTypeBuilder.GetProxyType(interfaceType));
        }
    }
}
