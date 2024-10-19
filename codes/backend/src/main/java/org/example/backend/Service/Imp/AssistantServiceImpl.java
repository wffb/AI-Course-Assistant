package org.example.backend.Service.Imp;


import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.theokanning.openai.assistants.assistant.Assistant;
import com.theokanning.openai.assistants.assistant.AssistantRequest;
import com.theokanning.openai.assistants.assistant.FileSearchTool;
import com.theokanning.openai.assistants.assistant.ModifyAssistantRequest;
import com.theokanning.openai.service.OpenAiService;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Mapper.AiAgentMapper;
import org.example.backend.Mapper.AiFileMapper;
import org.example.backend.Model.AiAgent;
import org.example.backend.Model.AiFile;
import org.example.backend.Model.Param.AssistantRegisterParam;
import org.example.backend.Model.Param.AssistantUpdateParam;
import org.example.backend.Service.IAssistantService;
import org.springframework.beans.factory.annotation.Autowired;

import java.io.File;
import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.net.URLDecoder;
import java.nio.charset.StandardCharsets;
import java.util.Collections;
import java.util.Date;
import java.util.List;


public class AssistantServiceImpl implements IAssistantService {

    @Autowired
    AiAgentMapper aiAgentMapper;
    @Autowired
    AiFileMapper aiFileMapper;
    private final OpenAiService service;

    public AssistantServiceImpl(String key){
        service = new OpenAiService(key);
    }


    @Override
    public AiAgent createAssistant(AssistantRegisterParam param) {

        if(param==null || !param.isFull())
            return null;

        AssistantRequest assistantRequest = AssistantRequest.builder()
                .model("gpt-4o")
                .name(param.getName())
                .description(param.getDescription())
                .instructions(param.getInstructions())
                //随机性
                .temperature(param.getTemperature())
                //采样权重
                .topP(param.getTopP())
                //文件搜索
                .tools(Collections.singletonList(new FileSearchTool()))
                .temperature(0D)
                .build();

        Assistant assistant = service.createAssistant(assistantRequest);

        System.out.println(assistant);

        AiAgent aiAgent = new AiAgent(
                null,
                assistant.getName(),
                assistant.getTemperature(),
                assistant.getTopP(),
                param.getSubjectId(),
                assistant.getInstructions(),
                assistant.getDescription(),
                assistant.getModel(),
                assistant.getId()
        );

        aiAgentMapper.insert(aiAgent);


        return aiAgent;
    }

    @Override
    public List<AiAgent> getAssistant(String subjectIdentityId) {
        QueryWrapper<AiAgent>wrapper = new QueryWrapper<AiAgent>()
                .eq("subject_id",subjectIdentityId);

        return aiAgentMapper.selectList(wrapper);
    }

    @Override
    public List<AiAgent> getAssistant() {

        return aiAgentMapper.selectList(null);
    }

    @Override
    public CommonResult<Object> updateAssistant(AssistantUpdateParam param) {

        if(param.isEmpty())
            return CommonResult.failed("Assistant update Information is incomplete");

        AiAgent aiAgent = new AiAgent(param);

        if(param.isApiUpdate()){
            //确认是否存在

            //获取对象
            ModifyAssistantRequest request = ModifyAssistantRequest.builder()
                    .model(param.getModel())
                    .name(param.getName())
                    .description(param.getDescription())
                    .instructions(param.getInstruction())
                    //随机性
                    .temperature(param.getTemperature())
                    //采样权重
                    .topP(param.getTopP())
                    //文件搜索
                    .tools(Collections.singletonList(new FileSearchTool()))
                    .temperature(0D)
                    .build();

            Assistant assistant= service.modifyAssistant(param.getIdentifyId(),request);

            if(assistant==null)
                return CommonResult.failed("Assistant update failed");

        }

            QueryWrapper<AiAgent>wrapper=new QueryWrapper<AiAgent>()
                    .eq("identify_id",param.getIdentifyId());

            aiAgentMapper.update(aiAgent,wrapper);

        return CommonResult.success("Assistant update success");


    }

    @Override
    public AiFile uploadFile(File upload,String assistantId) throws MalformedURLException, UnsupportedEncodingException {

        com.theokanning.openai.file.File file  = service.uploadFile(
                "assistants",
                URLDecoder.decode(upload.toURI().toURL().getFile(), StandardCharsets.UTF_8.name()));

//        URL resource = AssistantServiceImpl.class.getClassLoader().getResource("file/hello.txt");
//        com.theokanning.openai.file.File file  = service.uploadFile("assistants", URLDecoder.decode(resource.getFile(), StandardCharsets.UTF_8.name()));

        if(file==null)
            return null;

        AiFile aiFile = new AiFile(
                file.getId(),
                file.getFilename(),
                new Date(),
                null,
                assistantId
        );
        aiFileMapper.insert(aiFile);

        return aiFile;
    }


}
