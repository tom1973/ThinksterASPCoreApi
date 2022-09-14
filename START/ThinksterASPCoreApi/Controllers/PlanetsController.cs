using Microsoft.AspNetCore.Mvc;

namespace ThinksterASPCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class PlanetsController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return new { label = "value1", label2 = "value2" };
        }
    }
}
