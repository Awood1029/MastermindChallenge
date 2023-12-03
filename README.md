# Mastermind Game for LinkedIn's Reach Apprenticeship Interview Process

Welcome to the Mastermind Game, a project developed for LinkedIn's Reach Apprenticeship program interview process.

## Table of Contents

- [Gameplay](#gameplay)
- [Technology Stack](#technology-stack)
- [Features](#features)
- [Usage](#usage)
- [Live Demo](#live-demo)
- [API Endpoint](#api-endpoint)
- [LinkedIn Backend Software Engineer Considerations](#linkedin-backend-software-engineer-considerations)
- [Project Structure](#project-structure)

## Gameplay

To play the game, users need to register or log in. Once authenticated, they can choose from three difficulty levels:
- Easy (4 digits)
- Medium (5 digits)
- Hard (6 digits)

Players have 10 attempts to guess the correct number. Hints are available, revealing one digit at a time in order. After completing a game, the results are saved to the database, including the player's ID, win/loss status, and the number of attempts used.

Leaderboards display the top 10 players for each difficulty level, based on the lowest number of attempts.

## Technology Stack

- **Backend:** .NET Core, C#, EntityFramework
- **Frontend:** Blazor Server
- **Authentication:** .NET Identity, JWT
- **Real-time Communication:** SignalR
- **Database:** Azure SQL Database
- **API Documentation:** NSwag Studio
- **Logging:** Seq
- **Mapping:** AutoMapper
- **Random Number Generation:** [Random.org](https://www.random.org/clients/http/api/)

## Features

- User registration and authentication
- Three difficulty levels with varying digit lengths
- Hint system revealing digits one at a time
- Leaderboards for each difficulty level
- Game results stored in the database
- **Unfinished Multiplayer Feature:**
  - Started implementation of a multiplayer feature using SignalR in Blazor.
  - Created a MultiplayerController and Multiplayer Service.
  - Note: This feature is currently incomplete and requires further development.
 
## Project Structure

The solution consists of two projects:

### 1. MastermindChallenge.API

- **Controllers:**
  - **Auth:** Contains API controllers related to authentication.
  - **Game:** Holds API controllers for game-related functionality.

- **Data:**
  - **Models and DbContext:** This folder contains data models and the `DbContext` used by Entity Framework. It's responsible for defining the structure of the database and how the application interacts with it.

- **ModelDtos:**
  - **Dto Files:** This directory is dedicated to Data Transfer Objects (DTOs). DTOs are used when making API calls that don't require all the information from the original model, helping to optimize data transfer between the client and server.

### 2. MastermindChallenge.Blazor.Server

- **Models:**
  - **GamePage Model:** Holds data specific to the game view in the Blazor application.

- **Providers:**
  - **ApiAuthenticationStateProvider:** Manages JWT and authentication logic for the Blazor application. It's responsible for tracking the authentication state of the user through the use of the JWT token.

- **Services:**
  - **Authentication Service:** Handles business logic related to user authentication.
  - **Game Service:** Manages business logic and communication with the API.

## Usage

To run the project locally, follow these steps:

1. Clone the repository.
2. Set up the necessary configurations (e.g., database connection).
3. Set the.NET Core API and Blazor Server as the startup projects in Visual Studio and run.

## Live Demo

The project is deployed on Azure. Check out the live demo at [https://mastermindchallengeblazorserver.azurewebsites.net/](https://mastermindchallengeblazorserver.azurewebsites.net/).

## API Endpoint

Access the API endpoint at [https://mastermindchallengeapi.azurewebsites.net/api](https://mastermindchallengeapi.azurewebsites.net/api).

## LinkedIn Backend Software Engineer Considerations

As an aspiring backend software engineer at LinkedIn, I believe several aspects of this project align with the skills and qualities valued in this role. Here are specific points that showcase my capabilities and commitment to delivering high-quality software solutions:

### 1. **API Design and Development**

The project demonstrates my proficiency in designing and implementing robust APIs using .NET Core. I've utilized best practices for API development, ensuring efficiency, scalability, and maintainability to the best of my ability in a limited time.

### 2. **Database Management**

By incorporating EntityFramework and SQL Server, I've showcased my ability to manage and interact with databases efficiently. The utilization of Azure for deployment further emphasizes my familiarity and willingness to learn modern cloud-based database solutions.

### 3. **Authentication and Security**

The implementation of .NET Identity and JWT authentication reflects my understanding and application of secure user authentication methods. 

### 4. **Integration of External Services**

In the project, I integrated the [Random.org](https://www.random.org/clients/http/api/) API for random number generation, demonstrating my capability to incorporate external services effectively.

### 5. **Logging and Monitoring**

I specifically implemented Seq for logging to specifically show my understanding of the importance logging has in efficient debugging, monitoring, and continuous improvement in a production environment.

### 6. **Deployment on Azure**

Deploying the entire project on Azure showcases my ability to learn and utilize modern cloud-based solutions, aligning with LinkedIn's commitment to leveraging cutting-edge technologies and platforms.

### 7. **Documentation and Code Organization**

I've prioritized clear documentation and organized code structures to enhance readability and maintainability. This reflects my dedication to creating software that is not only functional but also comprehensible for collaborative development. 
Specific examples of this would include creating Services to handle business logic, maintaining clear folder structures, and creating utility files for utility functions.

### 8. **Technological Adaptability**

The use of Blazor Server for the UI highlights my adaptability to diverse technologies. This aligns with LinkedIn's dynamic tech environment, emphasizing the importance of staying current with emerging technologies.

## Full Build and Thought Process

When starting this project, I decided to build this game using Blazor Server to represent my general engineering knowledge. Despite having experience with other languages and frameworks more applicable to engineering at LinkedIn, the majority of my expertise lies in .NET development.

Firstly, I initiated the project by building out my API. I set up EntityFramework, created basic game models and DTOs, and implemented .NET's Identity Framework for authentication. Admittedly, this proved to be a time-consuming and challenging part of my implementation, given my previous authentication experience in React and Firebase Authentication. However, overcoming various hurdles, I established a one-to-many relationship between games and players.

At this point, I transitioned to building the initial frontend with Blazor Server. Streamlining the process, I used NSwag to generate models based on the Swagger file from my API. I created a game service to organize code and keep more of the business and data access logic off the frontend.

After setting up register and login pages, I focused on building the game in the UI and determining how to process the game. When a player selects a difficulty and starts the game, it triggers a call to an external API to generate a random string based on the difficulty level. After developing the base game logic and loop, I analyzed the functions used to process the player guess and compare it to the answer. Realizing the initial process was performing nested loops using arrays and .contains() methods, I optimized it using a dictionary, significantly improving time complexity.

Following the refactoring, I decided to add a hint button that provides a digit in the answer one by one, in order.

The next feature I introduced was creating leaderboards based on the attempts used as a score. I created three separate leaderboards based on difficulty levels, achieved by joining the game and player tables to access the username and attemptsUsed on games where the player won. This retrieves only the top 10 scores for each difficulty.

With a solid MVP coded up, I aimed to further showcase my backend passion by deploying the app to Azure Web Services for the first time. I created necessary resource groups in Azure, spun up an Azure SQL Database instance, imported my database structure and data, created another App Service for my frontend, updated necessary connections in my appsettings, and set up my code for CI/CD to Azure.

With my project live online, I asked my girlfriend to help with testing. After testing the game with her (Fun note: You'll see her real scores as Chels, and she convinced me to delete fake data that had scores better than hers), I wanted to tackle yet another feature that could show my backend development potential — implementing real-time multiplayer game sessions.

Unfortunately, I don't have the time to finish this feature before my deadline to submit. As of right now, I've researched how to use HubConnections from SignalR to create a real-time connection. I created a table in the database to hold sessions containing a sessionId and playerCount. I implemented a Multiplayer controller that handles the creation and retrieval of multiplayer sessions. After that, I started changing my UI to try and re-use my game component. I created a MultiplayerService and HubManager to handle the events for HubConnection and attempted implementing the HubConnection in my component to connect players to a single game session. Right now, I have successfully created sessions and connected to them, but I have struggled to figure out real-time UI updates to verify if my connection is working properly.

I feel proud of what I completed. I definitely have a list of things I would refactor and cleanup, but with limited time, I wanted to show a variety of my skills, as well as show my passion by tackling things I was not as familiar with. Throughout my process, I tried to keep clean architecture in mind, but admittedly with the time crunch, I started to slack on this. I'm very excited to be able to walk you all through this project live and discuss it!

