using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    bool IsDialogue1 = true;
    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    private Dialogue dialogue2;
    public int QuestNum = 0;


    private void OnTriggerEnter(Collider other)
    {
        //第一次跟NPC说话 -> 第一段对话
        if(other.name == "Player" && IsDialogue1 == true)
        {
            TriggerDialogue(dialogue);
            IsDialogue1 = false;
        }
        //第一次跟NPC说话 -> 第一段对话
        if (other.name == "Player" && IsDialogue1 == false && QuestNum == 5)
        {
            TriggerDialogue(dialogue2);
        }
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        //第一段对话
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
