package org.example.backend.Service.Imp;

import org.example.backend.Model.TeacherInfo;
import org.example.backend.Mapper.TeacherInfoMapper;
import org.example.backend.Service.ITeacherInfoService;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.stereotype.Service;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author werb
 * @since 2024-09-30
 */
@Service
public class TeacherInfoServiceImpl extends ServiceImpl<TeacherInfoMapper, TeacherInfo> implements ITeacherInfoService {

}
