using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzFnCRUDSample.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AzFnCRUDSample
{
    public class SaveItemTypeFn
    {
        private readonly ILogger<SaveItemTypeFn> _logger;

        public SaveItemTypeFn(ILogger<SaveItemTypeFn> log)
        {
            _logger = log;
        }

        [FunctionName("SaveItemTypeFn")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Mess" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] ItemType body)
        {
            
            return new OkObjectResult(body);
        }
    }
}

