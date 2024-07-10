using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Helper.Response;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IImageService
    {
        Task<SavaImageRespoce> SaveImageAsync(IFormFile imageFile);
    }
}
