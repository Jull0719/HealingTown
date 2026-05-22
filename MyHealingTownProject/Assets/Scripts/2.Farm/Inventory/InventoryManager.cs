using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Hand hand;//手
    public Transform attachmentOffset;//手持
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;
    private GameObject grabModel;

    public static InventoryManager Instance {get;private set;}



    private void Awake() {
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


    [Header("道具")]
    //道具格子
    public ItemData[] items = new ItemData[8];
    //手持道具
    public ItemData equippedItem = null;

    //道具从背包到手上
    public void InventoryToHand(int slotIndex)
    {
        //暂存道具信息
        ItemData itemToEquip = items[slotIndex];

        //将背包格子更换为手持道具
        items[slotIndex] = equippedItem;

        //手持道具变为背包相应格子
        equippedItem = itemToEquip;

        grabModel = Instantiate(itemToEquip.gameModel);
        hand.AttachObject(grabModel, GrabTypes.None, attachmentFlags, attachmentOffset);

        //更新UI
        UIManager.Instance.RenderInventory();

    }


    //道具从手上到背包中
    public void HandToInventory()
    {
        //遍历背包格子寻找空位
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {   //手持道具放入空格子
                items[i] = equippedItem;
                //移除手持道具格子
                equippedItem = null;
                Destroy(grabModel);
                break;
            }
        }

        //更新UI
        UIManager.Instance.RenderInventory();
    }
}
