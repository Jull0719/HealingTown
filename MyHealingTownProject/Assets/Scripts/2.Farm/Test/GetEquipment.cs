using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GetEquipment : MonoBehaviour
{
    [SerializeField] Hand hand;//需要出现在哪只手拖入
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;
    public Transform attachmentOffset;

    public void GetEquip(GameObject gameObject)
    {
            //后两个参数可以要可不要
            hand.AttachObject(gameObject, GrabTypes.Grip, attachmentFlags, attachmentOffset);
            hand.HideGrabHint();

    }
}
