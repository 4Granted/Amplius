using System.Collections.Generic;
using System.Text;

namespace Amplius.Data.Properties
{
    internal static class PropertiesBuilder
    {
        internal static string Build(Dictionary<string, object> data)
        {
            var builder = new StringBuilder();

            foreach (var node in data)
                builder.Append(BuildNode(node));

            return builder.ToString();
        }

        private static string BuildNode(KeyValuePair<string, object> node)
        {
            var baseNode = $"{node.Key}=";

            var builtNode = $"{baseNode}{BuildValue(node.Value)}\n";

            return builtNode;
        }

        private static string BuildValue(object value)
        {
            var builder = new StringBuilder();

            if (value is IEnumerable<object> values)
            {
                builder.Append("[");
                var newValues = new List<string>();
                foreach (var val in values) newValues.Add(BuildValue(val));
                builder.Append(string.Join(", ", newValues));
                builder.Append("]");
            } else if (value is string str)
            {
                builder.Append("\"");
                builder.Append(str);
                builder.Append("\"");
            } else
                builder.Append(value.ToString());

            return builder.ToString();
        }
    }
}
