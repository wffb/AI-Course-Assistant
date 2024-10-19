using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.ResponseModels;
using System.Net;
using System.Xml;

namespace llm{

    class LLM{

        private static OpenAIService? openAIService;
        private static ChatCompletionCreateRequest? cr;
        private static ChatCompletionCreateResponse? completionResult;


        public static String? output;
        public static bool Successful = true;

        public enum STATUS {
            Success,
            GetAswException,
            GetAswFailure,

        }

        public static STATUS status = STATUS.Success;



        // private void Example(){

                //初始化
        //     Init(OpenAIService service);
               // 输入内容
       //      GetAsw ("hello")     
               //获取结果
       //      if(LLM.status == LLM.STATUS.SUCCESS)  Console.Write(output);
        // }


        //initial function
        public static void Init(OpenAIService service){

            openAIService = service;   
            cr = new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                            {

                            },
                Model = Models.Gpt_4o,
                MaxTokens = 1000//optional
            };

            completionResult = new ChatCompletionCreateResponse();
        
        }

        //getAnswer
        public static async Task GetAsw(String text){

            cr.Messages.Add(ChatMessage.FromUser(text));
         try {  

                completionResult = await openAIService.ChatCompletion.CreateCompletion(cr);
            }
        catch (Exception e)
            {   
                status = STATUS.GetAswFailure;
                Console.WriteLine(e); 
            }

        if (completionResult.Successful)
            {   
            //后续消息写入
                status = STATUS.Success;
                output = completionResult.Choices.First().Message.Content;
            }
        else{   status = STATUS.GetAswFailure;
                output = "ERROR,somethin wrong";
            }    
        }

    }
}