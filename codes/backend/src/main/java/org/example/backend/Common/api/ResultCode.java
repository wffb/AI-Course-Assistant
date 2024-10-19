package org.example.backend.Common.api;

import lombok.Getter;

@Getter
public enum ResultCode {
    /** 固定枚举必须写在头部
     */
    SUCCESS(200, "Operation successful"),
    FAILED(500, "Operation failed"),
    VALIDATE_FAILED(404, "Parameter verification failed"),
    UNAUTHORIZED(401, "You have not logged in or your token has expired"),
    FORBIDDEN(403, "There are no relevant permissions");

    private Long code;
    private String message;

    /**
     * 枚举的构建函数应当私有来确保不出现额外的类型
     */
    private ResultCode(long code, String message) {
        this.code = code;
        this.message = message;
    }

//    public long getCode() {
//        return code;
//    }
//
//    public String getMessage() {
//        return message;
//    }
}
