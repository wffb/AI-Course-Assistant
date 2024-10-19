package org.example.backend.Service.Imp;

import org.example.backend.Model.Teacher;
import org.example.backend.Mapper.TeacherMapper;
import org.example.backend.Service.ITeacherService;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.stereotype.Service;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
@Service
public class TeacherServiceImpl extends ServiceImpl<TeacherMapper, Teacher> implements ITeacherService {

}
