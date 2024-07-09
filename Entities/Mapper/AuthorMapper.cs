using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.AuthorDto;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class AuthorMapper {
        public static Author ToAuthorFromAuthorRegisterDto(this AuthorRegisterDto  authorDto){
            return new Author{
                AuthorName = authorDto.Name,
                Biography = authorDto.Bio
            };
        }
        public static Author ToAuthorFromUpdaterDto(this AuthorUpdateDto  authorDto, int id){
            return new Author{
                AuthorName = authorDto.Name,
                Biography = authorDto.Bio,
                AuthorId = id
                
            };
        }
     }
}
