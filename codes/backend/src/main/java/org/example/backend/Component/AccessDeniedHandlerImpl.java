package org.example.backend.Component;

import com.alibaba.fastjson.JSON;
import com.fasterxml.jackson.annotation.JsonView;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.security.web.access.AccessDeniedHandler;
import org.example.backend.Common.api.CommonResult;
import org.example.backend.Common.util.WebUtils;
import org.springframework.stereotype.Component;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

//权限不足处理器
@Component
public class AccessDeniedHandlerImpl implements AccessDeniedHandler {

    @Override
    @JsonView(CommonResult.CommonResultView.class)
    public void handle(HttpServletRequest request, HttpServletResponse response, AccessDeniedException accessDeniedException) throws IOException, ServletException {
        //待写入响应
        CommonResult<Object>commonResult= CommonResult.forbidden(null);
        //转为字符串（json）类
        String json = JSON.toJSONString(commonResult);
        //写入HTTP流
        WebUtils.renderString(response,json);

    }
}