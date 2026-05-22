using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //游戏对象对应的道具信息  
    public ItemData item;

    public void Pickup()
    {
        //将角色当前持有道具设为该道具
        InventoryManager.Instance.equippedItem = item;
        //销毁这个实例，以免有多个副本  
        Destroy(gameObject);
    }
}