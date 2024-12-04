using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPause : MonoBehaviour
{
    [Header("0 means no unpausing will occur")]
    public float pauseDuration = 0;
    public bool loopable;

    bool hasPaused;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && (loopable || !hasPaused))
        {
            Controller.isPaused = true;
            hasPaused = true;

            if (pauseDuration > 0)
                StartCoroutine(UnpausedDelay());
        }
    }

    IEnumerator UnpausedDelay() 
    {
        yield return new WaitForSeconds(pauseDuration);

        Controller.isPaused = false;
    }
}
