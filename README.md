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

## Usage

To run the project locally, follow these steps:

1. Clone the repository.
2. Set up the necessary configurations (e.g., database connection).
3. Run the .NET Core API and Blazor Server.

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
