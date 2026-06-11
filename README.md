# Job Tracker API

A REST API for tracking job applications, built with ASP.NET Core 10 and PostgreSQL.

## Tech Stack

- ASP.NET Core 10
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- JWT Authentication

## Getting Started

### Prerequisites

- Docker

### Run the application

1. Clone the repository:
   git clone https://github.com/kirca01/job-tracker-api.git
   cd job-tracker-api

2. Start the application:
   docker-compose up --build

3. API is available at: http://localhost:8080

## API Endpoints

### Authentication

| Method | Endpoint           | Description                 |
| ------ | ------------------ | --------------------------- |
| POST   | /api/auth/register | Register a new user         |
| POST   | /api/auth/login    | Login and receive JWT token |

### Job Applications

All endpoints require a Bearer token in the Authorization header.

| Method | Endpoint                   | Description                                    |
| ------ | -------------------------- | ---------------------------------------------- |
| GET    | /api/jobapplications       | Get all applications (filter: ?status=Applied) |
| GET    | /api/jobapplications/{id}  | Get a single application                       |
| POST   | /api/jobapplications       | Create a new application                       |
| PUT    | /api/jobapplications/{id}  | Update an application                          |
| DELETE | /api/jobapplications/{id}  | Delete an application                          |
| GET    | /api/jobapplications/stats | Get statistics by status                       |

## Application Statuses

- Applied
- Interview
- Offer
- Rejected

## Usage Examples

### Register

POST /api/auth/register
{
"email": "user@example.com",
"password": "password123"
}

### Create a job application

POST /api/jobapplications
Authorization: Bearer <token>
{
"company": "Google",
"position": "Backend Developer",
"jobUrl": "https://careers.google.com/jobs/123",
"notes": "Applied via LinkedIn"
}
