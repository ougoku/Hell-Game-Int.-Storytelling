using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject target;
    public bool autoPlay; // play through dialogue while target is in trigger
    public float autoPlayPauseInterval;
    public bool restartSequence;
    public DialogueMode dialogueMode;
    public GameObject[] dialogue;

    public enum DialogueMode { Random, SequenceOnce, SequenceLoop };

    int index = -1;
    float pauseDuration;
    bool isPlaying;

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
