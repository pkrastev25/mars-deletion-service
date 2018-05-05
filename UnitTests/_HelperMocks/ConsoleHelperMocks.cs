using System;
using System.IO;

namespace UnitTests._HelperMocks
{
    public class ConsoleHelperMocks : IDisposable
    {
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalOutput;

        public ConsoleHelperMocks(
            TextWriter textWriter
        )
        {
            _stringWriter = new StringWriter();
            _originalOutput = textWriter;

            if (textWriter == Console.Out)
            {
                Console.SetOut(_stringWriter);
            }
            else if (textWriter == Console.Error)
            {
                Console.SetError(_stringWriter);
            }
        }

        public string GetOuput()
        {
            return _stringWriter.ToString();
        }

        public void Dispose()
        {
            if (_originalOutput == Console.Out)
            {
                Console.SetOut(_originalOutput);
            }
            else if (_originalOutput == Console.Error)
            {
                Console.SetError(_originalOutput);
            }

            _stringWriter.Dispose();
        }
    }
}