using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthKill : MonoBehaviour
{
   
    public GameObject sceneManager;

    // This function is NOT built in to Unity
    // It will only be called manually by our own code
    // It must be marked "public" so our other scripts can access it
    public void Kill()
    {
       SceneChanger gameover = sceneManager.GetComponent<SceneChanger>();
        gameover.GameOver(); 
    }
}

