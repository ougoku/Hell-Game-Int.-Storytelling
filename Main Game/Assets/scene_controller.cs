using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scene_controller : MonoBehaviour
{
    public void MainMenu() //loads the main menu when called
    {
        SceneManager.LoadScene("Mainmenu");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
