

using System;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CommonFunc 
{
    /*public static List<T> Clone<T>(object List)
    {
        using (Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, List);
            objectStream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as List<T>;
        }
    }*/
    
    /*public static T Clone<T>(T RealObject) 
    {  
        using(Stream stream=new MemoryStream())
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, RealObject);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(stream);
        }
    }*/
    
    /*public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable   
    {   
        return listToClone.Select(item => (T)item.Clone()).ToList();   
    } */

    /// <summary>
    /// 获取括号里的内容（包含括号）
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    public static string GetContentInBrackets(string origin)
    {
        string result = "";
        Match match = CommonParams.BracketsRegex.Match(origin);
        if (match.Groups.Count > 0)
        {
            result = match.Groups[0].Value;
            //return result.Substring(1, result.Length - 2);
        }

        return result;
    }
    
    /// <summary>
    /// 获取括号里的内容（不包含括号）
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    public static string GetContentInBracketsWithoutBrackets(string origin)
    {
        string result = "";
        Match match = CommonParams.BracketsRegex.Match(origin);
        if (match.Groups.Count > 0)
        {
            result = match.Groups[0].Value;
            return result.Substring(1, result.Length - 2);
        }

        return result;
    }

    public static void AddComponent(GameObject go, string componentName)
    {
        try
        {
            var script = Type.GetType(componentName);
            go.AddComponent(script);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}
