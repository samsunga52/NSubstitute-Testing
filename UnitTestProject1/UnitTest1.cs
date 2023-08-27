using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ISource source = Substitute.For<ISource>();
            IDestination destination = Substitute.For<IDestination>();

            // Set up the source to return characters
            source.ReadChar().Returns('H', 'e', 'l', 'l', 'o', '\n');

            Copier copier = new Copier(source, destination);
            copier.Copy();

            // Verify that the destination received the expected characters
            destination.Received().WriteChar('H');
            destination.Received().WriteChar('e');
            destination.Received().WriteChar('l');
            destination.Received().WriteChar('l');
            destination.Received().WriteChar('o');
        }
    }

    public interface ISource
    {
        char ReadChar();
    }

    public interface IDestination
    {
        void WriteChar(char c);
    }

    public class Copier
    {
        private readonly ISource _source;
        private readonly IDestination _destination;

        public Copier(ISource source, IDestination destination)
        {
            _source = source;
            _destination = destination;
        }

        public void Copy()
        {
            char c;
            while ((c = _source.ReadChar()) != '\n')
            {
                _destination.WriteChar(c);
            }
        }
    }
}
