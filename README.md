# Library Management System - Backend

Welcome to the Library Management System (LMS) backend project! This README will guide you through the setup, features, and architecture of the LMS backend built with .NET 8. 

## Introduction

The LMS backend is designed to provide a robust and scalable solution for managing a library's operations. This system includes features like book cataloging, user management, borrowing and returning books, reservation management and Notification using Email and Internally for better user exprience.

## Features

1. **Book Search and Cataloging**: 
   - Allows users to search for books using various criteria like title, author, genre, and tags.
   - Utilizes a robust search engine with filters and pagination.

2. **Membership Management**: 
   - Handles user registration, profile management, and authentication using ASP.NET Core Identity.

3. **Book Borrowing and Return**: 
   - Facilitates the borrowing and return of books, tracking due dates, and availability.

4. **Book Reservations**: 
   - Allows users to reserve books that are currently checked out, with notifications when they become available.

5. **Notification System**: 
   - Sends alerts for due dates, overdue books, and available reservations via email.

## Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core**
- **Microsoft Identity**
- **Hangfire**
- **Serilog**
- **MySQL**
- **Swagger**

## Installation

1. **Clone the repository**:
   ```sh
   git clone https://github.com/your-username/library-management-system-backend.git
   cd library-management-system-backend
   ```

2. **Install dependencies**:
   ```sh
   dotnet restore
   ```

3. **Build the project**:
   ```sh
   dotnet build
   ```

## Configuration

1. **Environment Variables**:
   - Create a `.env` file in the root directory.
   - Add the following variables:
     ```
     SMTP_MAIL=fuaadmuhe12@gmail.com
     SMTP_DISPLAYNAME=Fuad
     SMTP_PASSWORD=your-password
     SMTP_HOST=smtp.gmail.com
     SMTP_PORT=587
     ADMIN_PASSWORD=@Pass!211
     ADMIN_USERNAME=Fuad
     ADMIN_EMAIL=fuaadmuhe12@gmail.com
     ADMIN_FIRSTNAME=Fuad
     ADMIN_LASTNAME=Mohammed
     ```

2. **appsettings.json**:
   - Ensure your `appsettings.json` includes the JWT settings:
     ```json
     {
       "JWT": {
         "Key": "your-secret-key",
         "Issuer": "http://localhost:5143",
         "Audience": "http://localhost:5143"
       },
       "AppConfig": {
         "ResevationExipireHour": 1, 
         "DeadLineRemindDay": 1
       }
     }
     ```

## Background Jobs

- **Hangfire** is used for background jobs such as:
  - Sending email notifications for due dates and overdue books.
  - Notifying users when reserved books become available.
  - Periodically sending reminders.