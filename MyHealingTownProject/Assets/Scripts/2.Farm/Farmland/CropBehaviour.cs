using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropBehaviour : MonoBehaviour
{
    //种子数据
    SeedData seedToGrow;

    ////TODO:显示植物信息
    //[SerializeField]
    //private Text plantName;
    //[SerializeField]
    //private Text plantDayToGrow;

    [Header("植物生长阶段")]
    public GameObject seed;     //种子
    private GameObject seedling;     //小苗
    private GameObject bloom;          //开花
    public GameObject harvestable;     //成熟

    //植物生长点
    int growth;
    //在植物成熟前经历多少个生长点
    int maxGrowth;

    public enum CropState
    {
        Seed,   //种子
        Seedling,   //小苗
        Bloom,  //开花
        Harvestable     //成熟
    }
    //当前植物成长阶段
    public CropState cropState;

    //初始化植物
    //玩家种植植物时
    public void Plant(SeedData seedToGrow)
    {
        
        //种子的信息
        this.seedToGrow = seedToGrow;
        //plantName.text = seedToGrow.cropToYield.name;
        //plantDayToGrow.text = seedToGrow.daysToGrow.ToString();

        //植物小苗
        seedling = Instantiate(seedToGrow.seedling, transform);

        //植物开花
        bloom = Instantiate(seedToGrow.bloom, transform);

        //成熟植物
        harvestable = Instantiate(seedToGrow.harvestable, transform);

        //植物数据 收获果实时使用
        ItemData cropToYield = seedToGrow.cropToYield;

        //将天数转换为小时
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        //转换为分钟
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

        //初始状态为种子
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        //生长点++
        growth++;

        //生长点25% → 小苗
        if (growth >= maxGrowth / 4 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        //生长点50% → 开花
        if (growth >= maxGrowth / 2 && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Bloom);
        }

        //生长点加满
        if (growth >= maxGrowth && cropState == CropState.Bloom)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    //状态变化
    private void SwitchState(CropState stateToSwitch)
    {
        //所有物体不显示
        seed.SetActive(false);
        seedling.SetActive(false);
        bloom.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                //种子
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                //小苗
                seedling.SetActive(true);
                break;
            case CropState.Bloom:
                //开花
                bloom.SetActive(true);
                break;
            case CropState.Harvestable:
                //成熟
                harvestable.SetActive(true);
                break;
        }

        //当前植物状态
        cropState = stateToSwitch;
    }
}
