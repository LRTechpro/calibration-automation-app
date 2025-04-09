using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ReportsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet] // ✅ Fix: This properly maps GET /api/reports
        public async Task<IActionResult> Get()
        {
            var reports = await _db.Reports.OrderByDescending(r => r.Date).ToListAsync();
            return Ok(reports);
        }

        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var report = await _db.Reports.FindAsync(id);
            if (report == null)
            return NotFound();

        var pdfBytes = ReportPdfGenerator.Generate(report);

    // Log PDF info
    Console.WriteLine($"✅ PDF Generated for VIN: {report.VIN}, Size: {pdfBytes.Length} bytes");

    return File(pdfBytes, "application/pdf", $"report_{id}.pdf");
}


        [HttpPost] // POST /api/reports
        public async Task<IActionResult> Post([FromBody] Report report)
        {
            report.Date = report.Date == DateTime.MinValue ? DateTime.Now : report.Date;
            _db.Reports.Add(report);
            await _db.SaveChangesAsync();

            return Ok(report);
        }
    }
}


