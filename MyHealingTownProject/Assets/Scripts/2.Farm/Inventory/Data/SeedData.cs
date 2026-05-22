using UnityEngine;
[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    public int daysToGrow;	//成熟需要的时间
    public GameObject seedling;	//植物对应小苗
    public GameObject bloom;  //植物对应的花
    public GameObject harvestable;	//植物成熟
    public ItemData cropToYield;    //收获的果实
}
