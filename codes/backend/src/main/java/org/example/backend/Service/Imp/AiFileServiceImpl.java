package org.example.backend.Service.Imp;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import org.example.backend.Model.AiFile;
import org.example.backend.Mapper.AiFileMapper;
import org.example.backend.Service.IAiFileService;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
@Service
public class AiFileServiceImpl extends ServiceImpl<AiFileMapper, AiFile> implements IAiFileService {

    @Autowired
    AiFileMapper aiFileMapper;

    @Override
    public List<String> getByAssistantId(String id) {

        QueryWrapper<AiFile> wrapper = new QueryWrapper<AiFile>()
                .eq("assistant_id",id);

        return aiFileMapper.selectList(wrapper).stream()
                .map(AiFile::getId)
                .collect(Collectors.toList());
    }
}
