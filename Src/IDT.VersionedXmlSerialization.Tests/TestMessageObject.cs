using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDT.VersionedXmlSerialization.Tests
{
    public interface ITestMessageObjectWInterfaces1
    {
        string Text { get; set; }
    }

    public interface ITestMessageObjectWInterfaces2
    {
        string Reply { get; set; }
    }

    public class TestMessageObjectWInterfaces : ITestMessageObjectWInterfaces1, ITestMessageObjectWInterfaces2
    {
        public string Reply { get; set; }
        public string Text { get; set; }
    }

    public class TestMessageObjectNoInterface
    {
        public string Text { get; set; }
    }
}
