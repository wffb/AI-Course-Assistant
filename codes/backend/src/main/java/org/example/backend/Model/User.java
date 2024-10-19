package org.example.backend.Model;

import com.baomidou.mybatisplus.annotation.TableName;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import java.io.Serializable;

import com.fasterxml.jackson.annotation.JsonView;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.Param.RegisterParam;

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
@TableName("user")
public class User implements Serializable {

    //视图控制
    public interface UserSimpleView extends CommonResult.CommonResultView{};
    public interface UserDetailView extends UserSimpleView {};



    private static final long serialVersionUID = 1L;
    @JsonView(UserSimpleView.class)
    private String username;

    @JsonView(UserDetailView.class)
    private String password;

    @JsonView(UserSimpleView.class)
    private Boolean isTeacher;

    @JsonView(UserDetailView.class)
    private String infoId;

    @TableId(value = "id", type = IdType.ASSIGN_UUID)
    @JsonView(UserDetailView.class)
    private String id;


    //字段转换
    public User(RegisterParam param){
        this.isTeacher = param.getIsTeacher();
        this.password = param.getPassword();
        this.username = param.getUsername();
    }


}
