using System.Collections.Generic;
using System.Linq;

namespace Amplius.Data.Properties
{
    public static class Properties
    {
        public static Dictionary<string, object> Unmarshal(string data)
        {
            var lexer = new PLexer(data);
            var tokens = lexer.LexAll();

            var parser = new PParser(tokens);
            var results = parser.ParseAll();

            return results.ToDictionary(result => result.Key, result => result.Value);
        }

        public static string Marshal(Dictionary<string, object> data) => PropertiesBuilder.Build(data);
    }
}
