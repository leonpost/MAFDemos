using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;
using System.ComponentModel;

namespace MAFDemos.Demos
{
    public static class AgentUsingTools
    {
        public async static Task Run()
        {
            // Initialize chat client
            IChatClient chatClient =
            new OllamaApiClient(
                new Uri("http://localhost:11434/"),
                "llama3.2:1b");

            // Create agent with weather tool
            AIAgent weatherAgent = new ChatClientAgent(
                chatClient,
                new ChatClientAgentOptions
                {
                    Name = "WeatherAgent",
                    ChatOptions = new()
                    {
                        Instructions = "You provide weather information.",
                        Tools = [
                            AIFunctionFactory.Create(GetWeather)
                        ],
                    }
                });

            // Run agent
            var response = await weatherAgent.RunAsync("What's the weather in Amsterdam?");
            Console.WriteLine(response.Text);
        }

        // Define weather tool
        [Description("Gets the current weather for a location")]
        async static Task<string> GetWeather([Description("City name")] string city)
        {
            // Simulate API call
            await Task.Delay(500);
            return $"Sunny, 7°C in {city}";
        }
    }
}
