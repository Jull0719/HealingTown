using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent Agent; //AI
    [SerializeField]
    private Animator Animator;  //动画

    [SerializeField]
    private GameObject npcPanel;    //NPC对话面板

    [SerializeField]
    private Transform homePoint;    //玩家住处
    [SerializeField]
    private Transform IdlePoint;    //NPC常驻点

    private const string IsWalking = "Walking"; //行走参数
    private const string ISWaving = "Noding";

    bool IsFoundingHome = false;    //是否到达住处
    bool IsChating = false; //是否与玩家对话

    void Update()
    {
        //在目的地时，停止行走
        if (Agent.remainingDistance < 0.1f)
        {
            Animator.SetBool(IsWalking, false);
            Agent.transform.localEulerAngles = new Vector3(0, 180f, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && IsChating == false && IsFoundingHome == false)
        {
            //显示对话框
            npcPanel.SetActive(true);

        }
        //if()
        //{
        //    NPC_ToHome();
        //}
    }

    public void NPC_ToHome()
    {
        if (IsFoundingHome == false)
        {
            //找到住处
            //NPC行走
            NPC_Walking(homePoint);
            IsFoundingHome = true;
        }
    }

    /// <summary>
    /// NPC寻路到某地
    /// </summary>
    /// <param name="des">要到达的地点</param>
    public void NPC_Walking(Transform des)
    {
        Animator.SetBool(IsWalking, true);
        Agent.SetDestination(des.position);
    }

    IEnumerator waitForStop()
    {
        yield return new WaitForSeconds(3);
    }
}
