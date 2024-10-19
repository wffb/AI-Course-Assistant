package org.example.backend.Mapper;

import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.example.backend.Model.Subject;
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
public interface SubjectMapper extends BaseMapper<Subject> {


    /**
     * 通过科目名称获取科目信息
     * @param name 科目名称
     * @return 科目对象
     */
    public Subject getByName(@Param("name") String name);

    /**
     * 通过标识ID获取科目名称
     * @param identifyId 科目标识ID
     * @return 科目名称
     */
    public String getNameByIdentifyId(@Param("identifyId") String identifyId);

    public String getNameByDescription(@Param("description") String description);
}
