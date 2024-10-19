package org.example.backend.Component;


import org.example.backend.Common.exception.JwtException;
import org.example.backend.Common.util.JwtUtil;
import org.example.backend.Mapper.UserMapper;
import org.example.backend.Model.Detail.LoginUser;
import org.example.backend.Service.UserService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Component;
import org.springframework.util.StringUtils;
import org.springframework.web.filter.OncePerRequestFilter;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Date;


@Component
public class JwtAuthenticationTokenFilter extends OncePerRequestFilter {

    private static final Logger LOGGER = LoggerFactory.getLogger(JwtAuthenticationTokenFilter.class);

    @Autowired
    private JwtUtil jwtUtil;
    @Autowired
    UserMapper userMapper;

    @Value("${jwt.tokenHeader}")
    private String tokenHeader;


    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain) throws ServletException, IOException{

        //获取token
        String token = request.getHeader(this.tokenHeader);
        if (!StringUtils.hasText(token)) {
            //放行
            filterChain.doFilter(request, response); //无token则转为账号密码认证
            return;
        }

        //解析token
        String username;
        String role;
        Date iat;
        boolean isTeacher;

        try {
            username = jwtUtil.getUsernameFromToken(token);
            isTeacher= jwtUtil.getIsTeacherFromToken(token);
            iat=jwtUtil.getIatFromToken(token);

        } catch (Exception e) {

            LOGGER.info("token: "+token+" token非法");

            throw new RuntimeException("token非法");
        }


//        LOGGER.info("checking "+role+" username:{}", username);
//        //从redis中获取用户信息
//        String redisKey = "login:" + username;
//        LoginUser loginUser = (LoginUser) redisUtil.get(redisKey);
//
//        //检验token  数据是否一致
//        if(Objects.isNull(loginUser)){
//
//            LOGGER.info("authenticated "+role+":{} failed", username);
//
//            throw new RuntimeException("用户未登录");
//        }


        //检查token是否过期，是否是本次登录签发的
        //转换日期


        if(isTeacher)
            role ="Teacher";
        else
            role="Student";


        if(jwtUtil.isTokenExpired(token)){

            LOGGER.info("authenticated "+role+":{} failed", username);

            throw new JwtException(404L,"用户已过期，请重新登录");
        }
        LOGGER.info("authenticated "+role+":{}", username);

        LoginUser loginUser = new LoginUser(
           userMapper.getByUsernameUser(username)
        );


        //存入SecurityContextHolder
        //获取权限信息封装到Authentication中
        UsernamePasswordAuthenticationToken authenticationToken =
                new UsernamePasswordAuthenticationToken(loginUser,null,loginUser.getAuthorities());
        //交给安全上下文
        SecurityContextHolder.getContext().setAuthentication(authenticationToken);
        //放行
        filterChain.doFilter(request, response);
    }
}