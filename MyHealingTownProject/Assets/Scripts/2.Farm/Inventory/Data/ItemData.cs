using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [TextArea(3,10)]
    //描述
    public string description;

    //UI显示图标
    public Sprite thumbnail;

    //道具模型
    public GameObject gameModel;

}
