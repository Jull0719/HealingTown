using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandInteractVR : MonoBehaviour
{
    Land land;
    public GameObject select;

    private void Start()
    {
        land = GetComponent<Land>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(select.activeSelf)
        {
            if(other.tag == "Spade")
            {
                land.SwitchLandStatus(Land.LandStatus.Farmland);
            }
            if(other.tag == "WateringCan")
            {
                land.SwitchLandStatus(Land.LandStatus.Watered);
            }
            if(other.tag == "Seed")
            {

            }
        }
    }
}
