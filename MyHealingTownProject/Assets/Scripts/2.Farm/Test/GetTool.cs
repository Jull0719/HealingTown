using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class GetTool : MonoBehaviour
{
    [SerializeField] Hand hand;//需要出现在哪只手拖入
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;
    public Transform attachmentOffset;

    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(nameof(GetGUn));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            hand.DetachObject(gameObject, false);
        }
    }
    IEnumerator GetGUn()
    {
        //因为是手的姿势需要时间加载
        yield return new WaitForSeconds(1f);
        //后两个参数可以要可不要
        hand.AttachObject(this.gameObject, GrabTypes.None, attachmentFlags, attachmentOffset);
        hand.HideGrabHint();
    }

}
