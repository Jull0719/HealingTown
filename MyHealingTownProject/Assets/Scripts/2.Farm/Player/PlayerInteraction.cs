using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    //当前选中的土地
    Land selectedLand = null;

    //当前角色选中的交互物体
    InteractableObject selectedInteractable = null;


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    //交互
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        //是否与土地进行交互
        if (other.tag == "Land")
        {
            //获取土地组件
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            return;
        }

        //是否与物品进行交互
        if (other.tag == "Item")
        {
            selectedInteractable = other.GetComponent<InteractableObject>();
            return;
        }
        //取消选择
        if (selectedInteractable != null)
        {
            selectedInteractable = null;
        }

        //未站在土地上时取消选择
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
    }

    //处理土地选择过程
    void SelectLand(Land land)
    {
        //取消先前选定的土地
        if (selectedLand != null) 
        {
            selectedLand.Select(false);
        }

        //当前选定的土地
        selectedLand = land;
        land.Select(true);
    }

    //使用工具时触发
    public void Interact()
    {
        ////当玩家手中持有道具时不能再使用工具
        //if (InventoryManager.Instance.equippedItem != null)
        //{
        //    return;
        //}

        //检查是否选定了土地
        if (selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }
        Debug.Log("没有站在土地上");
    }

    //使用物品时触发
    public void ItemInteract()
    {
        //如果角色持有某物，将物体装入包中
        //if(InventoryManager.Instance.equippedItem != null)
        //{
        //    InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);
        //    return;
        //}

        //如果玩家空手，拾取道具
        //检查是否有可交互选项
        if(selectedInteractable != null)
        {
            //Pick it up
            selectedInteractable.Pickup();
        }

    }

}
