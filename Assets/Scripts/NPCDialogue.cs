using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public AudioClip dialogueClip; // The audio clip to play
    private AudioSource audioSource;
    private bool hasPlayed = false; // Ensure the dialogue only plays once per approach

    void Start()
    {
        // Get the AudioSource component attached to the NPC
        audioSource = GetComponent<AudioSource>();
        
        // Assign the audio clip if it's not already assigned in the AudioSource
        if (audioSource.clip == null && dialogueClip != null)
        {
            audioSource.clip = dialogueClip;
        }
    }

    // Detect when the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is tagged as "Player" and the audio hasn't played yet
        if (other.CompareTag("Player") && !hasPlayed)
        {
            PlayDialogue();
        }
    }

    // Play the dialogue audio clip
    private void PlayDialogue()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            hasPlayed = true; // Ensure the audio only plays once
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing on NPC.");
        }
    }

    // Optionally, reset the dialogue after the player leaves the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayed = false; // Reset so the player can hear the dialogue again on re-entry
        }
    }
}
