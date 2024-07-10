using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(IImageService imageService) : ControllerBase
    {
        private readonly IImageService _imageService = imageService;

        [HttpPost]
        [Route("bookCoverImage")]
        public async Task<IActionResult> createCoverImage([FromForm] IFormFile imageFile)
        {
            var responce = await _imageService.SaveImageAsync(imageFile);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce.Error!.Message);
        }
    }
}
