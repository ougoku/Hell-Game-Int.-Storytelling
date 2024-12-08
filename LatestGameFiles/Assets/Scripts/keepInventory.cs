using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepInventory : MonoBehaviour
{
    private static GameObject sampleInstance;
    private void Awake()
    {
        if (sampleInstance != null)
        {
            Destroy(sampleInstance);
        }
        //turn dupe into og
        sampleInstance = gameObject;
        DontDestroyOnLoad(this);
    }

}
