using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaparaThirdWeek.Services.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
using static PaparaThirdWeek.Services.DTOs.FakeUserDto;
using Microsoft.Extensions.Configuration;
using PaparaThirdWeek.Services.Abstracts;

namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        
    }
}
