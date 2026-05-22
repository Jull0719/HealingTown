using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //NPC
    private GameObject NPC;
    //检测是否是第一次见到
    bool Waving = true;
    bool gripTip = true;

    //对话框
    public GameObject dialogueUI;
    public Text NPCWord;    //NPC的话
    public Text PlayerWord; //Player的话

    //NPC动画
    private Animator animator;

    //NPC寻路
    public GameObject destinationPoint;    //目的地1（这里是NPC第一次停下来的位置）
    public GameObject destination;  //最终目的地
    NavMeshAgent theAgent;


    //提示框4
    public GameObject tipFour;
    bool tipFourOpen = true;

    //场景切换提示
    public GameObject change;

    ShowControllers showControllers;

    public void Start()
    {
        //NPC
        NPC = GameObject.Find("NPC");
        animator = NPC.GetComponent<Animator>();
        theAgent = NPC.GetComponent<NavMeshAgent>();    //寻路
        showControllers = GetComponent<ShowControllers>();

    }


    private void OnTriggerEnter(Collider other)
    {
        //遇到NPC时出现对话框
        if (other.name == "NPC" && Waving == true)
        {
            //第一次遇见时,NPC看向玩家，挥手
            other.transform.forward = -transform.forward;
            //挥手
            animator.SetBool("IsWaving", true);
            Waving = false;
            //1秒后出现对话框
            StartCoroutine(OnWaitMethod());
        }

        //检查苹果是否在手里
        if(other.name == "NPC" && tipFourOpen == false)
        {
            LeadingToDes(); //去往最终目的地
            StartCoroutine(Ondes());
        }

        //苹果抓握的教程
        //显示Tip4：抓握
        if (other.name == "GripTip" && tipFourOpen == true)
        {
            tipFour.SetActive(true);
            tipFourOpen = false;    //之后不再出现
        }

        //检查是否到达目的地
        if(other.name == "Destination")
        {
            //切换场景
            StartCoroutine(changeScene());
        }
    }

    IEnumerator OnWaitMethod()
    {
        yield return new WaitForSeconds(2);
        dialogueUI.SetActive(true); //出现对话框
        animator.SetBool("IsWaving", false);  //停止挥手动画
    }

    public void Leading()
    {
        if (gripTip == true)
        {
            //寻路去目的点1
            animator.SetBool("Walking", true);
            theAgent.SetDestination(destinationPoint.transform.position);
            //到达目的地后停止步行
            StartCoroutine(OnWait());
            gripTip = false;
        }
    }

    IEnumerator OnWait()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("Walking", false);
        theAgent.updateRotation = false;

        //对话框出现
        yield return new WaitForSeconds(1);
        //面向玩家
        NPC.transform.forward = -transform.forward;
        NPCWord.text = "我想你肯定饿了吧，这里有一些我刚摘下的苹果，你可以吃掉它，试着捡起它吧。";
        PlayerWord.text = "好的，谢谢您！";
        dialogueUI.SetActive(true);
        //关闭控制器的显示
        showControllers.showControllers = false;
    }


    //去目的地
    public void LeadingToDes()
    {
        NPCWord.text = "好的，现在让我们去小镇吧！";
        PlayerWord.text = "好的~";
        dialogueUI.SetActive(true);
    }

    IEnumerator Ondes()
    {
        yield return new WaitForSeconds(2);
        Destroy(dialogueUI);
        NPC.transform.forward = -NPC.transform.forward;
        //寻路去目的点1
        animator.SetBool("Walking", true);
        theAgent.SetDestination(destination.transform.position);
        yield return new WaitForSeconds(7);
        animator.SetBool("Walking", false);
        theAgent.updateRotation = false;
    }

    //2秒后更换场景 → 到小镇
    IEnumerator changeScene()
    {
        change.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

}
