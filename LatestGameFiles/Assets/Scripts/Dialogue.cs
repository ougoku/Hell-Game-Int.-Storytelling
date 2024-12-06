using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject target;
    public bool autoPlay; // play through dialogue while target is in trigger
    public float autoPlayPauseInterval;
    public bool restartSequence;
    public DialogueMode dialogueMode;
    public GameObject[] dialogue;

    // Reference to the Canvas attached to the Camera
    public Canvas dialogueCanvas;

    public enum DialogueMode { Random, SequenceOnce, SequenceLoop };

    int index = -1;
    float pauseDuration;
    bool isPlaying;

    void Start()
    {
        // Ensure the Canvas Scaler is set to scale with screen size
        if (dialogueCanvas != null)
        {
            CanvasScaler canvasScaler = dialogueCanvas.GetComponent<CanvasScaler>();
            if (canvasScaler != null)
            {
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(1920, 1080); // Adjust to your target resolution
                canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                canvasScaler.matchWidthOrHeight = 0.5f; // Adjust to preference (0 is width, 1 is height)
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == target)
        {
            isPlaying = true;

            if (restartSequence)
                index = -1;

            if (autoPlay)
                StartCoroutine(AutoPlay());
            else
                NextDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        isPlaying = false;
        CloseDialogue();
    }

    void NextDialogue()
    {
        if (dialogueMode == DialogueMode.Random)
            index = Random.Range(0, dialogue.Length);
        else
            index++;

        index = dialogueMode == DialogueMode.SequenceLoop && index >= dialogue.Length ? 0 : Mathf.Clamp(index, 0, dialogue.Length - 1);

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogue[i].SetActive(i == index);
        }
    }

    void CloseDialogue()
    {
        foreach (GameObject go in dialogue)
        {
            go.SetActive(false);
        }
    }

    IEnumerator AutoPlay()
    {
        while (isPlaying)
        {
            NextDialogue();
            yield return new WaitForSeconds(autoPlayPauseInterval);
        }
    }
}
