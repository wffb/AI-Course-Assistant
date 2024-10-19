package org.example.backend.Mapper;

import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.example.backend.Model.User;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author werb
 * @since 2024-09-22
 */

@Mapper
public interface UserMapper extends BaseMapper<User> {

    public User getByUsernameUser(@Param("username") String username);

    public String getIdByUsername(@Param("username") String username);
}
