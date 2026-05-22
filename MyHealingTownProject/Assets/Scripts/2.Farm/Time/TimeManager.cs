using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("昼夜节律")]
    [SerializeField]
    GameTimestamp timestamp;
    public float timeScale = 1.0f;

    [Header("日夜循环系统")]
    //directional light 太阳光位置
    public Transform sunTransform;

    //涉及时间变换（观察者列表）
    List<ITimeTracker> listeners = new List<ITimeTracker>();

    private void Awake()
    {

        //单例
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //初始化时间戳 第1天，6：00
        timestamp = new GameTimestamp(1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1 / timeScale);
        }
    }

    //游戏里时间 tick
    public void Tick()
    {
        timestamp.UpdateClock();

        //新的时间
        foreach (ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }

        UpdateSunMovement();
    }

    //日夜循环
    void UpdateSunMovement()
    {
        //当前时间转化为分
        int timeInMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;

        //太阳每小时旋转15度
        //每分钟0.25度
        //午夜(0:00), 太阳角度应为-90度
        float sunAngle = 0.25f * timeInMinutes - 90;

        //direction light相应角度变化
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    //时间戳
    public GameTimestamp GetGameTimestamp()
    {
        //副本
        return new GameTimestamp(timestamp);
    }

    //处理观察者

    //将物体注册到观察者列表
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    //移除
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
