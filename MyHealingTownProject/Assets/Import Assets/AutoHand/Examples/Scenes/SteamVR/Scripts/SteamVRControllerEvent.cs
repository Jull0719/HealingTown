using UnityEngine;
using UnityEngine.Events;
using Valve.VR;


public class SteamVRControllerEvent : MonoBehaviour{
    public SteamVR_Input_Sources controller;
    public SteamVR_Action_Boolean button;
    public UnityEvent Pressed;
    public UnityEvent Released;
    bool pressed;


    void Update()
    {
        if (!pressed && button.GetState(controller))
        {
            pressed = true;
            Pressed?.Invoke();
        }
        if (pressed && !button.GetState(controller))
        {
            pressed = false;
            Released?.Invoke();
        }
    }
}