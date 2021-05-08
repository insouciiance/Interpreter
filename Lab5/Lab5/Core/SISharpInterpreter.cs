using System.Text.RegularExpressions;

namespace Lab5
{
    internal class SISharpInterpreter
    {
        private readonly SISharpParser _parser = new();

        public static readonly VariableStorage Storage = new();

        public double Execute(string code)
        {
            string[] lines = code.Split(';');
            foreach (string line in lines)
            {
                string normalizedLine = Regex.Replace(line, @"\s+", "");

                if (normalizedLine == string.Empty) continue;

                _parser.ParseLine(normalizedLine);
            }

            _parser.Head.DebugPrint(0);

            return _parser.Head.Traverse();
        }
    }
}