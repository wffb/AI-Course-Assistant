package org.example.backend.Controller;

import com.fasterxml.jackson.annotation.JsonView;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.AiAgent;
import org.example.backend.Model.Param.AssistantRegisterParam;
import org.example.backend.Model.Param.AssistantUpdateParam;
import org.example.backend.Service.IAssistantService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
public class AssistantController {

    @Autowired
    IAssistantService assistantService;

    @GetMapping("/admin/assistant/getAllAssistants")
    public CommonResult<Object> getAllAssistants(){

        return CommonResult.success("Obtain assistants success!",assistantService.getAssistant());
    }

    @GetMapping("/admin/assistant/getAssistantBySubjectId")
    @JsonView(AiAgent.AgentDetailView.class)
    public CommonResult<Object> getAssistantBySubjectId(@RequestParam("subjectIdentifyId")String id){

        return CommonResult.success("Obtain assistants success!",assistantService.getAssistant(id));
    }

    @PostMapping("/admin/assistant/createAssistant")
    @JsonView(AiAgent.AgentDetailView.class)
    public CommonResult<Object> createAssistant(@RequestBody AssistantRegisterParam param){

        AiAgent aiAgent= assistantService.createAssistant(param);

        if(aiAgent==null)
            return CommonResult.failed("Assistant creation information is incomplete");
        else
            return CommonResult.success("Assistant creation success!",aiAgent);
    }

    @PostMapping("/admin/assistant/updateAssistant")
    @JsonView(AiAgent.AgentDetailView.class)
    public CommonResult<Object> updateAssistant(@RequestBody AssistantUpdateParam param){

       return assistantService.updateAssistant(param);
    }
}
