using System;
using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class IntroHint : MonoBehaviour
{
    [SerializeField]
    private GameObject tipOne;
    [SerializeField]
    Hand lefthand;
    [SerializeField]
    Hand righthand;
    public void Update()
    {
        if (tipOne.activeSelf == true)
        {
            ShowConfirmHint();
        }
        else
        {
            HideConfirmHint();
        }
    }

    //确定按键的高亮
    public void ShowConfirmHint()
    {
        righthand.ShowTriggerHint();
    }
    public void HideConfirmHint()
    {
        righthand.HideTriggerHint();
    }

    //移动按钮的高亮
    public void ShowMoveHint()
    {
        lefthand.ShowMoveHint();
    }
    public void HideMoveHint()
    {
        lefthand.HideMoveHint();
    }

    //旋转按钮的高亮
    public void ShowRotationHint()
    {
        righthand.ShowRotationHint();
    }
    public void HideRotationHint()
    {
        righthand.HideRotationHint();
    }

    //传送按钮的高亮
    public void ShowTeleportHint()
    {
        lefthand.ShowTriggerHint();
    }
    public void HideTeleportHint()
    {
        lefthand.HideTriggerHint();
    }

    //背包按钮的高亮
    public void ShowInventoryHint()
    {
        lefthand.ShowInventoryHint();
    }
    public void HideInventoryHint()
    {
        lefthand.HideInventoryHint();
    }

    ////交互按钮的高亮
    //public void ShowInteractHint()
    //{
    //    righthand.ShowInteractHint();
    //}
    //public void HideInteractHint()
    //{
    //    righthand.HideInteractHint();
    //}

}
