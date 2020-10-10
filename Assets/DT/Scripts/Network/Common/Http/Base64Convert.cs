using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base64Convert : MonoBehaviour
{
    public static byte[] StringToByteArray(string source)
    {
        return Convert.FromBase64String(source);
    }

    public static string ByteArrayToString(byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }
}
