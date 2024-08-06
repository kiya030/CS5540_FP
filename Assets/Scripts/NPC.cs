using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueSO dialogueSO; // Reference to the Dialogue ScriptableObject
    private DialogueManager dialogueManager;
    private bool dialogueTriggered = false; // Flag to track if dialogue has been triggered
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player") && !dialogueTriggered)
        {
            dialogueManager.StartDialogue(dialogueSO.dialogues);
            dialogueTriggered = true; 
        }
    }
}
