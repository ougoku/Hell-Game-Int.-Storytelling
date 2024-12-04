using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyChestOpen : MonoBehaviour
{
    public Animator gluttonyChestAnimator;
	private AudioSource audioSource; 
	//create array to store sound
	public AudioClip[] chestAudioClips; 

    //holds whatever food item is supposed to come from this chest
    //also for best results, make the food item starting point where you want it to land so that when the animation stops,
    //it will stay in the landing spot instead of launching back into the chest
    public GameObject foodItem;

    // Start is called before the first frame update
    void Start()
    {
        //grab the animation controller component 
        gluttonyChestAnimator = GetComponent<Animator>();
        //when game starts, assign audio source of empty game object to priv variable AudioSource
		audioSource = GetComponent<AudioSource>();
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        //can be changed later, just make sure the player has the same tag
        //when the player gets close to the chest
        if (player.tag == "Player")
        {
            gluttonyChestAnimator.SetBool("PlayerEnter", true);
            audioSource.PlayOneShot(chestAudioClips[0], .5f);
            foodItem.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D player)
    {
        //can be changed later, just make sure the player has the same tag
        //when the player gets close to the chest
        if (player.tag == "Player")
        {
            gluttonyChestAnimator.SetBool("PlayerEnter", false);
            audioSource.PlayOneShot(chestAudioClips[1], .5f);
        }
    }
}
