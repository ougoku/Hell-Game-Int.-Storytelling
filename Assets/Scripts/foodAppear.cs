using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodAppear : MonoBehaviour
{
    public Animator foodItemAnimator;
    public Animator gluttonyChestAnimator;
    private bool hasChestBeenOpened;
    public GameObject chestObject;
    //reference to the gluttonychest script
    private GluttonyChestOpen gluttonyChestScript;
    public bool hasBeenPickedUpBefore;
    // Start is called before the first frame update
    void Start()
    {
        foodItemAnimator = GetComponent<Animator>();
        if(chestObject != null)
        {
            gluttonyChestAnimator = GetComponent<Animator>();
            gluttonyChestScript = chestObject.GetComponent<GluttonyChestOpen>();
        }

    }
    void Update()
    {
        if(chestObject != null)
        {
            //this is to make sure that if the chest has been opened for the first time already,
            //the food item doesn't do that drop animation if you bring it into the scene from your inventory
            hasBeenPickedUpBefore = gluttonyChestScript.getOpenChestStatus();
            foodItemAnimator.SetBool("beenOpened", hasBeenPickedUpBefore);
        }
        else
        {
            foodItemAnimator.SetBool("beenOpened", true);
        }
        

    }
    //humble little function that makes sure that if you pick up the item again after clicking and dragging
    //it somewhere else from your inventory, the parent chest won't open up again
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player" && chestObject != null)
        {
            //just make sure that chest stays closed when you grab the object lmao
            gluttonyChestAnimator.SetBool("PlayerEnter", false);
        }
    }
}
