using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] items;

    public static Item[] _items;

    [System.Serializable]
    public class Item
    {
        public string itemName = "itemName - index #";
        public bool hasCollected;
    }

    private void Start()
    {
        _items = items;
    }
}
