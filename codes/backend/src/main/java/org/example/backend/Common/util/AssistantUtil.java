package org.example.backend.Common.util;

import org.springframework.beans.factory.annotation.Value;

public class AssistantUtil {

    @Value("${assistant.api}")
    public  static String apiKey;

}
