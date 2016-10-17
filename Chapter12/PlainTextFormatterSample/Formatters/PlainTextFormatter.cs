using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace PlainTextFormatterSample.Formatters {
    public class PlainTextFormatter : BufferedMediaTypeFormatter {
        public PlainTextFormatter() {
            // Set supported media type
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

            // Set default supported character encodings
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
            SupportedEncodings.Add(new UnicodeEncoding(bigEndian: false, byteOrderMark: true, throwOnInvalidBytes: true));
        }

        public override bool CanReadType(Type type) {
            return type == typeof (string);
        }

        public override bool CanWriteType(Type type) {
            return type == typeof (string);
        }

        public override object ReadFromStream(Type type, Stream stream, HttpContent content,
                                              IFormatterLogger formatterLogger) {
            Encoding selectedEncoding = SelectCharacterEncoding(content.Headers);
            using (var reader = new StreamReader(stream, selectedEncoding)) {
                return reader.ReadToEnd();
            }
        }

        public override void WriteToStream(Type type, object value, Stream stream, HttpContent content) {
            Encoding selectedEncoding = SelectCharacterEncoding(content.Headers);
            using (var writer = new StreamWriter(stream, selectedEncoding)) {
                writer.Write(value);
            }
        }
    }
}