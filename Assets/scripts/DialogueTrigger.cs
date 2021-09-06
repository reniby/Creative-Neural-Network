using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueController controller;

    public void TriggerDialogue()
    {
        controller.StartDialogue(dialogue);
    }

    
}
