using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class IntroHintMain : MonoBehaviour
{
    [SerializeField]
    Hand lefthand;
    [SerializeField]
    Hand righthand;
    [SerializeField]
    private Text tipText;
    public GameObject tipCanvas;

    bool IsOpenedInventory = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Player" && IsOpenedInventory == false)
        {
            ShowInventoryHint();
            StartCoroutine(wait());
        }
    }

    //背包按钮的高亮
    public void ShowInventoryHint()
    {
        tipText.text = "按下左控制器X键打开或关闭背包";
        tipCanvas.SetActive(true);
        lefthand.ShowInventoryHint();
    }
    public void HideInventoryHint()
    {
        tipCanvas.SetActive(false);
        lefthand.HideInventoryHint();
    }

    //交互按钮的高亮
    //public void ShowInteractHint()
    //{
    //    tipText.text = "按下右控制器A键进行交互";
    //    tipCanvas.SetActive(true);
    //    righthand.ShowInteractHint();
    //}
    //public void HideInteractHint()
    //{
    //    tipCanvas.SetActive(false);
    //    righthand.HideInteractHint();
    //}

    IEnumerator wait()
    {
        yield return new WaitForSeconds(4f);
        IsOpenedInventory = true;
        HideInventoryHint();
    }
}
