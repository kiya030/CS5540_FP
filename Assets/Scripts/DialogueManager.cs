using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component
    public Image dialogueBackground;
    private Queue<Dialogue> dialogues; // Queue to hold the dialogue sentences
    public GameObject levelManager;
    public PlayerController playerController;
    private bool isDialogueActive = false; // Flag to track if dialogue is active
    
    void Start()
    {
        LevelManager.isGameOver = false;
        dialogues = new Queue<Dialogue>();
        dialogueBackground.gameObject.SetActive(false); // Hide the background initially
        dialogueText.gameObject.SetActive(false); // Hide the text initially
    }

    void Update()
    {
        // Check for left mouse button click
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue[] dialogueEntries)
    {
        dialogues.Clear();
        dialogueBackground.gameObject.SetActive(true); // Show the background when dialogue starts
        dialogueText.gameObject.SetActive(true); // Show the text when dialogue starts
        playerController.enabled = false;
        isDialogueActive = true;

        foreach (Dialogue dialogue in dialogueEntries)
        {
            dialogues.Enqueue(dialogue);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue dialogue = dialogues.Dequeue();
        string sentence = dialogue.sentences.Length > 0 ? dialogue.sentences[0] : "";
        dialogueText.text = $"{dialogue.name}: {sentence}";
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        dialogueBackground.gameObject.SetActive(false); // Hide the background when dialogue ends
        dialogueText.gameObject.SetActive(false); // Hide the text when dialogue ends
        isDialogueActive = false; // Set dialogue active flag to false
        levelManager.SetActive(true);
    }
}
