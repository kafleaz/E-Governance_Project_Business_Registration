using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCR_E_gov.Models;
using System.ComponentModel.Design;

namespace OCR_E_gov.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the list of users
        public async Task<IActionResult> UserList()
        {
            var users = await _context.Users
                .Select(u => new User
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Username = u.Username,
                    Password = u.Password
                })
                .ToListAsync();

            return View(users);
        }

        // Display the edit form
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new User
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Username = u.Username,
                    Password = u.Password
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Process the edit form
        [HttpPost]
        public async Task<IActionResult> EditUser(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Username = model.Username;
                user.Password = model.Password;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(UserList));
            }

            return View(model);
        }

        // Display the list of companies
        public async Task<IActionResult> CompanyList()
        {
            var companies = await _context.Company
                .Select(c => new Company_Register_View_Model
                {
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName,
                    Telephone = c.Telephone,
                    FaxNo = c.FaxNo,
                    CompanyEmail = c.CompanyEmail,
                    District = c.District,
                    VDC_Munic = c.VDC_Munic,
                    WardNo = c.WardNo,
                    Street = c.Street,
                    BlockNo = c.BlockNo,
                    Objective = c.Objective,
                    EstablishedDate = c.EstablishedDate,
                    UserId = c.UserId
                })
                .ToListAsync();

            return View(companies);
        }
        
        // Display the edit form for company
        public async Task<IActionResult> EditCompany(int id)
        {
            var company = await _context.Company
            .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (company == null)
            {
                return NotFound();
            }

            var capital = await _context.Capital
                .FirstOrDefaultAsync(c => c.CompanyId == id);
            if (capital == null)
            {
                return NotFound();
            }

            var witness = await _context.Witness
                .FirstOrDefaultAsync(c => c.CompanyId == id);
            if (witness == null)
            {
                return NotFound();
            }

            var viewModel = new Company_Register_display_Model
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                Telephone = company.Telephone,
                FaxNo = company.FaxNo,
                CompanyEmail = company.CompanyEmail,
                District = company.District,
                VDC_Munic = company.VDC_Munic,
                WardNo = company.WardNo,
                Street = company.Street,
                BlockNo = company.BlockNo,
                Objective = company.Objective,
                EstablishedDate = company.EstablishedDate,
                DocumentPath = company.DocumentPath,
                UserId = company.UserId,

                //CompanyType = capital.CompanyType,
                CapitalId = capital.CapitalId,
                CompanyType = capital.CompanyType,
                Capital = capital.Capital,
                Rate = capital.Rate,
                IssuedCapital = capital.IssuedCapital,
                PaidupCapital = capital.PaidupCapital,

                WitnessId = witness.WitnessId,
                WitnessName = witness.WitnessName,
                WitDistrict = witness.WitDistrict,
                WitWardno = witness.WitWardno,
                Citizenship = witness.Citizenship
            };

            return View(viewModel);
        }

        // Process the edit form for company
        // Process the edit form for company
        [HttpPost]
        public async Task<IActionResult> EditCompany(Company_Register_display_Model model)
        {
            if (ModelState.IsValid)
            {
                var company = await _context.Company.FindAsync(model.CompanyId);
                if (company == null)
                {
                    return NotFound();
                }

                var capital = await _context.Capital
                    .FirstOrDefaultAsync(c => c.CompanyId == model.CompanyId);
                if (capital == null)
                {
                    return NotFound();
                }

                var witness = await _context.Witness
                    .FirstOrDefaultAsync(c => c.CompanyId == model.CompanyId);
                if (witness == null)
                {
                    return NotFound();
                }

                // Update company details
                //company.CompanyId = company.CompanyId;
                company.CompanyName = model.CompanyName;
                company.Telephone = model.Telephone;
                company.FaxNo = model.FaxNo;
                company.CompanyEmail = model.CompanyEmail;
                company.District = model.District;
                company.VDC_Munic = model.VDC_Munic;
                company.WardNo = model.WardNo;
                company.Street = model.Street;
                company.BlockNo = model.BlockNo;
                company.Objective = model.Objective;
                company.DocumentPath = company.DocumentPath;
                company.EstablishedDate = DateTime.Now;
                company.UserId = company.UserId;

                // Update capital details
                //capital.CapitalId = capital.CapitalId;
                capital.CompanyType = model.CompanyType;
                capital.Capital = model.Capital;
                capital.Rate = model.Rate;
                capital.IssuedCapital = model.IssuedCapital;
                capital.PaidupCapital = model.PaidupCapital;

                // Update witness details
                //witness.WitnessId = witness.WitnessId;
                witness.WitnessName = model.WitnessName;
                witness.WitDistrict = model.WitDistrict;
                witness.WitWardno = model.WitWardno;
                witness.Citizenship = model.Citizenship;

                await _context.SaveChangesAsync();
                //_context.Company.Add(company);
                //_context.Capital.Add(capital);
                //_context.Witness.Add(witness);
                //await _context.SaveChangesAsync();

                return RedirectToAction(nameof(CompanyList));

                //return RedirectToAction("CompanyList", "Admin");
            }

            return View(model);
        }

    }


}
