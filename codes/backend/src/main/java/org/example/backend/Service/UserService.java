package org.example.backend.Service;

import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.Param.LoginParam;
import org.example.backend.Model.Param.RegisterParam;
import org.example.backend.Model.User;
import com.baomidou.mybatisplus.extension.service.IService;

import java.util.Map;

/**
 * <p>
 *  服务类
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
public interface UserService extends IService<User> {

    public CommonResult<RegisterParam> register(RegisterParam param);

    public Map<String,Object> login(LoginParam param);
}
