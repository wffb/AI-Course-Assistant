# How to Use OpenAI Assistant API with C#

To use OpenAI's Assistant API in C#, follow these steps:

## 1. Install SDK

First, install the official OpenAI C# SDK. You can install it via NuGet package manager:

```
dotnet add package OpenAI
```

## 2. Import Namespaces

Import the necessary namespaces in your code:

```csharp
using OpenAI_API;
using OpenAI_API.Assistants;
```

## 3. Create API Client

Create an instance of the OpenAIAPI client:

```csharp
var api = new OpenAIAPI("Your API Key");
```

## 4. Create Assistant

Create or retrieve an Assistant:

```csharp
var assistant = await api.Assistants.CreateAsync(new AssistantCreateRequest
{
    Name = "My Assistant",
    Instructions = "You are a helpful assistant.",
    Model = "gpt-4-1106-preview"
});
```

## 5. Create Thread

Create a Thread:

```csharp
var thread = await api.Threads.CreateAsync();
```

## 6. Add Message

Add a message to the Thread:

```csharp
await api.Threads.Messages.CreateAsync(thread.Id, new MessageCreateRequest
{
    Role = "user",
    Content = "Hello, can you explain quantum computing to me?"
});
```

## 7. Run Assistant

Run the Assistant:

```csharp
var run = await api.Threads.Runs.CreateAsync(thread.Id, new RunCreateRequest
{
    AssistantId = assistant.Id
});
```

## 8. Get Results

Wait for the run to complete and get the results:

```csharp
while (run.Status == "queued" || run.Status == "in_progress")
{
    await Task.Delay(1000);
    run = await api.Threads.Runs.RetrieveAsync(thread.Id, run.Id);
}

var messages = await api.Threads.Messages.ListAsync(thread.Id);
foreach (var message in messages.Items)
{
    if (message.Role == "assistant")
    {
        Console.WriteLine(message.Content);
    }
}
```
