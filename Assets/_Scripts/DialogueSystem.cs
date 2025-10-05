using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    private Coroutine dialogueCoroutine;
    private bool isRunning;
    private string currentDialogue;

    float secondsPerCharacter = 0.3f; // speed of reading
    float baseDuration = 1f; // minimum time on screen

    public TextMeshProUGUI textMesh;

    public AudioClip[] speeches;
    AudioSource audioSource;
    // Call this from NPCController

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartDialogue(string dialogue,int voiceIndex = 0)
    {
        StopDialogue();
        currentDialogue = dialogue;

        //if (isRunning)
        //{
        //    Debug.Log("Updating dialogue: " + dialogue);
        //    // Dialogue already running, we just update the text
        //    return;
        //}

        Debug.Log("Starting dialogue: " + dialogue);
        dialogueCoroutine = StartCoroutine(DialogueLoop(voiceIndex));
    }

    public void StopDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        isRunning = false;
        Debug.Log("Dialogue stopped.");
    }

    private IEnumerator DialogueLoop(int voiceIndex = 0)
    {
        isRunning = true;
        Debug.Log("Dialogue loop started.");

        while (isRunning)
        {
            // Show dialogue text in UI here
            Debug.Log("Current dialogue: " + currentDialogue);
            textMesh.text = currentDialogue;

            audioSource.clip = speeches[voiceIndex];
            audioSource.Play();
        
            float duration = baseDuration + (currentDialogue.Length * secondsPerCharacter);
            Debug.Log($"Text will stay for {duration:F2} seconds.");

            yield return new WaitForSeconds(duration); // delay between lines or updates
        }

        Debug.Log("Dialogue loop ended.");
    }
}
