# Microsoft Agent Framework demos

Small demo project that shows how to use the `Microsoft.Agents.AI` framework with a local Ollama LLM model. Targets .NET 9 and contains three small demos that exercise single-agent usage, an agent workflow (orchestration), and an agent that uses a local tool.

## Prerequisites
- .NET 9 SDK installed (`dotnet --version` should show a 9.x SDK).
- Ollama installed and running locally (the demos connect to `http://localhost:11434/` by default).
- Models pulled into Ollama that match the model names used in the demos (examples below).

## Projects / Demos
The executable's entry point (`Program.cs`) presents a simple menu to run one of the three demos.

1. `BasicAgent` (`MAFDemos/Demos/BasicAgent.cs`)
   - Demonstrates a single `ChatClientAgent`.
   - Uses `OllamaApiClient` to connect to a local Ollama model (default model in the file: `llama3.2:1b`).
   - The agent receives instructions (Shakespeare-style 6-line poem) and runs a single prompt, printing the resulting text.

2. `OrchestrateAgents` (`MAFDemos/Demos/OrchestrateAgents.cs`)
   - Shows how to build a sequential workflow of agents using `AgentWorkflowBuilder`.
   - Constructs multiple `ChatClientAgent` instances (writer, explainer, translator) that share the same `IChatClient`.
   - Runs the workflow as a single agent and prints the combined response (writer -> explainer -> translator).

3. `AgentUsingTools` (`MAFDemos/Demos/AgentUsingTools.cs`)
   - Demonstrates how to register a local "tool" (a function) that an agent can call during its run.
   - The demo defines a `GetWeather` method (simulated) and registers it as an AI function for the agent to call.
   - Runs the agent with a weather-related prompt and prints the result.

## How to run
- Restore and run from the repo root:
  - `dotnet restore`
  - `dotnet run --project MAFDemos`
- Or open the solution in Visual Studio 2022 and run the project. The console app prompts for demo selection (1–3).

## Installing and running Ollama locally
The demos expect a local Ollama API reachable at `http://localhost:11434/`. Below are common install & setup steps.

0. Choose an install method for your OS:

- Windows (winget):
  - `winget install Ollama.Ollama`
- macOS (Homebrew):
  - `brew install ollama`
- Alternatively download the installer from the official Ollama website.

1. Pull models you plan to use (examples used in the demos):
- `ollama pull llama3.2:1b`
- `ollama pull phi3` (if you use the `phi3` model in your experiments)

2. Start the Ollama daemon / server so that the HTTP API is available:
- Run the Ollama service/daemon. Command examples:
  - `ollama serve` (or start the installed Ollama app/service)
  - Confirm the daemon is listening on port `11434`.

3. Verify models are available locally:
- `ollama list` (or `ollama ls`) to see pulled models.
- Optionally test with the CLI to generate a sample response:
  - `ollama run llama3.2:1b "Hello"` (CLI will return a generated response)

Note: If Ollama is running but you still get connection errors, verify firewall/network settings and that the server is bound to localhost.

## Troubleshooting
- If you encounter runtime errors like `MissingMethodException` referencing `Microsoft.Extensions.AI.ChatResponseUpdate.set_ContinuationToken(...)`, it's likely a package version mismatch:
  - Avoid wildcard package versions (`Version="*"`) for `Microsoft.Extensions.AI` and related packages.
  - Make sure `Microsoft.Agents.AI`, `Microsoft.Agents.AI.Workflows` and `Microsoft.Extensions.AI` are resolved to compatible versions. Pin the `Microsoft.Extensions.AI` package to the preview version that matches the agents packages (or upgrade the agent packages to match the installed extensions).
  - Steps to fix:
    - Edit `MAFDemos.csproj` to use exact versions for the AI packages.
    - `dotnet clean`, `dotnet restore`, then `dotnet build`.
    - If needed: clear nuget cache with `dotnet nuget locals all --clear` and delete `bin/obj` folders.

## Notes
- The demos are intentionally minimal to show patterns for agent creation, chaining, and tool usage. In real apps, prefer DI for client construction and robust error handling.
- If you change the model name or port in the demos, update the `OllamaApiClient` constructor URIs and model strings accordingly.
