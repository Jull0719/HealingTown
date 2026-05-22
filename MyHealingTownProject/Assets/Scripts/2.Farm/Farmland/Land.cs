using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    //土地的三种状态：普通土壤，可种植土壤，湿润土壤
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;

    public Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    //当角色选中土地时，显示选中组件
    public GameObject select;

    //浇水的时间
    GameTimestamp timeWatered;

    [Header("植物")]
    //实例化植物预制件
    public GameObject cropPrefab;

    //当前种植在土地上的植物状态
    CropBehaviour cropPlanted = null;

    void Start()
    {
        //获取渲染组件
        renderer = GetComponent<Renderer>();

        //默认设置土地的初始状态为普通土壤
        SwitchLandStatus(LandStatus.Soil);

        //默认取消对土地的选择
        Select(false);

        //加入观察者列表
        TimeManager.Instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        //设置相应的土地状态
        landStatus = statusToSwitch;

        //土地材质的选择
        Material materialToSwicth = soilMat;
        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                //普通土壤
                materialToSwicth = soilMat;
                break;
            case LandStatus.Farmland:
                //可种植土壤
                materialToSwicth = farmlandMat;
                break;
            case LandStatus.Watered:
                //湿润土壤（已浇水土壤）
                materialToSwicth = wateredMat;

                //记录浇水的时间
                timeWatered = TimeManager.Instance.GetGameTimestamp();
                break;
        }

        //使材质发生变化
        renderer.material = materialToSwicth;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    //当角色在选中的土地上按下交互键时进行交互
    public void Interact()
    {
        //将手持道具存为道具数据
        ItemData toolSlot = InventoryManager.Instance.equippedItem;

        //如果什么都没有装备
        if (toolSlot == null)
        {
            return;
        }

        //将手持道具数据转换为工具数据
        EquipmentData equipmentTool = toolSlot as EquipmentData;

        //检查手持道具是否属于工具
        if (equipmentTool != null)
        {
            //获取工具类型
            ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case ToolType.Spade:    //锄头
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case ToolType.WateringCan:    //水壶
                    SwitchLandStatus(LandStatus.Watered);
                    break;
            }

            //如果已经所持道具是工具，就不需要检查种子了  
            return;
        }

        //手持道具转换为种子数据
        SeedData seedTool = toolSlot as SeedData;


        ///角色能够种植一棵种子的条件
        ///1:持有种子（SeedData类型）
        ///2:土地状态必为可种植土壤或湿润土壤
        ///3:这块地没有已种植的植物
        if (seedTool != null && landStatus != LandStatus.Soil && cropPlanted == null)
        {
            //生成植物并以土地为父级
            GameObject cropObject = Instantiate(cropPrefab, transform);
            //将植物移到土地上方
            cropObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            //获取种植的植物的生长状态
            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            //种下植物
            cropPlanted.Plant(seedTool);
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        //检查距离上次浇水是否已过了24小时
        if (landStatus == LandStatus.Watered)
        {
            //已浇水时间
            int hoursElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);

            //植物生长
            if (cropPlanted != null)
            {
                cropPlanted.Grow();
            }

            if (hoursElapsed > 24)
            {
                //变干 (变回普通土壤)
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }
}
