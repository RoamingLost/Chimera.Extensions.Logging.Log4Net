namespace Chimera.Extensions.Logging.Log4Net.Tests.Configuration
{
    internal static class Resources
    {
        internal static string FormatError_JSONParseError(int lineNumber, string errorLine)
        {
            return $"Could not parse the JSON file. Error on line number '{lineNumber}': '{errorLine}'.";
        }

        internal static string FormatError_KeyIsDuplicated(string key)
        {
            return $"A duplicate key '{key}' was found.";
        }

        internal static string FormatError_UnsupportedJSONToken(object tokenType, string path, int lineNumber, int linePosition)
        {
            return $"Unsupported JSON token '{tokenType}' was found. Path '{path}', line {lineNumber} position {linePosition}.";
        }
    }
}
