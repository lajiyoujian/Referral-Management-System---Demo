## Referral Management System

This document provides an overview of the Referral Management System, including its design, architecture, and instructions for setting up and using the system. The Referral Management System is a web application developed to manage and track referrals between medical specialists, patients, and providers. The system is a web application developed using .NET 7.0 Standard Term Support Framework.
<p align="center">
    <img src="https://github.com/lajiyoujian/Referral-Management-System---Demo/blob/master/DemoGif.gif" alt="Demo GIF" width="700">
</p>

          **Figure:** Demo video to go through each page of the Referral Management System.

### Table of Contents
1. [Objective](#Objective)
2. [Design](#Design)
   - [Frontend](#Frontend)
   - [Backend](#Backend)
3. [Architecture & Package](#Architecture--Package)
4. [Setup Instructions](#Setup-Instructions)
5. [Usage](#Usage)
6. [License](#License)

## 1. Objective
The main objective of this assignment is to build a robust referral management system consisting of a back-end API and a front-end interface. The system should enable users to effectively manage patient referrals, providers, and specialties, and perform CRUD operations.

## 2. Design
The Referral Management System follows a client-server architecture, with a web-based frontend interacting with a backend server. The frontend is responsible for rendering the user interface and handling user interactions, while the backend manages the data and performs the necessary business logic.

### Frontend
The frontend of the system is built using HTML, CSS, and JavaScript. It utilizes the ASP.NET Razor Pages framework to generate dynamic web pages. The frontend provides a user-friendly interface for creating referrals, searching specialties, managing patients, and interacting with the system's various features. It incorporates responsive design principles to ensure optimal usability across different devices and screen sizes.

### Backend
The backend of the system is developed using the ASP.NET Core Web API framework, which provides a robust and scalable infrastructure for building web applications. It follows the Model-View-Controller (MVC) architectural pattern, where the models represent the data entities, the views generate the user interface, and the controllers handle the application's logic and facilitate communication between the frontend and the backend.

The backend interacts with a database management system to store and retrieve data. It utilizes Entity Framework Core as the Object-Relational Mapping (ORM) tool to simplify database operations and provide seamless integration between the application and the underlying database.

 **3.** ## Architecture & Package
The Referral Management System follows a layered architecture to ensure the separation of concerns and maintainability of the codebase. 

The Framework:
Microsoft.AspNetCore.App
Microsoft.NetCore.App

Packages:
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Sqlite: SQLite provider for Entity Framework Core.
Microsoft.EntityFrameworkCore.Tools: Tools for Entity Framework Core migrations.
Microsoft.Extension.Http
Microsoft.Extension.DependencyInjection
System.Net.Http.Json

The layered architecture promotes modularity, flexibility, and maintainability of the system. It allows for easy extension or modification of individual layers without impacting the entire system.

# 4. ## Setup Instructions
To set up the Referral Management System on your local machine, follow these steps:

1. Clone the project repository from GitHub.
2. Ensure that you have the .NET 7.0 Standard Term Support Framework installed, including the .NET Core SDK and an SQLite database management system.
3. The application contains a pre-built SQLite database (referral_management.db). If you decide to use the pre-built database, then ignore steps 4 & 5.
4. (Optional) Create a new database for the system and update the connection string in the configuration file to point to the newly created database.
5. (Optional) Run the database migrations to create the necessary tables and schema in the database.
6. The Solution file is located in the referral_management_system_1 folder.  Build the solution to restore the NuGet packages and ensure that there are no build errors.
7. Ensure the backend API is running using the provided "http://localhost:5045/".
8. Start the application, and it will launch the web server hosting the Referral Management System.
9. To ensure the proper execution of the Referral Management System, the solution is set up to run both the Backend API and Frontend together as multiple startup projects. However, if for some reason they cannot be run simultaneously, it is advised to follow the sequence of running the backend API first, followed by the front end. This order ensures that the necessary backend functionalities are available before interacting with the frontend user interface.

If you have any questions, please do not hesitate to contact me. 

# 5. ## Usage
Once the Referral Management System is up and running, users can access its various features through the web interface. Some key functionalities include:

- Creating new Patients, Providers, Specialties, and Referrals by providing necessary patient and specialty information.
- Searching function at the top of each page, for patients, providers, specialties, and viewing Referral detailed information about each specialty.
- Managing Patients, Providers, Specialties, and Referrals by adding, updating, or deleting records.
- Tracking the referral status of patients and viewing referrals associated with dedicated patients and specialists who are working on them.
- Subtotal numbers of Patients, Providers, Specialties, and Referrals for future reports.

The system provides an intuitive and user-friendly interface to streamline the referral management process and improve overall efficiency.

# 6. ## License
The Referral Management System is intended for assessment, display, and demonstration purposes only. Ask permission for reuse.

---

This README document provides an overview of the Referral Management System, including its design, architecture, setup instructions, and usage guidelines. For detailed implementation details and code samples, refer to the project's source code and documentation available in the GitHub repository.
