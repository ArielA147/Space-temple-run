using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*the story element*/
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue(){
        // singltone
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
