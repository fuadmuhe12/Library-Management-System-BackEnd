using Library_Management_System_BackEnd.Entities.Dtos.AuthorDto;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IAuthorRepository authorRepository) : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        const string _GetAuthorPath = "GetAuthorById";

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAuthors([FromQuery] AuthorQuery query)
        {
            var authors = await _authorRepository.GetAllAuthors(query);
            Log.Information("GetAllAuthors method in AuthorController class was called");
            return Ok(authors);
        }

        [HttpGet]
        [Route("{id}", Name = _GetAuthorPath)]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                Log.Warning($"Author with id {id} not found");
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAuthor([FromBody] AuthorRegisterDto authorDto)
        {
            Author author = authorDto.ToAuthorFromAuthorRegisterDto();
            await _authorRepository.AddAuthor(author);
            Log.Information("New author registered with  {@author}", author);

            return CreatedAtRoute(_GetAuthorPath, new { id = author.AuthorId }, author);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAuthor(
            [FromBody] AuthorUpdateDto authorDto,
            [FromRoute] int id
        )
        {
            var author = authorDto.ToAuthorFromUpdaterDto(id);
            bool AuthorExists = await _authorRepository.AuthorExists(id);
            if (!AuthorExists)
            {
                Log.Warning($"Author with id {id} not found");
                return BadRequest("Author not found");
            }
            var newauthor = await _authorRepository.UpdateAuthor(author);

            return Ok(newauthor);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var deletedAuthors = await _authorRepository.DeleteAuthor(id);
            if (deletedAuthors == 0)
            {
                Log.Warning($"Delete Failed:  Author with id {id}  not found");
                return BadRequest("Author not found");
            }
            return Ok();
        }
    }
}
