using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace SCAPI.MessageHandlers
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private string APIKey = System.Configuration.ConfigurationManager.AppSettings["APIKey"];

        /// <summary>
        /// Checks for the incoming request headers if it contains the required APIKey.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// If a valid APIKey is receied, then the requests are forwarded for further processing.
        /// Else, HTTP error indicating Forbidden access is returned.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            bool validKey = false;

            IEnumerable<string> requestHeaders;
            var checkAPIKeyExists = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);
            if (checkAPIKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(APIKey))
                    validKey = true;
            }

            if (!validKey)
            {
                return httpRequestMessage.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid API Key");

            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);
            return response;
        }
    }
}