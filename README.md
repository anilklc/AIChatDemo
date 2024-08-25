# AI Chat Demo

## Overview

**AI Chat Demo** is a sample application designed to help you learn and reinforce your skills in real-time communication using SignalR and integrating OpenAI's ChatGPT within a .NET Core environment. The main goal of this project is to understand how to implement real-time communication with SignalR and how to integrate it with an AI service.

## Technologies Used

- **.NET Core 8**: The base framework used for building the API and Client projects.
- **SignalR**: Used for real-time communication between the client and server.
- **OpenAI API**: Integrated to enable AI-based responses using ChatGPT.

## Project Structure

The project is divided into two main components:

1. **API**: Handles SignalR connections and interacts with the AI service.
2. **Client**: Provides the frontend interface for users to interact with the chat application.


## API Endpoints

- **/chatHub**: The main SignalR hub endpoint for real-time chat communication.

## SignalR Hub

The `ChatHub` class in the API handles all real-time messaging between the client and the AI service. It:

- Accepts messages from the client, along with the user's ID and name.
- Sends these messages to OpenAI's ChatGPT via the `AIChatService`.
- Returns the AI's response back to the user in real-time.

## Installation

### Prerequisites

- **.NET Core 8 SDK**
- **PostgreSQL** (or another used database)
- 
### Steps

1. **Clone the repository:**
    ```sh
    git clone https://github.com/anilklc/AIChatDemo.git
    ```

2. **Navigate to the project directory:**
    ```sh
    cd AIChatDemo
    ```

3. **Set up the API:**

    - **Navigate to the API project directory:**
      ```sh
      cd AIChatDemo.API
      ```

    - **Update the database connection string and other configurations in `appsettings.json`.**

    - **Apply migrations and create the database:**
      ```sh
      dotnet ef database update
      ```

    - **Run the API application:**
      ```sh
      dotnet run
      ```

4. **Set up the Client:**

    - **Navigate to the Client project directory:**
      ```sh
      cd ../AIChatDemo.Client
      ```

    - **Run the Client application:**
      ```sh
      dotnet run
      ```

### Screenshots

### Screenshots

![GIF 1](Screenshots/1.gif) 
*Shows an error alert for incomplete form submission on the registration page.*

![GIF 2](Screenshots/2.gif) 
*Displays a success alert for correct and complete form submission, followed by redirection to the login page.*

![GIF 3](Screenshots/3.gif) 
*Demonstrates an attempt to access the chat page without logging in, leading to redirection to the login page.*

![GIF 4](Screenshots/4.gif) 
*Shows successful access to the chat page after logging in.*

![GIF 5](Screenshots/5.gif) 
*Shows individual conversations between two users and AI on different browsers.*



### License

This project is licensed under the MIT License.

