package org.example.backend.Mapper;

import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.example.backend.Model.Student;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author Ev
 * @since 2024-10-06
 */
@Mapper
public interface StudentMapper extends BaseMapper<Student> {

    /**
     * 通过学生姓名获取学生信息
     * @param name 学生姓名
     * @return 学生对象
     */
    public Student getByName(@Param("name") String name);

    /**
     * 通过学生邮箱获取学生ID
     * @param email 学生邮箱
     * @return 学生ID
     */
    public String getIdByEmail(@Param("email") String email);

}
