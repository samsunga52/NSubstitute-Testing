using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            // Create test doubles
            var source = Substitute.For<ISource>();
            var destination = Substitute.For<IDestination>();

            // Set up behavior for the test doubles
            source.ReadChar().Returns('H', 'e', 'l', 'l', 'o', '\n');
            source.ReadChars(5).Returns("World");

            // Create an instance of the Copier class with the test doubles
            var copier = new Copier(source, destination);

            // Call the Copy method
            copier.Copy();

            // Verify that the expected behavior occurred
            destination.Received(5).WriteChar(Arg.Any<char>());

            // Call the CopyMultiple method
            copier.CopyMultiple(5);

            // Verify that the expected behavior occurred
            destination.Received().WriteChars(Arg.Is<char[]>(chars => chars.Length == 5));

        }
    }
    public interface ISource
    {
        char ReadChar();
        string ReadChars(int count);
    }

    public interface IDestination
    {
        void WriteChar(char c);
        void WriteChars(char[] values);
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

        public void CopyMultiple(int count)
        {
            string chars = _source.ReadChars(count);
            _destination.WriteChars(chars.ToCharArray());
        }
    }

}
