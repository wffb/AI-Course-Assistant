## Confirmation of AI technology route

### Spring Ai framework

The `Spring AI` project aims to streamline the development of applications that incorporate artificial intelligence functionality  without unnecessary complexity.

The project draws inspiration from notable Python projects, such as  LangChain and LlamaIndex, but Spring AI is not a direct port of those  projects. The project was founded with the belief that the next wave of Generative AI applications will not be only for Python developers but will be  ubiquitous across many programming languages.

At its core, Spring AI addresses the fundamental challenge of AI integration: `Connecting your enterprise Data and APIs with the AI Models`.



### Advantages of Spring Ai

------

- Spring Ai can support different types of language models at the same time, and is also compatible with some speech-to-text models (open ai Whisper)

- Spring Boot can also provide complete identity authentication and authorization functions, and can serve as the complete backend of the system

- It can integrate development tools such as lombank and maven dependency management, making development convenient

- It has good support for modular development and supports MVC, AOP and other concepts

- The one-click packaging provided by Spring facilitates project deployment

- Spring officially provides detailed documentation and a prosperous community has produced many teaching videos/blogs

  

### Potential Achievements with Spring Ai

------

-  All-in-one system backend: Implements AI functions and  login/authentication functions for unified deployment

-  Modular  development, testing and dependency management based on the Spring Boot  system

  

### Potential Drawbacks

------

- Java itself executes slower than compiled types, so there may be some delay issues

  

### Advantages Over Other Software

------

##### Phython script

- Spring Ai provides native API packaging support, which is easier to implement than developers writing scripts by themselves

- In addition to integrating the Ai module, Spring Ai can also use the tools provided by SpringBoot to enable the project to obtain a complete login authentication/authorization solution, which is more convenient and mature than implementing it by yourself through Phython 

-  As a widely used backend framework, Spring has a large number of reference cases, and developers have more experience in implementing backend functions through Spring than through Phython

  



#### Related learning materials

------

- [Official documents](https://spring.io/projects/spring-ai#overview)

- [Official warehouse](https://github.com/spring-projects/spring-ai)



Teaching video:

- [Accessing remote language model](https://www.bilibili.com/video/BV11b421h7uX/?spm_id_from=333.788&vd_source=6ffaf197f9622fbb81ce9106978874e8)

- [Related tutorials](https://www.bilibili.com/video/BV1d1421d7Fy/?spm_id_from=333.976.0.0)



Reference projects:

- [sprint-ai-zh](https://github.com/NingNing0111/spring-ai-zh-tutorial)

- [sprint-ai](https://www.yuque.com/pgthinker/spring-ai/ztmivcnoqxp3u21z)

- [sprint-open-tut](https://blog.csdn.net/qq_41481367/article/details/138090454?spm=1001.2014.3001.5502)



### Language model

#### RemoteLLM-openai

The OpenAI API provides a simple interface to state-of-the-art AI [models](https://platform.openai.com/docs/models) for natural language processing, image generation, semantic search, and speech recognition. Follow this guide to learn how to generate  human-like responses to [natural language prompts](https://platform.openai.com/docs/guides/chat-completions), [create vector embeddings](https://platform.openai.com/docs/guides/embeddings) for semantic search, and [generate images](https://platform.openai.com/docs/guides/images) from textual descriptions.



##### Advantages of openai

------

- The provided gpt3 turbo/gpt4o data training volume is large and the performance is excellent

- The official provides a complete and convenient management platform

- The official provides a complete documentation for easy understanding and learning

- It also provides other AI services (such as speech-to-text)

  

###### Potential Achievements

------

- Calling the openai plantform AP to conveniently implemente various AI functions



###### Potential Drawbacks

------

- The official charges for the API, and there may be restrictions on usage(but considering the frequency and amount of use in this project, the cost incurred should not be high)

- Functional implementation depends on stable network conditions

  



###### Related learning materials

Official platform: https://openai.com/api/ 

Official documents: https://platform.openai.com/docs/quickstart

Chinese documents: https://openai.xiniushu.com/docs/guides/speech-to-text (with online debugging function)



#### Local-ollama/open webui



**ollama** is an open source LLM (large language model) service tool that integrates model download, management and other functions, allowing large model developers, researchers and enthusiasts to quickly experiment, manage and deploy the latest large language models in a local environment



**open webui** is a packaged interface for local big predictions. It can provide a web interface similar to chatgpt style, and can also provide openai style API results, helping developers to use big language models more conveniently



##### Advantages of Local-ollama/open webui

------

- Local LLM provides some unique advantages compared to remote APIs - data privacy, security, offline availability and reduced dependence on external services

- Meta's open source model LLaMa3 has all indicators close to GPT-4, with excellent performance

  

##### Potential Achievements

------

- Calling the openai plantform AP to conveniently implemente various AI functions



##### Potential Drawbacks

------

- Local deployment has high hardware requirements, and continuous operation requires the hardware to provide sufficient video memory, memory and heat dissipation
- Local deployment will make the module structure more complicated
- The API provided by open webui is not as perfect as the openi platform, and it may take more effort to implement some functions



###### Related learning materials

 Deployment tutorial:  

1. [https://www.cnblogs.com/obullxl/p/18295202/NTopic2024071001](https://www.cnblogs.com/obullxl/p/18295202/NTopic2024071001)  

2. [https://cloud.tencent.com/developer/article/2424184](https://cloud.tencent.com/developer/article/2424184)  

3. [https://liaoxuefeng.com/blogs/all/2024-05-06-llama3/](https://liaoxuefeng.com/blogs/all/2024-05-06-llama3/)  

4. [https://blog.csdn.net/qq_53795212/article/details/139690567](https://blog.csdn.net/qq_53795212/article/details/139690567)


