package org.example.backend.Controller;

import cn.hutool.core.date.DateTime;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.AiFile;
import org.example.backend.Service.IAiFileService;
import org.example.backend.Service.IAssistantService;
import org.example.backend.Service.Imp.AiFileServiceImpl;
import org.example.backend.Service.Imp.AssistantServiceImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.File;
import java.io.IOException;

@RestController
public class AiFileController {

    @Autowired
    IAiFileService aiFileService;
    @Autowired
    IAssistantService assistantService;

    @Value("${assistant.uploadDir}")
    String uploadDir;

    @PostMapping("/admin/uploadFile")
    public CommonResult<Object> uploadFile(@RequestParam("file") MultipartFile file,@RequestParam("assistantId")String id) throws IOException {

        if(file==null||id==null||id.isEmpty())
            return CommonResult.failed("information is incomplete");
        //转储到本地
        File uploadedFile = new File(uploadDir + new DateTime().toString("yyyy_MM_dd_hh_mm_ss__") +file.getOriginalFilename());
        file.transferTo(uploadedFile);
        //上传
        AiFile aiFile = assistantService.uploadFile(uploadedFile,id);

        return CommonResult.success("upload file success!",aiFile);

    }

    @GetMapping("/admin/getAllFiles")
    public CommonResult<Object> getAllFile() throws IOException {

        return CommonResult.success("get file success!",aiFileService.list());

    }

    @GetMapping("/getFileByAssitantId")
    public CommonResult<Object> getFileByAssistantId(@RequestParam("assistantId")String id) throws IOException {

        if(id==null)
            return CommonResult.failed("Assistant Id con not be empty");

        return CommonResult.success("get file success!",aiFileService.getByAssistantId(id));
    }




}
