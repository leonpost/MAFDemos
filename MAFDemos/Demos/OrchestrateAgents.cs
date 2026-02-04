using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace MAFDemos.Demos
{
    public static class OrchestrateAgents
    {
        public async static Task Run()
        {
            // Initialize chat client
            IChatClient chatClient =
            new OllamaApiClient(
                new Uri("http://localhost:11434/"),
                "llama3.2:1b");

            // Create individual agents
            AIAgent writer = new ChatClientAgent(
                chatClient,
                new ChatClientAgentOptions
                {
                    Name = "Writer",
                    ChatOptions = new() { Instructions = "You are a poet that writes 6 line poems in Shakespeare style." }
                });

            AIAgent explainer = new ChatClientAgent(
                chatClient,
                new ChatClientAgentOptions
                {
                    Name = "Explainer",
                    ChatOptions = new() { Instructions = "You are an explainer of poems. Explain the deeper thoughts in the poem you review. Keep it limited to 5 lines max. Start your response with '[Explanation]'" }
                });

            AIAgent translator = new ChatClientAgent(
                chatClient,
                new ChatClientAgentOptions
                {
                    Name = "Translator",
                    ChatOptions = new() { Instructions = "You are an translator of poems. Translate the text to dutch language. Start your response with '[Translation]'" }
                });

            // Orchestrate agents in a workflow
            AIAgent workflowAgent = AgentWorkflowBuilder
                .BuildSequential(writer, explainer, translator)
                .AsAgent();

            // Run workflow agent
            var workflowResponse =
                await workflowAgent.RunAsync("Write, explain and translate a poem");

            Console.WriteLine(workflowResponse.Text);
        }
    }
}
