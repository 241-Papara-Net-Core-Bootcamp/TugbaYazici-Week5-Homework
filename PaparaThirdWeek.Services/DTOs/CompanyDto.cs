using PaparaThirdWeek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.DTOs
{
    public class CompanyDto 
    {
        [Required(ErrorMessage ="Şirket ismi zorunludur.")]
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        [StringLength(13,MinimumLength =13,ErrorMessage ="TaxNumber 13 karakterden az olamaz!")]
        public string TaxNumber { get; set; }
        public string Email { get; set; }
        
    }
}
