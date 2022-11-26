using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Abstracts
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        public void Add(Company company);
        public void Update(Company company);
        public void Delete(Company company);
    }
}
