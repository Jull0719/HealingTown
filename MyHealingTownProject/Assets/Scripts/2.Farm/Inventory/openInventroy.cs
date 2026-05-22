using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class openInventroy : MonoBehaviour
{
    public SteamVR_Action_Boolean openInventory;
    public SteamVR_Input_Sources curindex;

    public GameObject Inventory;
    public GameObject Anchor;
    bool UIActive;

    private void Start()
    {
        Inventory.SetActive(false);
        UIActive = false;
    }

    private void Update()
    {
        //通过X键控制背包开启或是关闭
        if (openInventory.GetStateDown(curindex))
        {
            UIActive = !UIActive;
            Inventory.SetActive(UIActive);
        }
        if (UIActive)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x, Anchor.transform.eulerAngles.y, 0);
        }
    }
}
