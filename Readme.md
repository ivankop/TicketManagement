# Concert Ticket Management System

 A simple .NET Web API for managing concert tickets with these features:

- Event Management
- Ticket Reservations and Sales
- Venue Capacity Management

## How to run
- Install VS 2022 with .NET Core 8
- Build solution
- Update "DefaultConnection" in appsettings.json to your database server
- Run these two commands to generate and create databases
```
dotnet ef migrations add AddData --project TicketManagement.Data --startup-project TicketManagement.Api

dotnet ef database update --project TicketManagement.Data --startup-project TicketManagement.Api
```
- Import Postman collection `Ticket APIs.postman_collection.json` to Postman and start application.
- Login as event manager or user and copy the returned token
- Use token to make calls to other APIs 


