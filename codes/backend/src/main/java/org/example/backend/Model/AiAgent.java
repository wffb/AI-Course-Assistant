package org.example.backend.Model;

import com.baomidou.mybatisplus.annotation.TableName;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import java.io.Serializable;

import com.fasterxml.jackson.annotation.JsonView;
import com.theokanning.openai.assistants.assistant.Assistant;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.Param.AssistantUpdateParam;

/**
 * <p>
 * 
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
@Data
@AllArgsConstructor
@NoArgsConstructor

@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("ai_agent")
public class AiAgent implements Serializable {

    private static final long serialVersionUID = 1L;
    public interface AgentSimpleView extends CommonResult.CommonResultView{};
    public interface AgentDetailView extends AgentSimpleView {};

    @TableId(value = "id", type = IdType.ASSIGN_UUID)
    @JsonView(AgentDetailView.class)
    private String id;

    @JsonView(AgentSimpleView.class)
    private String name;

    @JsonView(AgentSimpleView.class)
    private Double temperature=1.0;

    @JsonView(AgentSimpleView.class)
    private Double topP=1.0;

    @JsonView(AgentSimpleView.class)
    private String subjectId;

    @JsonView(AgentSimpleView.class)
    private String instructions;

    @JsonView(AgentSimpleView.class)
    private String description;

    @JsonView(AgentSimpleView.class)
    private String model;

    @JsonView(AgentSimpleView.class)
    private String identifyId;


//    public AiAgent(AssistantParam param){
//
//
//
//    }
//
    public AiAgent(Assistant assistant){

        this.name=assistant.getName();
        this.model=assistant.getModel();
        this.description=assistant.getDescription();
        this.temperature=assistant.getTemperature();
        this.topP=assistant.getTopP();
        this.instructions=assistant.getInstructions();
    }
    public AiAgent(AssistantUpdateParam param){
        this.name=param.getName();
        this.model=param.getModel();
        this.description=param.getDescription();
        this.temperature=param.getTemperature();
        this.topP=param.getTopP();
        this.subjectId=param.getSubjectId();
        this.instructions=param.getInstruction();
    }



}
