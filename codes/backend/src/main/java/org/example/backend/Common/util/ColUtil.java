package org.example.backend.Common.util;

import java.util.Arrays;
import java.util.List;

public class ColUtil {

    public static List<String> getClip(String s){

        String[] ss = s.split("/");

        return Arrays.asList(ss);
    }
}
