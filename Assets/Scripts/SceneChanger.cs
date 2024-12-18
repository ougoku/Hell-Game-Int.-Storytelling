using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int _nextScene;

    public static int nextScene;

    private void Start()
    {
        nextScene = _nextScene;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Gameover");
    }
}
