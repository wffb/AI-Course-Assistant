package org.example.backend.Mapper;

import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.example.backend.Model.StudentInfo;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author werb
 * @since 2024-10-04
 */
@Mapper
public interface StudentInfoMapper extends BaseMapper<StudentInfo> {

    // 根据userId获取学生信息
    public StudentInfo getByUserId(@Param("userId") String userId);
}
