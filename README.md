# WebRealTimeChatApp

WebRealTimeChatApp is a simple real-time chat application developed in C#, HTML, and JavaScript, utilizing the .NET platform and incorporating the SignalR library.

## Purpose

This application provides a chat environment with multiple channels. Users can freely chat in each channel with the desired username. The application stores messages in the database and initially displays them to users. When a user sends a message, it is broadcasted to all other users over the same connection (SignalR).

## Running the Application

To run the application, follow these steps:

1. Clone the Github repository:

   ```bash
   git clone https://github.com/halil-cinar/WebRealTimeChatApp.git
2.Modify the connection string with your own database information.

3.Apply migrations:
dotnet ef database update

The application is ready to use!

## Technologies Used
*C#*
*HTML*
*JavaScript*
*.NET*
*SignalR*
## Contributors
[[halil-cinar](https://github.com/halil-cinar)]
The sole contributor to this application is [halil-cinar](https://github.com/halil-cinar).
