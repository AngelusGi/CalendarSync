using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class HttpTriggerCSharp1
    {
        [FunctionName("SyncCalendars")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,"get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
           
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var end = GetDate(out var start);

            string responseMessage = $"Hello. Today is {start}. One week after {end}";

            return new OkObjectResult(responseMessage);
        }



        private static DateTime GetDate(out DateTime today, double daySpan = 7.00){
            today = DateTime.Now.ToLocalTime();
            var end = today.AddDays(daySpan);
            return end;
        }



        
    }
}
