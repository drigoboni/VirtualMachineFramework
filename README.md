# Virtual Vending Machine
---
## Problem and Solution

The project involves creating a virtual vending machine that allows users to purchase items using virtual money. The vending machine contains a list of products, each with a price and available quantity. Users can insert money, select products to purchase, and receive change if any balance remains. The system ensures users cannot purchase items if there is insufficient quantity or funds, and it accurately returns the change and updates product quantities after each purchase.

## Solution Focus

This solution is a full-stack implementation, featuring both a back-end and a front-end and focusing mainly on the .NET ecosystem. The back-end, built in C# with ASP .NET Core Web API and Entity Framework Core with SQL Server, handles product data and business logic, while the front-end, developed using ASP .NET MVC with Bootstrap, provides a user interface for interaction with the vending machine.

## Concept and Rules

- The vending machine contains a list of products, with a corresponding amount and quantity available.
- Users put in (virtual) money and purchase an item.
- After they have purchased an item, they can use the remaining money to purchase another item or have the change returned to them.
- Once they are done, they should see a list of the items they have purchased.
- Users cannot purchase a product if there is no quantity remaining.
- Users can only purchase a product if they have put in funds equal to or greater than the cost of the product.
- Users should receive the correct change back after the transaction.
- The product quantity should be reduced by the amount of quantity purchased of an item.

## Technical Choices and Architecture

#### Back-End

- ASP .NET Core: Chosen for its robustness, scalability, and familiarity with enterprise applications.
- Entity Framework Core: Used for data access to manage product information within a SQL Server database.
- RESTful API: Provides endpoints for interacting with the vending machine's functionalities (e.g., getting products, making purchases).

#### Front-End

- ASP .NET MVC: Allows for a clean separation of concerns and integrates well with the ASP.NET Core back-end.
- Bootstrap: Used for responsive design and ensuring a user-friendly interface.
- JavaScript: Enhances interactivity, such as handling modal displays and updating the UI based on user actions.

#### Architectural Reasoning

The architecture is designed to be modular and maintainable, the client/service architecture ensures a clean separation between front-end and back-end, enhancing maintainability and scalability. The back-end handles core business logic and data persistence, while the front-end focuses on presenting data and interacting with users. This separation ensures that each part of the application can be developed, tested, and maintained independently. These technologies were chosen for their powerful features and strong community support, making them ideal for enterprise-grade applications.

## Solution Structure

| Main Projects        | Description                                                           |
| -------------------- | --------------------------------------------------------------------- |
| VirtualMachine .Web  | The ASP .NET MVC web application.                                     |
| VirtualMachine .Core | Core business logic.                                                  |
| VirtualMachine .Data | Data access layer using Entity Framework Core.                        |
| VirtualMachine .API  | ASP .NET Core Web API (RESTful) for managing products and purchases.  |

| Test Projects               | Description                                    |
| --------------------------- | ---------------------------------------------- |
| VirtualMachine .Web .Tests  | Unit tests for the Web project.                |
| VirtualMachine .Core .Tests | Unit tests for the Core project.               |
| VirtualMachine .Data .Tests | Unit tests for the Data project.               |
| VirtualMachine .API .Tests  | Unit tests for the API project.                |

### Design Pattern

The State Design Pattern allows an object to change its behavior when its internal state changes, which is perfect for modeling the different states of a vending machine (e.g., accepting money, selecting a product, dispensing product, returning change). 
The root namespace uses "Virtual Machine" instead of "Vending Machine," making the project more generic and allowing for future expansions to include other types of virtual machines that accept virtual money.


## Code Quality

- Clarity and Maintainability: The code follows SOLID principles, ensuring that each class and method has a single responsibility. Consistent naming conventions and clear separation of concerns are maintained throughout the project. The root uses Virtual Machine rather than Vending Machine, making it more generic and making room for possible expansions of other virtual machines that accept virtual money.
- Testing: Unit tests for core business logic, data and integration tests for the web layer are included. These tests ensure the correctness of the application and facilitate future changes.
- UX: The web interface is designed to be intuitive and user-friendly, providing clear feedback to the user about their actions (e.g., displaying current balance, purchased products).

## Trade-Offs and Future Improvements

- Scalability: The current design is optimized for a single-instance application. To achieve higher scalability, we can implement caching, load balancing, and a more robust database solution, enabling better handling of multiple users. The database, primarily used for proof of concept, currently lists only Product as an entity. This can be expanded to include transactions and user relationships, enhancing functionality and data integrity.
- Error Handling and Logging: Basic error handling has been implemented, particularly for the development environment. However, for production readiness, more comprehensive logging and monitoring should be added. The Vending Machine includes a robust messaging mechanism for all interactions, which could be better utilized in the front-end to enhance the user experience. Additionally, logs can be stored in the SQL Server database using a logging library like Serilog for improved traceability and analysis.
- Security: The application assumes a trusted environment. For production, authentication and authorization mechanisms should be added. Additionally, performing critical operations on the server and validating data received from the client would enhance security and prevent potential vulnerabilities.
- Real-Time Updates: Implementing real-time updates, such as using SignalR or WebSockets to reflect changes in product availability and user balance immediately.
- Enhanced User Interface: Last but not least, the front-end can be improved to provide a more user-friendly experience. This includes better styling, more intuitive controls, and responsive design to support various devices.

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server

### Setup Instructions

1. **Clone the repository**:
    ```sh
    git clone <GIT REPOSITORY URL>
    cd virtual-vending-machine
    ```

2. **Set up the database**:
    - Update the connection string in `appsettings.json` for `VirtualMachine.API` project.
    - Note: A default connection string has been configured in the VendingMachineDbContext for the VirtualMachine.Data project, making it easy to apply migrations and create the database schema directly from there.
    - Apply migrations to create the database schema:
    
    .NET CLI
      ```sh
      cd VirtualMachine.Data
      dotnet ef database update
      ```
    Visual Studio
      ```sh
      PowerShell
      Update-Database
      ```
    > More info: `https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations`
      
3. **Run the application**:
    - Start the API project:
      ```sh
      cd ../VirtualMachine.API
      dotnet run
      ```
    - Start the web project:
      ```sh
      cd ../VirtualMachine.Web
      dotnet run
      ```
     
     > Note: You might need to update the Url or Port in the configuration setting `ApiEndpoints:Base` in `appsettings.json` for `VirtualMachine.Web` project.
     
4. **Navigate to the application**:
    - Open your browser and go to `https://localhost:<PORT NUMBER>` for the web application.

## Small Print

- **Author:** [Rodrigo Eduardo Boni](https://www.linkedin.com/in/rodrigoeduardoboni/)
- **License:** MIT - feel free to play with it!