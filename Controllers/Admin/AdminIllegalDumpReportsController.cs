using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenTogetheR.Data;
using GreenTogetheR.Models;

namespace GreenTogetheR.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class AdminIllegalDumpReportsController : Controller
    {
        private readonly GreenTogetherContext _context;

        public AdminIllegalDumpReportsController(GreenTogetherContext context)
        {
            _context = context;
        }

        // GET: AdminIllegalDumpReports
        public async Task<IActionResult> Index()
        {
            var greenTogetherContext = _context.IllegalDumpReports.Include(i => i.City).Include(i => i.User);
            return View(await greenTogetherContext.ToListAsync());
        }

        // GET: AdminIllegalDumpReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illegalDumpReport = await _context.IllegalDumpReports
                .Include(i => i.City)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (illegalDumpReport == null)
            {
                return NotFound();
            }

            return View(illegalDumpReport);
        }

        

        private bool IllegalDumpReportExists(int id)
        {
            return _context.IllegalDumpReports.Any(e => e.ReportId == id);
        }

        public async Task<IActionResult> DownloadPdf()
        {
            var reports = await _context.IllegalDumpReports
                .Include(i => i.City)
                .Include(i => i.User)
                .ToListAsync();

            using (var stream = new MemoryStream())
            {
                var doc = new iTextSharp.text.Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, stream);
                doc.Open();

                // Title
                var titleFont = iTextSharp.text.FontFactory.GetFont("Helvetica", 16, iTextSharp.text.Font.BOLD);
                doc.Add(new iTextSharp.text.Paragraph("Illegal Dump Reports", titleFont)
                {
                    Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                    SpacingAfter = 15f
                });

                // Table with columns
                var table = new iTextSharp.text.pdf.PdfPTable(6);
                table.WidthPercentage = 100;

                // Add headers
                string[] headers = { "Reporter", "Description", "Date", "Location", "City", "User" };
                foreach (var header in headers)
                {
                    table.AddCell(new iTextSharp.text.Phrase(header, iTextSharp.text.FontFactory.GetFont("Helvetica", 11, iTextSharp.text.Font.BOLD)));
                }

                // Add rows
                foreach (var r in reports)
                {
                    table.AddCell(r.ReporterName ?? "-");
                    table.AddCell(r.Description ?? "-");
                    table.AddCell(r.ReportDate.ToString("yyyy-MM-dd"));
                    table.AddCell(r.Location ?? "-");
                    table.AddCell(r.City?.CityName ?? "-");
                    table.AddCell(r.User?.FullName ?? "-");
                }

                doc.Add(table);
                doc.Close();

                return File(stream.ToArray(), "application/pdf", "IllegalDumpReports.pdf");
            }
        }
    }
}
