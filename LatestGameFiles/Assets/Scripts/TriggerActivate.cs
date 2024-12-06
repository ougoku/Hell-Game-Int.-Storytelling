using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    public GameObject triggeringObject;
    public GameObject[] activateObjects;
    public GameObject[] deactivateObjects;
    public GameObject[] toggleObjects;

    [Header("Only Trigger W/ Item (-1 if no item required)")]
    public int itemIndex = -1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == triggeringObject && (itemIndex == -1 || Inventory._items[itemIndex].hasCollected))
        {
            foreach (var go in activateObjects)
            {
                go.SetActive(true);
            }

            foreach (var go in deactivateObjects)
            {
                go.SetActive(false);
            }

            foreach (var go in toggleObjects)
            {
                go.SetActive(go.activeSelf ? false : true);
            }
        }
    }
}
