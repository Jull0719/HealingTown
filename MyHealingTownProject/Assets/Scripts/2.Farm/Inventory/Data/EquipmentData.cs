using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    //工具类型
    Spade,    //铲子
    WateringCan,    //水壶   
    Sickle  //镰刀
}

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
    public ToolType toolType;
}
