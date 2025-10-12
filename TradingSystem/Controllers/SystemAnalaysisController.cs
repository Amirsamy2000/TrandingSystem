using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrandingSystem.Application.Features.analysis.ProcedureExe;

namespace TrandingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemAnalaysisController : Controller
    {
        private readonly ISystemAnalysisService _analysisService;

        public SystemAnalaysisController(ISystemAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        public async Task<IActionResult> Index(DateTime? startDate = null, DateTime? endDate = null)
        {
            var data = await _analysisService.GetDashboardDataAsync(startDate, endDate);
            return View(data);
        }

        // AJAX endpoint for refreshing data
        [HttpGet]
        public async Task<JsonResult> GetDashboardData(DateTime? startDate = null, DateTime? endDate = null)
        {
            var data = await _analysisService.GetDashboardDataAsync(startDate, endDate);
            return Json(data);
        }

    }
}
