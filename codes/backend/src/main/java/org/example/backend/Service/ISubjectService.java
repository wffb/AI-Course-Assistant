package org.example.backend.Service;

import com.baomidou.mybatisplus.extension.service.IService;
import org.example.backend.Model.Subject;

import java.util.List; /**
 * <p>
 *  服务类
 * </p>
 *
 * @author Ev
 * @since 2024-10-06
 */
public interface ISubjectService extends IService<Subject> {
    public List<Subject> getSubjectsByIdentifyIdList(List<String> s);
    // 新增 Subject
    void addSubject(Subject subject);

    // 删除 Subject
    void deleteSubjectByIdentifyId(String identifyId);

    // 更新 Subject
    void updateSubject(Subject subject);

    // 根据 ID 查询 Subject
    Subject getSubjectByIdentifyId(String identifyId);




}


