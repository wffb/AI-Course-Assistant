
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.SharedModels;
using OpenAI.Builders;
using UnityEngine;
using OpenAI.Managers;
using OpenAI;
using static OpenAI.ObjectModels.StaticValues;
using OpenAI.ObjectModels.ResponseModels;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;


namespace my_assistant
{
    class Assistant{

        
        //指令：可以为助手指定行为-角色
        // private const string Instruction = "You are my first AI assistant";
        // private const string Name = "WFFB";

        private static OpenAIService? service;

        private static string? assistantId { get; set; }
        private static string? threadId { get; set; }

        public static bool isSuccess = true;
        public static string? reply;

        public static List<Attachment>? files = new List<Attachment>();

        public static  void HelloAsiss() {
            
             Console.WriteLine("hello");
        }
        

        //create

        public static async Task Init(string key,string Id,List<string> fileIds){
            service = CreateService(key);
            // await CreateAssistant(service);

            assistantId = Id;

            //Debug.Log("assistant: ！ ", assistantId);

            //creare thread
            var threadResult = await service.Beta.Threads.ThreadCreate();
            threadId = threadResult.Id;


            //add file
            List<ToolDefinition>tools = new List<ToolDefinition>();
            tools.Add(ToolDefinition.DefineFileSearch());
    
            foreach(string id in fileIds){

                files.Add(new(){
                    FileId=id,
                    Tools=tools
                });
            }
            
            
        }

        private static OpenAIService CreateService(string key){
            
            service = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = key,
                UseBeta = true
            });

            return service;
        }
        private static async Task CreateAssistant(IOpenAIService openAI)


        {
            Console.WriteLine("Create Assistant Testing is starting:");



            var result = await openAI.Beta.Assistants.AssistantCreate(new()
            {
                Instructions = null, // Change to Instruction!!!
                Name = null, // Change to Name!!!
                // Tools = [ToolDefinition.DefineCodeInterpreter()],
                Model = Models.Gpt_4_turbo
            });

            if (result.Successful)
            {
                Console.WriteLine("assistant creation success:"+result);
                assistantId = result.Id;
            }
            else
            {
                 Console.WriteLine(result.Error);
                 assistantId = "";
            }
        }

        public static async Task Run(string words){
            
            //create message
            var msg = service.CreateMessage(
            threadId,
            new(){
                Role = AssistantsStatics.MessageStatics.Roles.User,
                Content = new(words),
                //add file
                Attachments = files
   
            }
            );

            //create run
            var runResult = await service.Beta.Runs.RunCreate(threadId, new()
            {
                AssistantId = assistantId
            });

            RunResponse run;
            do
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                //get Run
                run = await service.Beta.Runs.RunRetrieve(threadId, runResult.Id);

            } while (!(run.Status == AssistantsStatics.RunStatus.Completed || 
                    run.Status == AssistantsStatics.RunStatus.Failed || 
                    run.Status == AssistantsStatics.RunStatus.Expired
            ));

            if(run.Status == AssistantsStatics.RunStatus.Completed){
                await getRes();      
            }
            else
                isSuccess = false;
            
        }

        private static async Task getRes(){

           MessageListResponse replys = await service.Beta.Messages.ListMessages(threadId, new() { Order= "desc", Limit = 1 });

           if(replys.Successful){
                isSuccess = true;
                reply = replys.Data[0].Content[0].Text.Value;
           }
           else
                isSuccess = false; 
        }    


    }
}

