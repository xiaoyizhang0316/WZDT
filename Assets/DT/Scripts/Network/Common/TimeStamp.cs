using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeStamp
{
    private static DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 日期转时间戳
    /// </summary>
    /// <param name="dateTime">日期</param>
    /// <returns></returns>
    public static int GetTimeStamp(DateTime dateTime)
    {
        return (int)(dateTime.ToUniversalTime() - startTime).TotalSeconds;
    }

    /// <summary>
    /// 获取当前时间的时间戳
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentTimeStamp()
    {
        return (int)(DateTime.Now.ToUniversalTime() - startTime).TotalSeconds;
    }

    /// <summary>
    /// 时间戳转时间
    /// </summary>
    /// <param name="timeStamp">时间戳</param>
    /// <returns></returns>
    public static string TimeStampToString(int timeStamp)
    {
        return startTime.AddSeconds(timeStamp).ToLocalTime().ToString();
    }
}
