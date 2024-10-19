package org.example.backend.Service.Imp;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Common.util.JwtUtil;
import org.example.backend.Model.Detail.LoginUser;
import org.example.backend.Model.Param.LoginParam;
import org.example.backend.Model.Param.RegisterParam;
import org.example.backend.Model.User;
import org.example.backend.Mapper.UserMapper;
import org.example.backend.Service.UserService;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;
import java.util.Objects;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */
@Service
public class UserServiceImpl extends ServiceImpl<UserMapper, User> implements UserService {

    @Autowired
    UserMapper userMapper;

    @Autowired
    PasswordEncoder passwordEncoder;
    @Autowired
    private AuthenticationManager authenticationManager;
    @Autowired
    JwtUtil jwtUtil;


    @Override
    public CommonResult<RegisterParam> register(RegisterParam param) {
        //判断重名
        if(userExist(param.getUsername()))
            return CommonResult.failed("The current username already exists");

        //存入用户
        User user = new User(param);
        String pwd = user.getPassword();
        //密码加密存储
        user.setPassword(passwordEncoder.encode(pwd));
        userMapper.insert( user);

        return CommonResult.success("The user registration is successful, please complete the personal information as soon as possible",null);
    }

    @Override
    public Map<String, Object> login(LoginParam param) {

        System.out.println("开始登录");

        //封装用户信息
        UsernamePasswordAuthenticationToken authenticationToken = new UsernamePasswordAuthenticationToken(param.getUsername(),param.getPassword());
        //调用进行检查
        Authentication authenticate = authenticationManager.authenticate(authenticationToken);
        if(Objects.isNull(authenticate)){
            throw new RuntimeException("Wrong username or password");
        }
        //使用userid生成token
        LoginUser loginUser = (LoginUser) authenticate.getPrincipal();
        //authenticate存入redis


        String jwt = jwtUtil.generateToken(loginUser,loginUser.getUser().getIsTeacher());


        //把token响应给前端
        HashMap<String,Object> map = new HashMap<>();
        map.put("token",jwt);
        map.put("isTeacher",loginUser.getUser().getIsTeacher().toString());
        map.put("user",loginUser.getUser());


        return map;
    }


    public boolean userExist(String name) {
        if(name==null) return false;
        //数据库查找
        QueryWrapper<User>queryWrapper = new QueryWrapper<User>().eq("username",name);
        return !Objects.isNull(userMapper.selectOne(queryWrapper));
    }
}
