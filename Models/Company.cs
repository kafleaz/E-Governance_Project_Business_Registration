using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCR_E_gov.Models
{
    public class Company_Table
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Telephone { get; set; }
        public string FaxNo { get; set; }
        public string CompanyEmail { get; set; }
        public string District { get; set; }
        public string VDC_Munic { get; set; }
        public int WardNo { get; set; }
        public string Street { get; set; }
        public string BlockNo { get; set; }
        public string Objective { get; set; }
        public string DocumentPath { get; set; }
        //public IFormFile DocumentPath { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class Capital_Table
    {
        [Key]
        public int CapitalId { get; set;}
        [ForeignKey("Company_Table")]
        public int CompanyId { get; set; }
        public string CompanyType { get; set;}
        public decimal Capital { get; set;}
        public decimal Rate { get; set;}
        public decimal IssuedCapital { get; set; }
        public decimal PaidupCapital { get; set;}
        public Company_Table Company_Table { get; set; }
    }

    public class Witness_Table
    {
        [Key]
        public int WitnessId { get; set; }
        [ForeignKey("Company_Table")]
        public int CompanyId { get; set; }
        public string WitnessName { get; set; }
        public string WitDistrict { get; set; }
        public int WitWardno { get; set; }
        public string Citizenship { get; set; }
        public Company_Table Company_Table { get; set; }
    }

    public class Company_Register_View_Model
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Telephone { get; set; }
        public string FaxNo { get; set; }
        public string CompanyEmail { get; set; }
        public string District { get; set; }
        public string VDC_Munic { get; set; }
        public int WardNo { get; set; }
        public string Street { get; set; }
        public string BlockNo { get; set; }
        public string Objective { get; set; }
        public IFormFile DocumentPath { get; set; }
        //public string DocumentPath { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int UserId { get; set; }

        public int CapitalId { get; set; }
        public string CompanyType { get; set; }
        public decimal Capital { get; set; }
        public decimal Rate { get; set; }
        public decimal IssuedCapital { get; set; }
        public decimal PaidupCapital { get; set; }

        public int WitnessId { get; set; }
        public string WitnessName { get; set; }
        public string WitDistrict { get; set; }
        public int WitWardno { get; set; }
        public string Citizenship { get; set;}

    }

    public class Company_Register_display_Model
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Telephone { get; set; }
        public string FaxNo { get; set; }
        public string CompanyEmail { get; set; }
        public string District { get; set; }
        public string VDC_Munic { get; set; }
        public int WardNo { get; set; }
        public string Street { get; set; }
        public string BlockNo { get; set; }
        public string Objective { get; set; }
        //public IFormFile DocumentPath { get; set; }
        public string DocumentPath { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int UserId { get; set; }

        public int CapitalId { get; set; }
        public string CompanyType { get; set; }
        public decimal Capital { get; set; }
        public decimal Rate { get; set; }
        public decimal IssuedCapital { get; set; }
        public decimal PaidupCapital { get; set; }

        public int WitnessId { get; set; }
        public string WitnessName { get; set; }
        public string WitDistrict { get; set; }
        public int WitWardno { get; set; }
        public string Citizenship { get; set; }

    }
}
