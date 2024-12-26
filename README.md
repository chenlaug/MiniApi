# Raccoon and Owner Management API

This project is an API that allows managing `Owners` and their associated `Raccoons`. It provides endpoints to create, update, retrieve, and delete both `Owners` and `Raccoons`. Additionally, it supports adding and removing `Raccoons` from specific `Owners`.

## Features

- **Owner Management**:
  - Create a new `Owner`.
  - Update an existing `Owner`.
  - Delete an `Owner`.
  - Retrieve all `Owners` or a specific `Owner` by ID.

- **Raccoon Management**:
  - Create a new `Raccoon` and assign it to an `Owner`.
  - Update an existing `Raccoon`.
  - Delete a `Raccoon`.
  - Retrieve all `Raccoons` or a specific `Raccoon` by ID.

- **Owner-Raccoon Association**:
  - Add an existing `Raccoon` to an `Owner`.
  - Remove a `Raccoon` from an `Owner`.

- **Circular Reference Handling**:
  - The API uses `JsonIgnore` to prevent circular references between `Owner` and `Raccoon`.

## Technologies

- **ASP.NET Core**: Used for building the API.
- **Entity Framework Core**: ORM for database interaction.
- **SQL Server**: For database management.
- **JSON Serialization**: Handling circular references using `JsonIgnore`.

## Installation

### Prerequisites:
- .NET SDK installed on your machine
- SQL Server instance running
- An IDE like Visual Studio or Visual Studio Code

### Steps:

1. Clone the repository:

   ```bash
   git clone https://github.com/chenlaug/MiniApi.git

2. Navigate to the project directory:
   
    ```bash
   cd MiniApi
    
3. Restore the NuGet packages:
    ```bash
    dotnet restore
4. Update the connection string in appsettings.json to match your SQL Server instance.
5. Apply the database migrations:
    ```bash
      dotnet ef migrations add InitialCreate
      dotnet ef database update
6. Run the application:
     ```bash
     dotnet run
  
## API Endpoints

### Owner Endpoints

- **GET /api/owner**: Get all owners.
- **GET /api/owner/{id}**: Get a specific owner by ID.
- **POST /api/owner**: Create a new owner.
- **PUT /api/owner/{id}**: Update an existing owner by ID.
- **DELETE /api/owner/{id}**: Delete an owner by ID.

### Raccoon Endpoints

- **GET /api/raccoon**: Get all raccoons.
- **GET /api/raccoon/{id}**: Get a specific raccoon by ID.
- **POST /api/raccoon**: Create a new raccoon.
- **PUT /api/raccoon/{id}**: Update an existing raccoon by ID.
- **DELETE /api/raccoon/{id}**: Delete a raccoon by ID.

### Additional Endpoints

- **POST /api/owner/AddRaccoonToOwner**: Assign an existing raccoon to an owner.
- **POST /api/owner/RemoveRaccoonFromOwner**: Remove a raccoon from an owner.
- **GET /api/owner/GetRaccoonsByOwner**: Get a list of raccoons belonging to an owner.
- **POST /api/owner/CreateRaccoonForOwner**: Create a new raccoon and assign it to an owner.

## Auteur
- Laughan Chenevot - [Profil GitHub](https://github.com/chenlaug)
