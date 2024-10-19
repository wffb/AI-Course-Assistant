package org.example.backend.Service;

import org.example.backend.Mapper.StudentMapper;
import org.example.backend.Mapper.SubjectMapper;
import org.example.backend.Model.Student;
import com.baomidou.mybatisplus.extension.service.IService;
import org.example.backend.Model.Subject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * <p>
 *  服务类
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
public interface IStudentService extends IService<Student> {

    public List<Subject> getSubject(String username);


}

