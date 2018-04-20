using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Controllers
{
    [Route("healthz")]
    public class HealtController : Controller
    {
        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok(
                "deletion-svc is currently running!"
            );
        }
    }
}