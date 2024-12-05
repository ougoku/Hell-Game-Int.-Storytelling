using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameObject triggeringObject;
    public int itemIndex;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == triggeringObject)
        {
            Inventory._items[itemIndex].hasCollected = true;
        }
    }
}
