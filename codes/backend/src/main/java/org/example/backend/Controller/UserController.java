package org.example.backend.Controller;

import com.fasterxml.jackson.annotation.JsonView;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.Param.LoginParam;
import org.example.backend.Model.Param.RegisterParam;
import org.example.backend.Model.User;
import org.example.backend.Service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.Map;

@RestController
public class UserController {


    @Autowired
    private UserService userService;


    //无需权限
    @PostMapping("/login")
    @JsonView(User.UserSimpleView.class)
    public CommonResult<Map<String,Object>> login(@RequestBody LoginParam param){

        if(param==null|| param.isEmpty())
            return  CommonResult.failed("User information cannot be empty");

        return CommonResult.success("Login Success!",userService.login(param));
    }


    @PostMapping("/register")
    @JsonView(User.UserSimpleView.class)
    public   CommonResult<RegisterParam>  register(@RequestBody RegisterParam param){

        if(param==null|| param.isEmpty())
            return  CommonResult.failed("User information cannot be empty");

        return userService.register(param);

    }

    @JsonView(User.UserDetailView.class)
    @GetMapping("/admin/getUserList")
    public CommonResult<Object> getUserList(){

        return  CommonResult.success("get user list success!",userService.list());
    }


    @PreAuthorize("hasAnyAuthority('teacher')")
    @GetMapping("/something")
    public  void  something(){

    }

    @PreAuthorize("hasAnyAuthority('student')")
    @GetMapping("/something2")
    public  void  something2(){

    }




}
