package org.example.backend.Service.Imp;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import org.example.backend.Mapper.SubjectMapper;
import org.example.backend.Mapper.UserMapper;
import org.example.backend.Model.Student;
import org.example.backend.Mapper.StudentMapper;
import org.example.backend.Model.Subject;
import org.example.backend.Model.User;
import org.example.backend.Service.IStudentService;
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
 * @since 2024-09-22
 */
@Service
public class StudentServiceImpl extends ServiceImpl<StudentMapper, Student> implements IStudentService {

    @Autowired
    UserMapper userMapper;
    @Autowired
    SubjectMapper subjectMapper;

    @Override
    public List<Subject> getSubject(String username) {

        QueryWrapper<User>wrapper = new QueryWrapper<User>()
                .eq("username",username);

        User user = userMapper.selectOne(wrapper);

        if(user==null) return null;

        return null;


    }
    private StudentMapper studentMapper;

    // 获取所有学生
    public List<Student> getAllStudents() {
        return studentMapper.selectList(null);
    }

    // 通过ID获取学生
    public Student getStudentById(String id) {
        return studentMapper.selectById(id);
    }

    // 新增学生
    public void addStudent(Student student) {
        studentMapper.insert(student);
    }

    // 更新学生信息
    public void updateStudent(Student student) {
        studentMapper.updateById(student);
    }

    // 删除学生
    public void deleteStudent(String id) {
        studentMapper.deleteById(id);
    }

    // 通过姓名获取学生
    public Student getStudentByName(String name) {
        return studentMapper.getByName(name);
    }
}
