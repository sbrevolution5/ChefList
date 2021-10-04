using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MasterMealWA.Server.CustomErrors
{
    public class DuplicateFound : IHttpActionResult
    {
        private string message;

        public DuplicateFound(string message)
        {
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent(message);
            return Task.FromResult(response);
        }
    }
}
