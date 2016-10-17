using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpContentSample {
    public class CompressedContent : HttpContent {
        private readonly HttpContent _initialContent;
        private readonly string _contentEncoding;

        public CompressedContent(HttpContent content, string contentEncoding) {
            if (content == null) {
                throw new ArgumentNullException("content");
            }

            if (contentEncoding == null) {
                throw new ArgumentNullException("Content-Encoding");
            }

            _initialContent = content;
            _contentEncoding = contentEncoding.ToLowerInvariant();

            if (_contentEncoding != "gzip" && this._contentEncoding != "deflate") {
                throw new InvalidOperationException(string.Format(
                    "Encoding '{0}' is not supported. Only gzip or deflate encoding supported.",
                        _contentEncoding));
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in 
                _initialContent.Headers.Where(header => header.Key != "Content-Length")) {
                Headers.Add(header.Key, header.Value);
            }

            Headers.ContentEncoding.Add(contentEncoding);
        }

        protected override bool TryComputeLength(out long length) {
            length = -1;
            return false;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context) {
            Stream compressedStream = null;

            switch (_contentEncoding) {
                case "gzip":
                    compressedStream = 
                        new GZipStream(stream, CompressionMode.Compress, leaveOpen: true);
                    break;
                case "deflate":
                    compressedStream =
                        new DeflateStream(stream, CompressionMode.Compress, leaveOpen: true);
                    break;
            }

            return _initialContent.CopyToAsync(compressedStream).ContinueWith(tsk => {
                if (compressedStream != null) {
                    compressedStream.Dispose();
                }
            });
        }
    }
}