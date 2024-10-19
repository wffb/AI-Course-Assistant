package org.example.backend.Config;

import com.theokanning.openai.service.OpenAiService;
import org.example.backend.Service.IAssistantService;
import org.example.backend.Service.Imp.AssistantServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class AssistantConfig {

    @Value("${assistant.api}")
    public  static String apiKey;


    @Bean("assistantService")
    IAssistantService assistantService(){

        System.out.println("key:"+apiKey);
        return new AssistantServiceImpl(
                "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"
        );
    }

}
