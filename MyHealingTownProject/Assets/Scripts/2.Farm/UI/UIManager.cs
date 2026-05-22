using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header("状态栏")]
    ////状态栏显示的手持工具

    //时间UI
    public Text timeText;
    public Text dateText;
    public Image weatherIcon;
    public Sprite Sun;
    public Sprite Moon;

    public Material dayMat;
    public Material nightMat;

    [Header("背包系统")]

    //背包面板
    public GameObject inventoryPanel;

    //背包面板中当前装备的道具
    public HandInventorySlot itemHandSlot;

    //道具背包格子
    public InventorySlot[] itemSlots;

    //道具信息
    public Text itemNameText;
    public Text itemDescriptionText;

    private void Awake()
    {

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

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();

        //观察者模式：将UIManager添加到对象列表中，当时间更新时，TimeManager会通知  
        TimeManager.Instance.RegisterTracker(this);
    }

    //遍历UI中道具格子，并赋予索引值 
    public void AssignSlotIndexes()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].AssignIndex(i);
        }
    }

    //背包信息展示到UI
    public void RenderInventory()
    {
        //从背包管理器中获取道具信息
        ItemData[] inventoryItemSlot = InventoryManager.Instance.items;

        //道具选区
        RenderInventoryPanel(inventoryItemSlot, itemSlots);

        //当前手持道具的显示
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);

    }

    //遍历格子并呈现于UI
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            //显示相应的格子
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //如果面板是隐藏的，显示出来，反之亦然  
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    //将道具信息展示到背包面板上
    public void DisplayItemInfo(ItemData data)
    {
        //数据为空便设置为空
        if (data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";

            return;
        }
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }

    //时间UI
    public void ClockUpdate(GameTimestamp timestamp)
    {
        //时间的处理
        //获取小时、分钟
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        //AM
        string prefix = "AM ";
        if (hours > 6)
        {
            ChangeSkybox(dayMat);
        }

        //12小时制
        if (hours > 12)
        {
            //PM
            prefix = "PM ";
            hours = hours - 12;
            //Debug.Log(hours);
            if (hours > 6)
            {
                ChangeSkybox(nightMat);
            }
        }

        //格式：AM 6:00
        timeText.text = prefix + hours + ":" + minutes.ToString("00");

        int day = timestamp.day;

        //格式：第X天
        dateText.text = "第 " + day.ToString() + " 天";
    }

    //更换天空盒子
    public void ChangeSkybox(Material Skybox)
    {
        RenderSettings.skybox = Skybox;
    }
}
