using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using AutoMapper;
using PaparaThirdWeek.Api.Filters;
using Microsoft.Extensions.Caching.Memory;
using PaparaThirdWeek.Services.Concretes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PaparaThirdWeek.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;
        private readonly string cacheKey = $"{typeof(CompanyController)}";


        public CompanyController(ICompanyService companyService, IMapper mapper = null, IMemoryCache memoryCache = null, ICacheService cacheService = null)
        {
            this.companyService = companyService;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        
        public async Task<IActionResult> Post(CompanyDto company)
        {            
            var mappedCompany= mapper.Map<Company>(company);
            mappedCompany.LastUpdateAt = DateTime.Now;
            companyService.Add(mappedCompany);
            cacheService.Remove(cacheKey);

            return Ok();
        }
        [HttpGet]
        [Route("Filter")]
        [TypeFilter(typeof(HttpResponseExceptionFilter))]

        public IActionResult Filter()
        {
            throw new HttpResponseException("Custom Exception Filter");
        }



        [HttpGet("Companies")]
        [Log]
        [ResponseCache(CacheProfileName = "Duration45")]
        public IActionResult Get()
        {
            var result = companyService.GetAll();
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(Company company) 
        {
            companyService.Delete(company);
            return Ok();

        }

        [HttpPut]
        public IActionResult Put(Company company)
        {
            companyService.Update(company);
            return Ok();
        }
     
    }
}
