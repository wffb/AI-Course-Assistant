package org.example.backend.Service.Imp;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;

import org.example.backend.Mapper.UserMapper;
import org.example.backend.Model.Detail.LoginUser;
import org.example.backend.Model.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

/**
 * 指导Spring如何从数据库按照Username获取数据
 */
@Service
public class UserDetailServiceImp implements UserDetailsService {

    @Autowired
    private UserMapper userMapper;


    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        //根据用户名查询用户信息
        QueryWrapper<User> wrapper = new QueryWrapper<User>()
                .eq("username",username);

        User user = userMapper.selectOne(wrapper);
        //如果查询不到数据就通过抛出异常来给出提示
        if(Objects.isNull(user)){
            throw new RuntimeException("Wrong username or password");
        }

        //写入权限
        List<String>roles= new ArrayList<>();

       if(user.getIsTeacher())
           roles.add("teacher");

       roles.add("student");


        //封装成UserDetails对象返回
        return new LoginUser(user,roles);
    }
}