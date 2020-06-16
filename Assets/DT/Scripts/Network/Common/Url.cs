using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Url
{
    private static string ip = "http://39.106.226.52:8080/";
   

    public static string loginUrl = ip + "login";
    public static string registerUrl = ip + "register";
    public static string creatKeyUrl = ip + "createKeyCodes";
    public static string getKeyUrl = ip + "getKeyCodes";


}
