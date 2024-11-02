# Project Management Task
## Overview
This Project Management Task allows users to create, manage, and track projects and tasks effectively.
and also manage user roles
## Features
- **Project Management:** Create, update, and delete projects. Each project can have multiple tasks.
- **Task Management:** Assign tasks to team members, set deadlines, and track completion.
- **User Roles:** Define roles for users (e.g., Manager, Employee) with specific permissions.
- ## Installation
- ### Prerequisites
Before you begin, ensure you have met the following requirements:
- [**.NET SDK**](https://dotnet.microsoft.com/download) (version 8.0 or later) installed on your machine.
- A compatible IDE or editor (e.g., Visual Studio, Visual Studio Code).
### Clone the Repository
- **1. Clone the repository using Git:**
       git clone https://github.com/samar-mostafa/ProjectManagementTask.git
- **2.Navigate to the project directory:**
      cd ProjectManagementTask
- **3.Restore the project dependencies by running:**
     dotnet restore
- **4.Build the application using the following command:**
 dotnet build
- **5.To run the application, execute:**
   dotnet run
- **6.Open your browser and go to http://localhost:5270  to access the application.**
  # API Documentation

## Products API

### Get Projects
- **URL**: `/api/Project/AllProjects`
- **Method**: `GET`
- **Description**: Retrieves a list of all Projects.
- **Response**:
    - **200 OK**: Returns an array of products.
    - **Example Response**:
    ```json
    [
        { "id": 1, "name": "Projectt 1", "budget": 100 },
        { "id": 2, "name": "Project 2", "budget": 200 }
    ]
    ```

### Create Project
- **URL**: `/api/Project/NewProject`
- **Method**: `POST`
- **Description**: Creates a new project.
- **Request Body**:
    - **Content-Type**: `application/json`
    - **Example Request Body**:
    ```json
    { "name": "New Project", "budget": 150000 }
    ```
- **Response**:
    - **201 Created**: Returns created message.
    - **400 Bad Request**: If validation fails.

### Edit Project
- **URL**: `/api/Project/Edit`
- **Method**: `POST`
- **Description**: Edit a project.
- **Request Body**:
    - **Content-Type**: `application/json`
    - **Example Request Body**:
    ```json
    {"Id":1 , "name": "New Project", "budget": 150000 }
    ```
- **Response**:
    - **200 Ok**: Returns a message that edited successfully.
    - **400 Bad Request**: If validation fails.
 
  ### Change Project status
- **URL**: `/api/Project/ChangeStatus`
- **Method**: `POST`
- **Description**: change status of  a project.
- **Request Body**:
    - **Content-Type**: `application/json`
    - **Example Request Body**:
    ```json
    {"Id":1 }
    ```
- **Response**:
    - **200 Ok**: Returns a message that changed successfully.
    - **400 Bad Request**: If validation fails.



