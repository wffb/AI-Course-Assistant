// using OpenAI;
// using OpenAI.Managers;
// using OpenAI.ObjectModels.RequestModels;
// using OpenAI.ObjectModels;
// using OpenAI.ObjectModels.ResponseModels;
// using System.Net;
// using System.Xml;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using System;
// using System.Linq;

// namespace llm{

//     class LLM{

//         private static OpenAIService? openAIService;
//         private static ChatCompletionCreateRequest? cr;
//         private static ChatCompletionCreateResponse? completionResult;


//         public static string? output;
//         public static bool Successful = true;

//         public enum STATUS {
//             Success,
//             GetAswException,
//             GetAswFailure,

//         }

//         public static STATUS status = STATUS.Success;



//         // private void Example(){

//                 //初始化
//         //     Init(OpenAIService service);
//                // 输入内容
//        //      GetAsw ("hello")     
//                //获取结果
//        //      if(LLM.status == LLM.STATUS.SUCCESS)  Console.Write(output);
//         // }


//         //initial function
//         public static void Init(string key){

//             openAIService = getService(key);   
//             cr = new ChatCompletionCreateRequest
//             {
//                 Messages = new List<ChatMessage>
//                             {

//                             },
//                 Model = Models.Gpt_4o,
//                 MaxTokens = 1000//optional
//             };

//             completionResult = new ChatCompletionCreateResponse();
        
//         }

//         //getAnswer
//         public static async Task<string> GetAsw(string text){

//             cr.Messages.Add(ChatMessage.FromUser(text));
//          try {  

//                 completionResult = await openAIService.ChatCompletion.CreateCompletion(cr);
//             }
//         catch (Exception e)
//             {   
//                 status = STATUS.GetAswFailure;
//                 Console.WriteLine(e); 
//             }

//         if (completionResult.Successful)
//             {   
//             //后续消息写入
//                 status = STATUS.Success;
//                 return completionResult.Choices.First().Message.Content;
//             }
//         else{   status = STATUS.GetAswFailure;
//                 return "ERROR,somethin wrong";
//             }    
//         }

//         private static OpenAIService getService(string key)
//     {
//         // var handler = new HttpClientHandler()//这里使用了代理
//         // {
//         //     Proxy = new WebProxy("192.168.1.67", 1082),
//         //     UseProxy = true
//         // };
        
//         // var client = new HttpClient(handler);
 
//         // var openAiService = new OpenAIService(new OpenAiOptions()
//         // {
//         //     ApiKey = "your key",  /*哦对了，我这里只是简单的设置了一下apikey,建议采用依赖注入，这样更安全*/
//         // }, client);

//         var serviceOption = new OpenAiOptions()
//         {
//             ApiKey = key,
//             UseBeta = true
//         };

//         var openAiService = new OpenAIService(serviceOption);

//         return openAiService;
//     }

//     }

    
// }