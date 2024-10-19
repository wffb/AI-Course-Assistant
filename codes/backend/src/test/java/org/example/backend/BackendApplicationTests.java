package org.example.backend;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;

import org.example.backend.Mapper.UserMapper;

import org.example.backend.Model.Param.AssistantUpdateParam;
import org.example.backend.Model.User;
import org.example.backend.Service.IAssistantService;
import org.example.backend.Service.IStudentInfoService;
import org.example.backend.Service.ISubjectService;
import org.example.backend.Service.UserService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.util.ArrayList;
import java.util.List;

@SpringBootTest
class BackendApplicationTests {

    @Test
    void contextLoads() {
        System.out.println("hello");
    }

    @Autowired
    UserService userService;
    @Autowired
    UserMapper userMapper;
    @Autowired
    IAssistantService assistantService;
    @Autowired
    IStudentInfoService studentInfoService;
    @Autowired
    ISubjectService subjectService;

    @Test
    public void userTest(){

        //注册测试
//        RegisterParam param = new RegisterParam(
//                "werb","12345",true
//        );
//
//        System.out.println(userService.register(param).getMessage());

        //登录
//        LoginParam param = new LoginParam("werb","12345");
//
//        System.out.println(userService.login(param));

    }

    @Test
    public  void searchTest(){
        String username = "werb";

        QueryWrapper<User> wrapper = new QueryWrapper<User>()
                .eq("username",username);

        User user = userMapper.selectOne(wrapper);

        System.out.println(user);
    }
    @Test
    public  void subjectTest(){

        List<String>list = new ArrayList<>();
        list.add("90016");
        list.add("90014");

        System.out.println(subjectService.getSubjectsByIdentifyIdList(list));
    }

    @Test
    public void assistantTest(){

//        List<String>list= studentInfoService.getSubjectListByUserId("114514");
//        System.out.println(list);

        AssistantUpdateParam param = new AssistantUpdateParam();
        param.setIdentifyId("asst_JVhfEEGECs5oL3YEVBHn8DqP");
        param.setDescription("这是摸鱼机器人");
        param.setTopP(0.5);
        param.setTemperature(0.6);
        param.setName("摸鱼机器人三号机");


        assistantService.updateAssistant(param);


    }
    @Test
    public void fileTest() throws MalformedURLException, UnsupportedEncodingException {
//        URL resource = AssistantServiceImpl.class.getClassLoader().getResource("file/hello.txt");
//
//        if(resource!=null)
//            System.out.println(resource);
//        else
//            System.out.println("file can not find");



    }




}
