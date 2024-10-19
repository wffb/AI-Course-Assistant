package org.example.backend.Service;

import org.example.backend.Model.AiFile;
import com.baomidou.mybatisplus.extension.service.IService;

import java.util.List;

/**
 * <p>
 *  服务类
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
public interface IAiFileService extends IService<AiFile> {

    public List<String> getByAssistantId(String id);
}
