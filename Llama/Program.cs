using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

// Configure the Semantic kernel
var kernelBuilder = Kernel.CreateBuilder();

#pragma warning disable SKEXP0010
var kernel = kernelBuilder
    .AddOpenAIChatCompletion(
        modelId: "phi3:medium",
        apiKey: null,
        endpoint: new Uri("http://localhost:11434"))
    .Build();

// Create the chat service
var aiModel = kernel.GetRequiredService<IChatCompletionService>();

// Conversation loop
while (true)
{
    Console.Write("Your question: ");
    var question = Console.ReadLine();

    // Stream the response back async
    await foreach (var message in aiModel.GetStreamingChatMessageContentsAsync(question, kernel: kernel))
    {
        Console.Write(message);
    }
    Console.WriteLine();
}