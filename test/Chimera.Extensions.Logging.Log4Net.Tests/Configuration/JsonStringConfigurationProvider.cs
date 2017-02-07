namespace Chimera.Extensions.Logging.Log4Net.Tests.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    /// <summary>
    /// A JSON string based <see cref="ConfigurationProvider"/>.
    /// </summary>
    /// <remarks>Based on <a href="https://github.com/aspnet/Configuration/blob/dev/src/Microsoft.Extensions.Configuration.Json/JsonConfigurationProvider.cs">JsonConfigurationProvider</a>.</remarks>
    /// <seealso cref="Microsoft.Extensions.Configuration.ConfigurationProvider" />
    /// <seealso href="https://github.com/aspnet/Configuration/tree/dev/src/Microsoft.Extensions.Configuration.Json">Microsoft.Extensions.Configuration.Json</seealso>
    public class JsonStringConfigurationProvider : ConfigurationProvider
    {
        private string _json;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringConfigurationProvider"/> class.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        public JsonStringConfigurationProvider(string json)
        {
            _json = json;
        }

        /// <summary>
        /// Loads (or reloads) the data for this provider.
        /// </summary>
        public override void Load()
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(_json);
            using (var stream = new MemoryStream(bytes))
            {
                Load(stream);
            }
        }

        // signature modified from source, body unmodified
        private void Load(Stream stream)
        {
            var parser = new JsonConfigurationFileParser();
            try
            {
                Data = parser.Parse(stream);
            }
            catch (JsonReaderException e)
            {
                string errorLine = string.Empty;
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    IEnumerable<string> fileContent;
                    using (var streamReader = new StreamReader(stream))
                    {
                        fileContent = ReadLines(streamReader);
                        errorLine = RetrieveErrorContext(e, fileContent);
                    }
                }

                throw new FormatException(Resources.FormatError_JSONParseError(e.LineNumber, errorLine), e);
            }
        }

        // unmodified from source
        private static string RetrieveErrorContext(JsonReaderException e, IEnumerable<string> fileContent)
        {
            string errorLine = null;
            if (e.LineNumber >= 2)
            {
                var errorContext = fileContent.Skip(e.LineNumber - 2).Take(2).ToList();
                // Handle situations when the line number reported is out of bounds
                if (errorContext.Count() >= 2)
                {
                    errorLine = errorContext[0].Trim() + Environment.NewLine + errorContext[1].Trim();
                }
            }
            if (string.IsNullOrEmpty(errorLine))
            {
                var possibleLineContent = fileContent.Skip(e.LineNumber - 1).FirstOrDefault();
                errorLine = possibleLineContent ?? string.Empty;
            }
            return errorLine;
        }

        // unmodified from source
        private static IEnumerable<string> ReadLines(StreamReader streamReader)
        {
            string line;
            do
            {
                line = streamReader.ReadLine();
                yield return line;
            } while (line != null);
        }
    }
}
