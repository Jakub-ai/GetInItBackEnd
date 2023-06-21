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
1. Clone the repository to your local computer:
```git clone https://github.com/YourGithubUsername/GetInBackEnd.git```

2. Navigate to the project folder:
```cd GetInBackEnd```

3. Restore the .NET packages and install the required dependencies:
```dotnet restore```

4. Update the connection strings in appsettings.json to match your SQL Server setup.

5. Build the solution:
```dotnet build```

6. Run the web project:
```dotnet run --project GetInBackEnd```

7. Alternatively, run the application in a Docker container:

8. Build the Docker image from the project directory:
```docker build -t getinbackend ```

9. Run the Docker container:
```docker run -p 5000:80 getinbackend```

The API will now be running on http://localhost:50099.
# Usage
After installation and running the application, companies can register and add employees to create and view their own job listings. Individual users can view these listings and apply directly, attaching their CV in PDF format if necessary. Payments for donations can be made securely via Stripe.

# Future Updates
Additional features and improvements are planned. Details will be provided soon.

# Support
If you encounter any problems during installation or usage of the application, create an issue in the Issues section on GitHub.




