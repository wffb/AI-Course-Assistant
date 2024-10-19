package org.example.backend.Service.Imp;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Common.util.ColUtil;
import org.example.backend.Mapper.UserMapper;
import org.example.backend.Model.Param.StudentInfoParam;
import org.example.backend.Model.StudentInfo;
import org.example.backend.Mapper.StudentInfoMapper;
import org.example.backend.Model.User;
import org.example.backend.Service.IStudentInfoService;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author werb
 * @since 2024-10-04
 */
@Service
public class StudentInfoServiceImpl extends ServiceImpl<StudentInfoMapper, StudentInfo> implements IStudentInfoService {

    @Autowired
    private StudentInfoMapper mapper;
    @Autowired
    private UserMapper userMapper;




    @Override
    public List<String> getSubjectListByUserId(String id) {

        QueryWrapper<StudentInfo>wrapper=new QueryWrapper<StudentInfo>()
                .eq("user_id",id);

        StudentInfo studentInfo = mapper.selectOne(wrapper);

        if(studentInfo==null)
            return null;

        return ColUtil.getClip(studentInfo.getSubjects());
    }

    @Override
    public List<String> getSubjectListByUsername(String name) {

        if(name==null) return null;
        String id = userMapper.getIdByUsername(name);
        if (id==null)  return null;

        return getSubjectListByUserId(id);
    }

    // 新增学生信息
    @Override
    public CommonResult<Object> addStudentInfo(StudentInfoParam param) {

        // 插入新的学生信息
        QueryWrapper<User> userQueryWrapper  = new QueryWrapper<User>()
                .eq("id",param.getStudentId());
        User user = userMapper.selectOne(userQueryWrapper);

        if(user==null)
            return  CommonResult.failed("user does not exist");

        QueryWrapper<StudentInfo> wrapper = new QueryWrapper<StudentInfo>()
                .eq("user_id",param.getStudentId());

        StudentInfo info = mapper.selectOne(wrapper);

        if(info==null){

            StudentInfo studentInfo = new StudentInfo();
            studentInfo.setSubjects(param.getSubjectIdentifyId());
            studentInfo.setUserId(param.getStudentId());

            mapper.insert(studentInfo);
            return  CommonResult.success("Insert subject Success!");
        }else {

            if(info.getSubjects().contains(param.getSubjectIdentifyId()))
                return CommonResult.failed("Subject has already been added");

            info.setSubjects(info.getSubjects()+"/"+param.getSubjectIdentifyId());
            mapper.updateById(info);

            return CommonResult.success("Insert subject Success!");

        }
    }
}

