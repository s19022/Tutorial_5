using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial_5.DTOs.Requests;
using Tutorial_5.Services;

namespace Tutorial_5.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IStudentServiceDb _service;

        public EnrollmentController(IStudentServiceDb db)
        {
            _service = db;
        }

        [HttpPut]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            
           return Ok(_service.EnrollStudent(request));

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("GG");
        }
    }
}