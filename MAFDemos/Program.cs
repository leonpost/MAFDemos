try
{
    Console.WriteLine("Select a demo to run:");
    Console.WriteLine("1 - Basic Agent");
    Console.WriteLine("2 - Agents Workflow");
    Console.WriteLine("3 - Agent Using Tools");
    Console.Write("Enter your choice (1-3): ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("=== Basic Agent demo ===");
            await MAFDemos.Demos.BasicAgent.Run();
            break;
        case "2":
            Console.WriteLine("\n=== Agents Workflow demo ===");
            await MAFDemos.Demos.OrchestrateAgents.Run();
            break;
        case "3":
            Console.WriteLine("\n=== Agent Using Tools demo ===");
            await MAFDemos.Demos.AgentUsingTools.Run();
            break;
        default:
            Console.WriteLine("Invalid choice. Please select 1, 2 or 3.");
            break;
    }

}

catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
}
