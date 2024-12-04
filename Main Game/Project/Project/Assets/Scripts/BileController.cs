using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BileController : MonoBehaviour
{
    public ParticleSystem bileFlow;
    public GameObject bileBarrier;
    //gets activated when skull makes contact with the controller and basically slaps the same skull sprite over
    //the hole to show that it's been closed up
    public GameObject childSkullPlugHolder;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only stops flow if the correct collider object makes contact
        if (other.name.Contains("skull") || other.name.Contains("Skull"))
        {
            //stops the particle system
            bileFlow.Stop();
            //deactivates the barrier so the player can pass through
            bileBarrier.SetActive(false);
            //also change the sprite so the thing gets plugged up :^)
            childSkullPlugHolder.GetComponent<SpriteRenderer>().sprite = other.GetComponent<SpriteRenderer>().sprite;
            childSkullPlugHolder.SetActive(true);
            //deactivating the draggable skull item so you can't reuse the same skull on different holes
            other.gameObject.SetActive(false);
        }
    }

}
