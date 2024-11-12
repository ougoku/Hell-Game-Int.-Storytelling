using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }


    public void gameScene01()
    {
        SceneManager.LoadScene("Floor01");
    }

    /*  public void endGameLose()
      {
          SceneManager.LoadScene("endGameLose");
      }

      public void endGameWin()
      {
          SceneManager.LoadScene("endGameWin");
      }*/

    public void gameScene02()
    {
        SceneManager.LoadScene("Floor02");
    }

    public void winScene()
    {
        SceneManager.LoadScene("win");
    }


}