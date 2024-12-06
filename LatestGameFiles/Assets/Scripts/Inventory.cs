using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventorySlots = new GameObject[5];
    public Item[] items;
    public static Item[] _items;

    //oh this kinda looks like a struct
    [System.Serializable]
    public class Item
    {
        public string itemName = "itemName - index #";
        public Sprite icon;
        public bool hasCollected;
        public GameObject currentItem;
    }

    private void Start()
    {
        _items = items;

        //set the inventory slots
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = GameObject.Find("InventorySlot" + i);
        }
    }
    private void Update()
    {
        //iterate through inventory  
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].hasCollected == true)
            {
                inventorySlots[i].GetComponent<SpriteRenderer>().sprite = _items[i].icon;
            }
        }
    }
}
