using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.AuthDto;
using Library_Management_System_BackEnd.Entities.Dtos.BookDto;

namespace Library_Management_System_BackEnd.Entities.Dtos.ReservationDto
{
    /*
    {
    "reservationId": 1,
    "userId": "f902a534-3189-466f-81dd-ccb4cde938b4",
    "user": {
      "firstName": "Fuad",
      "lastName": "Mohammed",
      "roles": "User",
      "id": "f902a534-3189-466f-81dd-ccb4cde938b4",
      "userName": "fuadmuhe",
      "normalizedUserName": "FUADMUHE",
      "email": "fuad.mohamed@aait.edu.et",
      "normalizedEmail": "FUAD.MOHAMED@AAIT.EDU.ET",
      "emailConfirmed": false,
      "passwordHash": "AQAAAAIAAYagAAAAEHCXwiWqNXZixoX0DmCM8E/xzWDIR+vmYxlNfSXvFDntqGlvp9eyR6qWgQbASdc3ag==",
      "securityStamp": "AMCEZPJIF47V56GGCQ5WMQQBQ2DHRO3B",
      "concurrencyStamp": "d16a787b-472f-47e5-b21f-14e64cb74159",
      "phoneNumber": null,
      "phoneNumberConfirmed": false,
      "twoFactorEnabled": false,
      "lockoutEnd": null,
      "lockoutEnabled": true,
      "accessFailedCount": 0
    },
    "bookId": 1,
    "book": {
      "bookId": 1,
      "title": "the sadjasd",
      "authorId": 2,
      "author": {
        "authorId": 2,
        "authorName": "Autheor 2",
        "biography": "The ds Trader"
      },
      "isbn": "9780141439518",
      "categoryId": 5,
      "category": {
        "categoryId": 5,
        "categoryName": "Academic"
      },
      "bookStatus": 2,
      "publicationYear": "2015-04-10",
      "description": "A romantic novel that charts the emotional development of the protagonist Elizabeth Bennet.",
      "coverImage": "Resource//6d186e59-cde9-47d1-887f-41595b2e65b9.jpg",
      "bookTags": [
        {
          "bookId": 1,
          "tagId": 2,
          "tag": {
            "tagId": 2,
            "tagName": "Mystery",
            "bookTags": []
          }
        },
        {
          "bookId": 1,
          "tagId": 4,
          "tag": {
            "tagId": 4,
            "tagName": "Science Fiction",
            "bookTags": []
          }
        },
        {
          "bookId": 1,
          "tagId": 5,
          "tag": {
            "tagId": 5,
            "tagName": "Fantasy",
            "bookTags": []
          }
        },
        {
          "bookId": 1,
          "tagId": 8,
          "tag": {
            "tagId": 8,
            "tagName": "Coming of Age",
            "bookTags": []
          }
        }
      ],
      "imageFile": null
    },
    "reservationDate": "2024-07-11T22:30:47.8722514",
    "status": 2,
    "notificationSent": false,
    "notificationTime": null
  }, */
    public class ReservationViewDto
    {
        public int ReservationId { get; set; }
        public UserMinimalViewDto? User { get; set; }
        public ViewBookDto? Book { get; set; }
        public string? ReservationStatus { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
