using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameTimestamp
{
    public int day;
    public int hour;
    public int minute;

    //构造类
    public GameTimestamp( int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    //从另一个已经存在的实例中创建一个新的GameTimestamp实例  
    public GameTimestamp(GameTimestamp timestamp)
    {
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
    }

    //使得时间增加1分钟
    public void UpdateClock()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }
        if (hour >= 24)
        {
            hour = 0;
            day++;
        }
        
    }

    //小时化为分钟
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }

    //天数化为小时
    public static int DaysToHours(int days)
    {
        return days * 24;
    }

    //计算两个时间戳间时间间隔
    public static int CompareTimestamps(GameTimestamp timestamp1, GameTimestamp timestamp2)
    {
        //差值为小时
        int timestamp1Hours = DaysToHours(timestamp1.day) + timestamp1.hour;
        int timestamp2Hours =  DaysToHours(timestamp2.day) + timestamp2.hour;
        int difference = timestamp2Hours - timestamp1Hours;
        return Mathf.Abs(difference);
    }
}
