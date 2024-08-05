using api.BusinessLogic;
using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PrinterController : ControllerBase
    {
        private readonly IPrinterBusinessLogic _printerBusinessLogic;

        public PrinterController(IPrinterBusinessLogic printerBusinessLogic)
        {
            _printerBusinessLogic = printerBusinessLogic;
        }

        [HttpPost("GetPrinters")]
        public IActionResult GetPrinters([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) }
                });

                int storeID = (int)parameters["storeID"];

                List<Printer> printers = _printerBusinessLogic.GetPrinters(storeID);

                return Ok(printers);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemovePrinter")]
        public IActionResult RemovePrinter([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "printerID", typeof(int) }
                });
                int printerID = (int)parameters["printerID"];

                bool isDeleted = _printerBusinessLogic.RemovePrinter(printerID);

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdatePrinter")]
        public async Task<IActionResult> AddOrUpdatePrinter([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "id", typeof(int) },
                    { "name", typeof(string) },
                    { "ip", typeof(string) },
                    { "isPrincipal", typeof(int) },
                    { "storeID", typeof(int) }
                });

                Printer printer = new Printer();

                printer.Id = (int)parameters["id"];
                printer.Name = (string)parameters["name"];
                printer.Ip = (string)parameters["ip"];
                printer.IsPrincipal = (bool)parameters["isPrincipal"];
                printer.StoreID = (int)parameters["storeID"];

                int result = _printerBusinessLogic.AddOrUpdatePrinter(printer);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
