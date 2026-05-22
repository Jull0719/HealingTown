using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowControllers : MonoBehaviour
{
    //是否显示控制器
    public bool showControllers = false;

    void Update()
    {
        foreach(var hand in Player.instance.hands)
        {
            if(showControllers)
            {
                hand.ShowController();  //显示控制器
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);  //手的骨骼动画适配控制器
            }
            else
            {
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);   
            }
        }
    }

}
