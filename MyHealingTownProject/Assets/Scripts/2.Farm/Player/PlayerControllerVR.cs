using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerControllerVR : MonoBehaviour
{
    public SteamVR_Action_Boolean interact_Tool;
    public SteamVR_Input_Sources curindex;

    //交互
    PlayerInteraction playerInteraction;

    void Start()
    {
        //交互初始化
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    void Update()
    {
        //交互
        Interact();

        //这里用于Debug，后面要取消
        //左Ctrl键
        if (Input.GetKey(KeyCode.Space)                                                                                                                        )
        {
            TimeManager.Instance.Tick();
        }
    }

    //工具交互
    public void Interact()
    {
        //工具交互，A键进行交互
        if(interact_Tool.GetStateDown(curindex))
        {
            playerInteraction.Interact();
        }
    }
}
