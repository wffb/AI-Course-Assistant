// using OpenAI;
// using OpenAI.Managers;
// using OpenAI.ObjectModels.RequestModels;
// using OpenAI.ObjectModels;
// using OpenAI.ObjectModels.ResponseModels;
// using System.Net;
// using System;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using System.Linq;

// namespace new_test{

//     class Test{
//                 public static bool PingTest()
//         {
//             System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
 
//             System.Net.NetworkInformation.PingReply pingStatus =
//                 ping.Send(IPAddress.Parse("208.69.34.231"),1000);
 
//             if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
//             {   
//                 Console.WriteLine("连接成功");
//                 return true;
//             }
//             else
//             {   
//                 Console.WriteLine("连接失败");
//                 return false;
//             }
//         }
//         public static bool WebRequestTest()
//         {
//             string url = "https://api.openai.com";
//             try
//             {
//                 System.Net.WebRequest myRequest = WebRequest.Create(url);
//                 System.Net.WebResponse myResponse = myRequest.GetResponse();
                
//             }
//             catch (System.Net.WebException e)
//             {   

//                  Console.WriteLine(e);
//                 return false;
//             }

//             Console.WriteLine("连接成功");
//             return true;
//         }
//     }


//     class Gpt{


//         static async Task Main(string[] args){
//         Console.WriteLine("****************************");
//         OpenAIService openAIService = GetService();
       
//         ChatCompletionCreateResponse completionResult = new ChatCompletionCreateResponse();

//         //初次问题
//         Console.Write("Q: ");
//         var msg = Console.ReadLine();

//         ChatCompletionCreateRequest cr = new ChatCompletionCreateRequest
//         {
//             Messages = new List<ChatMessage>
//                             {
//                                 //ChatMessage.FromSystem("You are a helpful assistant."),
//                                 //ChatMessage.FromUser("Who won the world series in 2020?"),
//                                 //ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
//                                 ChatMessage.FromUser(msg)
//                             },
//             Model = Models.ChatGpt3_5Turbo,
//             MaxTokens = 1000//optional
//         };

//             Console.WriteLine("Start connection");
//             await GetResult(openAIService, completionResult, cr);//注意，这里使用异步，我们下文再说
//             Console.WriteLine("connection end");


        
//         //Task.WaitAll(Task.Delay(1000));

//     //    WebRequestTest();
//     }
 
// //这里定义一个异步方法，因为原库的openAIService.ChatCompletion.CreateCompletion()是一个异步方法，我在原库的GitHub社区看到有人建议在方法名后面加一个async强调异步，看来不少人中招了，把它当同步方法用，哈哈哈。
//     private static async Task GetResult(OpenAIService openAIService, ChatCompletionCreateResponse completionResult, ChatCompletionCreateRequest cr)
//     {
//         try
//         {   
//             completionResult = await openAIService.ChatCompletion.CreateCompletion(cr);
//         }
//         catch (Exception e)
//         {
 
//             Console.WriteLine(e);
//         }
//         if (completionResult.Successful)
//         {   
//             //后续消息写入
//             Console.WriteLine("A:"+completionResult.Choices.First().Message.Content);
//             Console.Write("Q:");
//             cr.Messages.Add(ChatMessage.FromUser(Console.ReadLine()));
            
//             //维持现有会话不变，继续问答
//             await GetResult(openAIService, completionResult,cr);
//         }
//     }
 
//     public static OpenAIService GetService()
//     {
//         // var handler = new HttpClientHandler()//这里使用了代理
//         // {
//         //     Proxy = new WebProxy("192.168.1.67", 1082),
//         //     UseProxy = true
//         // };
        
//         // var client = new HttpClient(handler);
 
//         // var openAiService = new OpenAIService(new OpenAiOptions()
//         // {
//         //     ApiKey = "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",  /*哦对了，我这里只是简单的设置了一下apikey,建议采用依赖注入，这样更安全*/
//         // }, client);

//         var openAiService = new OpenAIService(new OpenAiOptions()
//         {
//             ApiKey = ""
            
//         });

//         return openAiService;
//     }
//     }

//     //     class Run
//     // {
//     //     static void Main(string[] args)
//     //     {
//     //         Gpt gpt = new();
//     //         gpt.
//     //     }
//     // }


// };






