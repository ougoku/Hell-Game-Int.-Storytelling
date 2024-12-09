using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    //usually is the player
    public GameObject triggeringObject;
    private AudioSource audioSource; 
	//create array to store sound
	public AudioClip collectSound;
    //item index of the object this script is on
    public int itemIndex;
    public GameObject[] inventorySlots = new GameObject[5];



    private void Start()
    {
        //when game starts, assign audio source of empty game object to priv variable AudioSource
		audioSource = GetComponent<AudioSource>();

        //set the inventory slots
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (GameObject.Find("InventorySlot" + i) != null)
            {
                inventorySlots[i] = GameObject.Find("InventorySlot" + i);
                Debug.Log("adding slots");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if the collider is the same gameobject as the one set in triggeringObject
        if (collider.gameObject == triggeringObject)
        {
            //check for available space
            itemIndex = addItemCheck();
            //if we have enough room then add the item to our list
            if (itemIndex != -1)
            {
                
                this.gameObject.SetActive(false);
                Inventory._items[itemIndex].itemName = this.name;
                //reference the inventory script and set the item collection state as true
                Inventory._items[itemIndex].hasCollected = true;
                Inventory._items[itemIndex].icon = this.GetComponent<SpriteRenderer>().sprite;
                Inventory._items[itemIndex].currentItem = this.gameObject;
                inventorySlots[itemIndex].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                audioSource.PlayOneShot(collectSound, .5f);
                Debug.Log("adding " + this.name + " to the inventory");
            }
            else
            {
                Debug.Log("Inventory Full!");
            }

        }
    }
    //check for inventory space
    private int addItemCheck()
    {
        for (int i = 0; i < Inventory._items.Length; i++)
        {
            //if we find an empty slot, return the index
            if (Inventory._items[i].hasCollected == false)
            {
                return i;
            }
        }
        return -1;
    }
}
