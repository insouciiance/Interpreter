using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class SISharpReader : IDisposable
    {
        private readonly StreamReader _reader;

        public SISharpReader(string path) => _reader = new StreamReader(path);

        public async Task<double> ExecuteAsync()
        {
            string code = await _reader.ReadToEndAsync();
            string[] lines = code.Split(Environment.NewLine);

            SISharpInterpreter interpreter = new ();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                interpreter.Execute(lines[i]);
            }

            return interpreter.Execute(lines[^1]);
        }

        public void Dispose() => _reader.Dispose();
    }
}
