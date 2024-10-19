package org.example.backend.Service.Imp;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import org.example.backend.Model.Subject;
import org.example.backend.Mapper.SubjectMapper;
import org.example.backend.Service.ISubjectService;
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
public class SubjectServiceImpl extends ServiceImpl<SubjectMapper, Subject> implements ISubjectService {

    @Autowired
    SubjectMapper mapper;

    @Override
    public List<Subject> getSubjectsByIdentifyIdList(List<String> s) {
        int len=s.size();
        QueryWrapper<Subject>wrapper=new QueryWrapper<Subject>();

        if(len==0)
            return null;
        else if(len==1)
            wrapper.eq("identify_id",s.get(0));
        else{
            for(String id : s){
                wrapper.or().eq("identify_id",id);
            }
        }

        return mapper.selectList(wrapper);
    }

    // 新增 Subject
    @Override
    public void addSubject(Subject subject) {
        mapper.insert(subject);  // MyBatis Plus 提供的插入方法
    }

    // 删除 Subject
    @Override
    public void deleteSubjectByIdentifyId(String identifyId) {

        LambdaQueryWrapper<Subject> queryWrapper = new LambdaQueryWrapper<>();
        queryWrapper.eq(Subject::getIdentifyId, identifyId);  // 根据 identifyId 查询
        this.remove(queryWrapper);  // MyBatis-Plus 提供的删除方法
    }

    // 更新 Subject
    @Override
    public void updateSubject(Subject subject) {
        UpdateWrapper<Subject> updateWrapper = new UpdateWrapper<>();
        updateWrapper.eq("id", subject.getId());  // 条件为 identifyId

        // 执行更新操作，只更新非空的字段
        mapper.update(subject, updateWrapper);
    }

    // 根据 ID 查询 Subject
    @Override
    public Subject getSubjectByIdentifyId(String identifyId) {
        LambdaQueryWrapper<Subject> queryWrapper = new LambdaQueryWrapper<>();
        queryWrapper.eq(Subject::getIdentifyId, identifyId);  // 使用 identifyId 作为查询条件
        return this.getOne(queryWrapper);  // MyBatis Plus 提供的根据 ID 查询方法
    }
}

