# GetInBackEnd
# Description
GetInBackEnd is a backend solution for a job board application, designed with the ASP.NET Core REST Web API. The application allows companies to post IT job listings, and individuals to view and apply to these listings. It also supports donations via Stripe.

The application is built using a layered architecture: Data Access Layer with Entity Framework, Service Layer with ASP.NET Core REST Web API, and the Docker Infrastructure. This design ensures the application is scalable and maintainable. It leverages JWT Token Authentication for securing resources and AutoMapper for object mapping.

# Technologies
The project is implemented using the following technologies:

- ASP.NET Core REST Web API
-Docker
-Entity Framework
-MSSQL 2019
-AutoMapper
-JWT Token Authentication
-Stripe Payment Integration
-.NET 7.0
# Requirements
To run this project, you need Docker and .NET 7.0 SDK installed.

# Installation
Clone the repository to your local computer:
git clone https://github.com/YourGithubUsername/GetInBackEnd.git

Navigate to the project folder:
cd GetInBackEnd

Restore the .NET packages and install the required dependencies:
dotnet restore

Update the connection strings in appsettings.json to match your SQL Server setup.

Build the solution:
dotnet build

Run the web project:
dotnet run --project GetInBackEnd

Alternatively, run the application in a Docker container:

Build the Docker image:
docker build -t getinbackend .

Run the Docker container:
docker run -p 5000:80 getinbackend

The API will now be running on http://localhost:5000.

# Usage
After installation and running the application, companies can register and add employees to create and view their own job listings. Individual users can view these listings and apply directly, attaching their CV in PDF format if necessary. Payments for donations can be made securely via Stripe.

# Future Updates
Additional features and improvements are planned. Details will be provided soon.

# Support
If you encounter any problems during installation or usage of the application, create an issue in the Issues section on GitHub.




