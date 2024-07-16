using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzFnCRUDSample.Service.Menu;
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
    public class GetMenu
    {
        private readonly ILogger<GetMenu> _logger;
        private readonly IMenuService _menuService;

        public GetMenu(ILogger<GetMenu> log, IMenuService menuService)
        {
            _logger = log;
            _menuService = menuService;
        }

        [FunctionName("GetMenu")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = await _menuService.GetItemTypesAsync();
            return new OkObjectResult(response);
        }
    }
}

