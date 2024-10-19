package org.example.backend.Model.Param;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AssistantRegisterParam {

    private String name;
    private String model;
    private Double temperature;
    private Double topP;
    private String instructions;
    private String description;
    private String subjectId;

    public Boolean isFull(){
        return !(name==null || instructions==null || subjectId==null
                || model==null  );
    }

}
