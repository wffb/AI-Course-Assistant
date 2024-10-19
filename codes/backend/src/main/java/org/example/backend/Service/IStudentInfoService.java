package org.example.backend.Service;

import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.Param.StudentInfoParam;
import org.example.backend.Model.StudentInfo;
import com.baomidou.mybatisplus.extension.service.IService;

import java.util.List;

/**
 * <p>
 *  服务类
 * </p>
 *
 * @author werb
 * @since 2024-10-04
 */
public interface IStudentInfoService extends IService<StudentInfo> {

    public List<String> getSubjectListByUserId(String id);

    public List<String>

    getSubjectListByUsername(String name);

    // 新增学生信息
    public CommonResult<Object> addStudentInfo(StudentInfoParam param);  // 新增方法

}
