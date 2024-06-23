using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   public Dialogues dialogue;

    private bool wasActivated=false;

    [SerializeField] private bool hasEndEvent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasEndEvent && wasActivated==false)
        {
            DialogueManager.Instance.hasEndEvent = true;
        }

        if(collision.gameObject.tag == "Player" && wasActivated==false)
        {
            TriggerDialogue();
            wasActivated=true;
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
