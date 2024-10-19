using System;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace new_test
{
    public class OpenAIHandler
    {
        private readonly OpenAIAPI _api;

        public OpenAIHandler(string apiKey)
        {
            _api = new OpenAIAPI(apiKey);
        }

        public async Task<string> GetChatResponse(string userInput)
        {
            var chat = _api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are a helpful assistant.");
            chat.AppendUserInput(userInput);

            try
            {
                string response = await chat.GetResponseFromChatbotAsync();
                return response;
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "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";
            var handler = new OpenAIHandler(apiKey);

            Console.WriteLine("Enter your question (or 'exit' to quit):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "exit") break;

                string response = await handler.GetChatResponse(input);
                Console.WriteLine("Assistant: " + response);
                Console.WriteLine("\nEnter your next question (or 'exit' to quit):");
            }
        }
    }
}