using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace FileUploadSample.APIs {

    public class FileResult {

        public IEnumerable<string> FileNames { get; set; }
        public string Submitter { get; set; }
    }

    public class UploadController : ApiController {

        public async Task<FileResult> Post() {

            //Check whether it is an HTML form file upload request
            if (!Request.Content.IsMimeMultipartContent("form-data")) {

                //return UnsupportedMediaType response back if not
                throw new HttpResponseException(
                    new HttpResponseMessage(
                        HttpStatusCode.UnsupportedMediaType)
                );
            }

            //Determine the upload path
            var uploadPath = HttpContext.Current.Server.MapPath("~/Files");

            var multipartFormDataStreamProvider =
                new CustomMultipartFormDataStreamProvider(uploadPath);

            // Read the MIME multipart asynchronously 
            // content using the stream provider we just created.
            await Request.Content.ReadAsMultipartAsync(
                multipartFormDataStreamProvider);

            // Create response
            return new FileResult {

                FileNames = 
                    multipartFormDataStreamProvider
                    .FileData.Select(
                        entry => entry.LocalFileName),

                Submitter = 
                    multipartFormDataStreamProvider
                    .FormData["submitter"]
            };
        }
    }

    public class CustomMultipartFormDataStreamProvider 
        : MultipartFormDataStreamProvider {

        public CustomMultipartFormDataStreamProvider(
            string rootPath) : base(rootPath) { }

        public override string GetLocalFileName(
            HttpContentHeaders headers) {

            if (headers != null && 
                headers.ContentDisposition != null) {

                return headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"');
            }

            return base.GetLocalFileName(headers);
        }
    }
}