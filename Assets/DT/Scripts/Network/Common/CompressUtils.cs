using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
//using System.IO.Compression;
using Unity.IO.Compression;
using System;
using System.Security.Cryptography;

public static class CompressUtils
{
    /// <summary>
    /// 压缩
    /// </summary>
    /// <param name="str">需压缩的字符串</param>
    /// <returns>压缩后的字符串</returns>
    public static string Compress(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length == 0)
        {
            return "";
        }
        else
        {
            byte[] strByte = Encoding.UTF8.GetBytes(str);
            byte[] bytes = Compress(strByte);

            return Convert.ToBase64String(bytes);
        }
    }

    static byte[] Compress(byte[] data)
    {
        try
        {
            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(data, 0, data.Length);
            zip.Close();
            byte[] buffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, buffer.Length);
            ms.Close();
            return buffer;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// 解压缩
    /// </summary>
    /// <param name="str">需要解压缩的字符串</param>
    /// <returns>解压缩之后的字符串</returns>
    public static string Uncompress(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length == 0)
        {
            return "";
        }
        else
        {
            byte[] strByte = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(Uncompress(strByte));
            //return Encoding.GetEncoding("GB2312").GetString();
        }
    }

    static byte[] Uncompress(byte[] data)
    {
        try
        {
            MemoryStream ms = new MemoryStream(data);
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
            MemoryStream msreader = new MemoryStream();
            byte[] buffer = new byte[0x1000];
            while (true)
            {
                int reader = zip.Read(buffer, 0, buffer.Length);
                if (reader <= 0)
                {
                    break;
                }
                msreader.Write(buffer, 0, reader);
            }
            zip.Close();
            ms.Close();
            msreader.Position = 0;
            buffer = msreader.ToArray();
            msreader.Close();
            return buffer;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
     
    /// <summary>
    /// 加密  返回加密后的结果
    /// </summary>
    /// <param name="toE">需要加密的数据内容</param>
    /// <returns></returns>
    public  static string Encrypt(string toE)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
 
    /// <summary>
    /// 解密  返回解密后的结果
    /// </summary>
    /// <param name="toD">加密的数据内容</param>
    /// <returns></returns>
    public static string Decrypt(string toD)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");

        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        byte[] toEncryptArray = Convert.FromBase64String(toD);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return UTF8Encoding.UTF8.GetString(resultArray);
    } 
}
