package org.example.backend.Mapper;

import org.apache.ibatis.annotations.Mapper;
import org.example.backend.Model.Teacher;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
@Mapper
public interface TeacherMapper extends BaseMapper<Teacher> {

}
