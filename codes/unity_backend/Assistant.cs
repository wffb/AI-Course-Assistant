using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.ResponseModels;
using System.Net;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.SharedModels;
using CsvHelper.Configuration.Attributes;
using System.Security;
using OpenAI.Builders;


namespace my_assistant
{
    class My_assistant{

        
        //指令：可以为助手指定行为-角色
        private const string Instruction = "You are my first AI assistant";
        private const string Name = "WFFB";
        private static string? CreatedAssistantId { get; set; }

        public static  void HelloAsiss() {
            
             Console.WriteLine("hello");
        }
        

        //create
         public static async Task CreateAssistant(IOpenAIService openAI)
        {
            Console.WriteLine("Create Assistant Testing is starting:");

        

            var result = await openAI.Beta.Assistants.AssistantCreate(new()
            {
                Instructions = Instruction,
                Name = Name,
                Tools = [ToolDefinition.DefineCodeInterpreter()],
                Model = Models.Gpt_4_turbo
            });

            if (result.Successful)
            {
                CreatedAssistantId = result.Id;
                 Console.WriteLine($"Assistant Created Successfully with ID: {result.Id}", ConsoleColor.Green);
            }
            else
            {
                 Console.WriteLine(result.Error);
            }
        }


        public static async Task AllTest(IOpenAIService sdk){

             Console.WriteLine("Run create Testing is starting:", ConsoleColor.Cyan);
        try
        {   

            Console.WriteLine("Run Create Test:", ConsoleColor.DarkCyan);

            //创建线程
            var threadResult = await sdk.Beta.Threads.ThreadCreate();
            var threadId = threadResult.Id;
                //添加功能
            var func = new FunctionDefinitionBuilder("get_corp_location", "get location of corp").AddParameter("name", PropertyDefinition.DefineString("company name, e.g. Betterway")).Validate().Build();
            
            //创建助手
            var assistantResult = await sdk.Beta.Assistants.AssistantCreate(new()
            {
                Instructions = "You are a professional assistant who provides company information. Company-related data comes from uploaded questions and does not provide vague answers, only clear answers.",
                Name = "Qicha",
                Tools = new() { ToolDefinition.DefineCodeInterpreter(), ToolDefinition.DefineFileSearch(), ToolDefinition.DefineFunction(func) },
                Model = Models.Gpt_3_5_Turbo_1106
            });
            
            //创建运行
            var runResult = await sdk.Beta.Runs.RunCreate(threadId, new()
            {
                AssistantId = assistantResult.Id
            });
            if (runResult.Successful)
            {
                Console.WriteLine(runResult);
            }
            else
            {
                if (runResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{runResult.Error.Code}: {runResult.Error.Message}");
            }

            var runId = runResult.Id;
            Console.WriteLine($"runId: {runId}");

            var doneStatusList = new List<string>()
                { StaticValues.AssistantsStatics.RunStatus.Cancelled, StaticValues.AssistantsStatics.RunStatus.Completed, StaticValues.AssistantsStatics.RunStatus.Failed, StaticValues.AssistantsStatics.RunStatus.Expired };
            var runStatus = StaticValues.AssistantsStatics.RunStatus.Queued;
            var attemptCount = 0;
            var maxAttempts = 10;

            do
            {   
                //绑定线程
                var runRetrieveResult = await sdk.Beta.Runs.RunRetrieve(threadId, runId);
                runStatus = runRetrieveResult.Status;
                if (doneStatusList.Contains(runStatus))
                {
                    break;
                }

                var requireAction = runRetrieveResult.RequiredAction;
                if (runStatus == StaticValues.AssistantsStatics.RunStatus.RequiresAction && requireAction.Type == StaticValues.AssistantsStatics.RequiredActionTypes.SubmitToolOutputs)
                {
                    var toolCalls = requireAction.SubmitToolOutputs.ToolCalls;
                    foreach (var toolCall in toolCalls)
                    {
                        Console.WriteLine($"ToolCall:{toolCall}");
                        if (toolCall.FunctionCall == null) return;

                        var funcName = toolCall.FunctionCall.Name;
                        if (funcName == "get_corp_location")
                        {
                            await sdk.Beta.Runs.RunCancel(threadId, runRetrieveResult.Id);
                            // Do submit tool
                        }
                    }
                }

                await Task.Delay(1000);
                attemptCount++;
                if (attemptCount >= maxAttempts)
                {
                    throw new("The maximum number of attempts has been reached.");
                }
            } while (!doneStatusList.Contains(runStatus));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        }

    }
}

