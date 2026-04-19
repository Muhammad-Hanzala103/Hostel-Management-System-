# AI Chatbot in .NET

A simple AI-powered chatbot application built with .NET 10, integrating OpenAI's GPT-3.5-turbo API.

## Features

- Interactive console-based chat interface
- Integration with OpenAI API
- Context-aware conversations
- Hostel management focused responses

## Prerequisites

- .NET 10 SDK
- OpenAI API Key

## Setup

1. Clone the repository
2. Navigate to the AIChatbot directory
3. Replace `YOUR_OPENAI_API_KEY` in `Program.cs` with your actual OpenAI API key
4. Run the application:

```bash
dotnet run
```

## Usage

- Type your message and press Enter
- The AI will respond based on the conversation context
- Type 'exit' to quit

## Architecture

- Uses HttpClient for API calls
- JSON serialization with System.Text.Json
- Asynchronous programming with async/await

## Technologies Used

- .NET 10
- OpenAI API
- C# 13