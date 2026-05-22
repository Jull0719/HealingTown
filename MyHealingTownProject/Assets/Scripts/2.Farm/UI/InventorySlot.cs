using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    ItemData itemToDisplay;

    public Image itemDisplayImage;

    int slotIndex;

    //public enum InventoryType
    //{
    //    Item,Tool
    //}

    ////道具类型
    //public InventoryType inventoryType;


    //public GameObject handPoint;


    public void Display(ItemData itemToDisplay)
    {   //显示道具
        if(itemToDisplay != null)
        {
            //显示图标
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);

            return;
        }

        itemDisplayImage.gameObject.SetActive(false);
  
    }


    //设置背包格子索引
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //从背包移到手中
        InventoryManager.Instance.InventoryToHand(slotIndex);
    }

    //悬浮时展示道具信息
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    //离开时道具信息设为空
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }
}
