using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventorycs : MonoBehaviour
{
    public GameObject wantedItem;

    //deactivate parent object if player has specified item in their inventory
    public void deactivateBarrier()
    {
        this.gameObject.SetActive(false);
    }
    public void Update()
    {

        for (int i = 0; i < Inventory._items.Length; i++)
        {
            //if we find te item in our inventory, deactivate barrier
            if (Inventory._items[i].icon == wantedItem.GetComponent<SpriteRenderer>().sprite && Inventory._items[i].hasCollected == true)
            {
                deactivateBarrier();
            }
        }
    }
}
