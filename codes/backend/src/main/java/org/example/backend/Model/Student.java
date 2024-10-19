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
@TableName("student")
public class Student implements Serializable {

    private static final long serialVersionUID = 1L;

    @TableId(value = "id", type = IdType.ASSIGN_UUID)
    private String id;

    private String name;

    private String password;

    private String email;

    private String subjects;




}


