using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace MAFDemos.Demos
{
    public static class BasicAgent
    {
        public async static Task Run()
        {
            // Initialize chat client
            IChatClient chatClient =
            new OllamaApiClient(
                new Uri("http://localhost:11434/"),
                "llama3.2:1b");

            // Create agent
            AIAgent writer = new ChatClientAgent(
                chatClient,
                new ChatClientAgentOptions
                {
                    Name = "Writer",
                    ChatOptions = new() { Instructions = "You are a poet that writes 6 line poems in Shakespeare style." }
                });

            // Run agent
            var response = await writer.RunAsync("Write a poem.");

            Console.WriteLine(response.Text);
        }
    }
}
