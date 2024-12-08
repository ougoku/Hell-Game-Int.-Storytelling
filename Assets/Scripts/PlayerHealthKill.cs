using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthKill : MonoBehaviour
{
    public GameObject sceneManager;

    // This function is called when the player is hit
    public void Kill()
    {
        // Get the SceneChanger component from the scene manager
        SceneChanger gameover = sceneManager.GetComponent<SceneChanger>();

        // Call the GameOver method to trigger the game over screen or behavior
        gameover.GameOver();
    }

    // This method is called when a collider enters the trigger of the enemy
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the object that collided is the player
        if (collider.CompareTag("Player"))
        {
            // Call the Kill function when the player collides with the enemy
            Kill();
        }
    }
}

