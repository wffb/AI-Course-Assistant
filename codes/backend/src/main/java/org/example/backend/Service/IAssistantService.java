package org.example.backend.Service;

import org.example.backend.Common.api.CommonResult;
import org.example.backend.Model.AiAgent;
import org.example.backend.Model.AiFile;
import org.example.backend.Model.Param.AssistantRegisterParam;
import org.example.backend.Model.Param.AssistantUpdateParam;

import java.io.File;
import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.util.List;

public interface IAssistantService {

    public AiAgent createAssistant(AssistantRegisterParam param);

    public List<AiAgent> getAssistant(String subjectIdentityId);

    public List<AiAgent> getAssistant();

    public CommonResult<Object> updateAssistant(AssistantUpdateParam param);

    public AiFile uploadFile(File file,String id) throws MalformedURLException, UnsupportedEncodingException;
}

