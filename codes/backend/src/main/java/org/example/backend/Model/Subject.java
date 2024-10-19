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
@TableName("subject")
public class Subject implements Serializable {

    //视图控制
    public interface SubjectSimpleView extends CommonResult.CommonResultView{};
    public interface SubjectDetailView extends SubjectSimpleView {};

    private static final long serialVersionUID = 1L;

    @TableId(value = "id", type = IdType.ASSIGN_UUID)
    @JsonView(SubjectDetailView.class)
    private String id;

    @JsonView(SubjectSimpleView.class)
    private String name;

    @JsonView(SubjectSimpleView.class)
    private String identifyId;


    @JsonView(SubjectSimpleView.class)
    private String description;

}
