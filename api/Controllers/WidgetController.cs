using api.BusinessLogic;
using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetBusinessLogic _widgetBusinessLogic;

        public WidgetController(IWidgetBusinessLogic widgetBusinessLogic)
        {
            _widgetBusinessLogic = widgetBusinessLogic;
        }

        [HttpPost("GetWidgets")]
        public IActionResult GetWidgets([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "userId", typeof(int) }
                });

                int userID = (int)parameters["userId"];

                List<Widget> widgets = _widgetBusinessLogic.GetWidgets(userID);

                return Ok(widgets);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveWidget")]
        public IActionResult RemoveWidget([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "widgetId", typeof(int) }
                });
                int widgetId = (int)parameters["widgetId"];

                bool isDeleted = _widgetBusinessLogic.RemoveWidget(widgetId);

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateWidget")]
        public IActionResult AddOrUpdateWidget([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "widgetId", typeof(int) },
                    { "userId", typeof(int) },
                    { "title", typeof(string) },
                    { "symbol", typeof(string) },
                    { "isLeading", typeof(bool) },
                    { "infoType", typeof(int) },
                    { "type", typeof(int) },
                    { "dateFrom", typeof(string) },
                    { "dateTo", typeof(string) },
                    { "dateFromType", typeof(int) },
                    { "dateToType", typeof(int) },
                    { "position", typeof(int) },
                    { "sizeX", typeof(int) },
                    { "sizeY", typeof(int) },
                    { "bgColor", typeof(string) }
                });

                Widget widget = new Widget();

                widget.Id = (int)parameters["widgetId"];
                widget.UserID = (int)parameters["userId"];
                widget.Title = parameters["title"].ToString();
                widget.Symbol = parameters["symbol"].ToString();
                widget.IsLeading = (bool)parameters["isLeading"];
                widget.InfoType = (WidgetInfoType)(int)parameters["infoType"];
                widget.Type = (WidgetType)(int)parameters["type"];
                widget.DateFrom = parameters["dateFrom"].ToString();
                widget.DateTo = parameters["dateTo"].ToString();
                widget.DateFromType = (DateType)(int)parameters["dateFromType"];
                widget.DateToType = (DateType)(int)parameters["dateToType"];
                widget.Position = (int)parameters["position"];
                widget.SizeX = (int)parameters["sizeX"];
                widget.SizeY = (int)parameters["sizeY"];
                widget.BgColor = parameters["bgColor"].ToString();

                int result = _widgetBusinessLogic.AddOrUpdateWidget(widget);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetWidgetData")]
        public IActionResult GetWidgetData([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "widgetID", typeof(int) },
                    { "storeID", typeof(int) },
                    { "type", typeof(int) }
                });
                int widgetID = (int)parameters["widgetID"];
                int storeID = (int)parameters["storeID"];
                WidgetType type = (WidgetType)(int)parameters["type"];

                if (type == WidgetType.Kpi)
                {
                    int result = _widgetBusinessLogic.GetWidgetData(widgetID, storeID);
                    return Ok(result);
                }
                else 
                {
                    DataTable data = _widgetBusinessLogic.GetWidgetDataList(widgetID, storeID);
                    return Ok(JsonConvert.SerializeObject(data));
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateWidgetPositions")]
        public IActionResult UpdateWidgetPositions([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "userID", typeof(int) },
                    { "widgetIDs", typeof(string) }
                });
                int userID = (int)parameters["userID"];
                string widgetIDs = (string)parameters["widgetIDs"];

                _widgetBusinessLogic.UpdateWidgetPositions(userID, widgetIDs);

                return Ok("ok");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetWidgetTypeList")]
        public IActionResult GetWidgetTypeList()
        {
            try
            {
                DataTable widgetTypeList = _widgetBusinessLogic.GetWidgetTypeList();
                DataTable widgetInfoTypeList = _widgetBusinessLogic.GetWidgetInfoTypeList();

                return Ok(JsonConvert.SerializeObject(new { widgetTypeList, widgetInfoTypeList }));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
