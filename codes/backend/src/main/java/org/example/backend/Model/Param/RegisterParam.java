package org.example.backend.Model.Param;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor

public class RegisterParam {

    private String username;

    private String password;

    private Boolean isTeacher;

    public boolean isEmpty(){
        return username==null||password==null;
    }

}
