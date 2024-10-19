package org.example.backend.Controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Mapper.StudentInfoMapper;
import org.example.backend.Model.Param.StudentInfoParam;
import org.example.backend.Model.StudentInfo;
import org.example.backend.Service.IStudentInfoService;
import org.example.backend.Service.IStudentInfoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;

@RestController
public class StudentInfoController {

    @Autowired
    private IStudentInfoService studentInfoService;
    @Autowired
    private StudentInfoMapper mapper;

    // 新增学生信息
    @PostMapping("/admin/addStuToSub")
    public CommonResult<Object> addStudentInfo(@RequestBody StudentInfoParam param) {

        return studentInfoService.addStudentInfo(param);
    }

    // 其他接口实现...
}