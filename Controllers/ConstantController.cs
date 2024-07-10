using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class ConstantController(IConstantRepository constantRepository) : ControllerBase
    {
        private readonly IConstantRepository _constantRepository = constantRepository;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _constantRepository.GetCategories());
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            return Ok(await _constantRepository.GetTags());
        }
    }
}
