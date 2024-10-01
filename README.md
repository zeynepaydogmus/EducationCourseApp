# EducationCourseApp

EducationCourseApp is a microservices-based demo application for managing educational courses. The project includes multiple services, an API gateway, and an identity server.

## Features
- **Identity Server**: Handles user authentication and authorization.
- **Gateway**: API gateway for routing requests to microservices.
- **Microservices**: Manage courses, students, and instructors.
- **Shared Components**: Common logic shared between services.

## Microservices and Technologies

### 1. Identity Server
- **Libraries**:
  - IdentityServer4
  - Microsoft.AspNetCore.Identity
- **Database**: SQL Server

### 2. Course Service
- **Libraries**:
  - Entity Framework Core
  - AutoMapper
  - MediatR
- **Database**: PostgreSQL

### 3. Student Service
- **Libraries**:
  - Dapper
  - FluentValidation
- **Database**: MongoDB

### 4. Instructor Service
- **Libraries**:
  - Entity Framework Core

- **Database**: MySQL

### 5. Gateway
- **Libraries**:
  - Ocelot


## Getting Started
1. Clone the repository:  
   `git clone https://github.com/zeynepaydogmus/EducationCourseApp.git`
2. Navigate to the solution folder.
3. Run the solution in Visual Studio or via CLI.

