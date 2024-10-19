package org.example.backend.Model.Param;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AssistantUpdateParam {

    private String identifyId;
    private String name;
    private String model;
    private Double temperature;
    private Double topP;
    private String instruction;
    private String description;
    private String subjectId;

    public Boolean isEmpty(){
        return identifyId==null || (!isApiUpdate()&& subjectId==null );
    }

    public Boolean isApiUpdate(){
        return !(name==null && instruction==null && topP==null
                && model==null );
    }

}
