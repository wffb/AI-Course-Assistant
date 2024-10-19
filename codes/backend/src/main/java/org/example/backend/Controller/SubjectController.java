package org.example.backend.Controller;

import com.fasterxml.jackson.annotation.JsonView;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Common.util.JwtUtil;
import org.example.backend.Model.Subject;
import org.example.backend.Service.IStudentInfoService;
import org.example.backend.Service.ISubjectService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;
import java.util.List;

@RestController
public class SubjectController {

    @Autowired
    JwtUtil jwtUtil;
    @Value("${jwt.tokenHeader}")
    private String tokenHeader;


    @Autowired
    ISubjectService subjectService;
    @Autowired
    IStudentInfoService studentInfoService;

    @GetMapping("/assistant/getSubjects")
    @JsonView(Subject.SubjectSimpleView.class)
    public CommonResult<Object> getSubjects(HttpServletRequest httpRequest){

        String token=httpRequest.getHeader(tokenHeader);
        String name=jwtUtil.getUsernameFromToken(token);

        List<String> names = studentInfoService.getSubjectListByUsername(name);

        if(names==null || names.isEmpty())
            return CommonResult.failed("Subjects get failed");
        return CommonResult.success("Subjects get success!",subjectService.getSubjectsByIdentifyIdList(names));

    }

    @GetMapping("/admin/assistant/getSubjectsByUserId")
    @JsonView(Subject.SubjectSimpleView.class)
    public CommonResult<Object> adminGetSubjects(@RequestParam("username")String username){

        List<String> names = studentInfoService.getSubjectListByUsername(username);

        if(names==null || names.isEmpty())
            return CommonResult.failed("Subjects get failed");

        return CommonResult.success("Subjects get success!",names);

    }

    @GetMapping("/admin/assistant/getSubjects")
    @JsonView(Subject.SubjectDetailView.class)
    public CommonResult<Object> getSubjects(){

        return CommonResult.success("Subjects get success!",subjectService.list());

    }

    @PostMapping("/admin/subject/add")
    public CommonResult<Object> addSubject(@RequestBody Subject subject) {
        try {
            subject.setId(null);
            subjectService.addSubject(subject);  // 使用 service 层的 add 方法
            return CommonResult.success("Subject added successfully!");
        } catch (Exception e) {
            return CommonResult.failed("Failed to add subject: " + e.getMessage());
        }
    }

    // 新增代码：删除 Subject
    @DeleteMapping("/admin/subject/delete/{identifyId}")
    public CommonResult<Object> deleteSubject(@PathVariable String identifyId) {
        try {
            subjectService.deleteSubjectByIdentifyId(identifyId);  // 使用 service 层的 delete 方法
            return CommonResult.success("Subject deleted successfully!");
        } catch (Exception e) {
            return CommonResult.failed("Failed to delete subject: " + e.getMessage());
        }
    }

    // 新增代码：更新 Subject
    @PutMapping("/admin/subject/update")
    public CommonResult<Object> updateSubject(@RequestBody Subject subject) {
        try {
            subjectService.updateSubject(subject);
            return CommonResult.success("Subject updated successfully!");
        } catch (Exception e) {
            return CommonResult.failed("Failed to update subject: " + e.getMessage());
        }
    }

    // 新增代码：根据 ID 获取 Subject
    @GetMapping("/admin/subject/get/{identifyId}")
    @JsonView(Subject.SubjectDetailView.class)
    public CommonResult<Object> getSubjectById(@PathVariable String identifyId) {
        Subject subject = subjectService.getSubjectByIdentifyId(identifyId);  // 使用 service 层的 getById 方法
        if (subject != null) {
            return CommonResult.success("Subject retrieved successfully!", subject);
        } else {
            return CommonResult.failed("Subject not found.");
        }
    }
}




