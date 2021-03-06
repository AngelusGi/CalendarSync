using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Company.Function
{
    public static class SyncCalendars
    {
        [FunctionName("Sync")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request, ILogger log, ExecutionContext context)
        {

            var config = new ConfigurationBuilder().SetBasePath(context.FunctionAppDirectory).AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();

            var fistAccount = config["FirstAccount"];

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var end = GetDate(out var start);

            string responseMessage = $"Hello. Today is {start}. One week after {end}";

            return new OkObjectResult(responseMessage);
        }


        private static DateTime GetDate(out DateTime today, double daySpan = 7.00)
        {
            today = DateTime.Now.ToLocalTime();
            var end = today.AddDays(daySpan);
            return end;
        }

        private static Object GetCalendar()
        {
            throw new NotImplementedException();
        }

        private static void CompareEvents()
        {
            throw new NotImplementedException();
        }

        private static int SyncEvents()
        {
            throw new NotImplementedException();
        }

    }
}
