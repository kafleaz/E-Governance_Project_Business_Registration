using Microsoft.AspNetCore.Mvc;
using OCR_E_gov.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OCR_E_gov.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, ILogger<CompanyController> logger)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;

            var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "documents");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
        }

        public IActionResult Create()
        {
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company_Register_View_Model model, IFormFile documentPath)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                if (!userId.HasValue)
                {
                    // Handle case where userId is null
                    _logger.LogError("User ID is null.");
                    return RedirectToAction("Login", "Account");
                }

                try
                {
                    // Save to Company_Table
                    var company = new Company_Table
                    {
                        UserId = userId.Value,
                        CompanyName = model.CompanyName,
                        Telephone = model.Telephone,
                        FaxNo = model.FaxNo,
                        CompanyEmail = model.CompanyEmail,
                        District = model.District,
                        VDC_Munic = model.VDC_Munic,
                        WardNo = model.WardNo,
                        Street = model.Street,
                        BlockNo = model.BlockNo,
                        Objective = model.Objective,
                        EstablishedDate = DateTime.Now
                    };

                    if (documentPath != null && documentPath.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "documents");
                        var fileName = Path.GetFileName(documentPath.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        try
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await documentPath.CopyToAsync(fileStream);
                            }
                            company.DocumentPath = $"/documents/{fileName}";
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Error saving file: {ex.Message}");
                        }
                    }

                    _context.Company.Add(company);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Company_Table record saved successfully.");

                    // Save to CapitalDetails
                    var capital = new Capital_Table
                    {
                        CompanyId = company.CompanyId,
                        CompanyType = model.CompanyType,
                        Capital = model.Capital,
                        Rate = model.Rate,
                        IssuedCapital = model.IssuedCapital,
                        PaidupCapital = model.PaidupCapital
                    };
                    _context.Capital.Add(capital);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Capital_Table record saved successfully.");

                    // Save to WitnessDetails
                    var witness = new Witness_Table
                    {
                        CompanyId = company.CompanyId,
                        WitnessName = model.WitnessName,
                        WitDistrict = model.WitDistrict,
                        WitWardno = model.WitWardno,
                        Citizenship = model.Citizenship
                    };
                    _context.Witness.Add(witness);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Witness_Table record saved successfully.");

                    return RedirectToAction("Index", "Company");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error saving data: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving the data. Please try again.");
                }
            }

            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            return View(model);
        }
    }
}
