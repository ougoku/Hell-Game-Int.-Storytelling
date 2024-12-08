using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Vector2 defaultCursorHotSpot;
    public Texture2D hoverCursor;
    public Vector2 hoverCursorHotSpot;
    public Texture2D clickCursor;
    public Vector2 clickCursorHotSpot;
    public AudioClip objectclick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonCursorEnter()
    {
        Cursor.SetCursor(hoverCursor, hoverCursorHotSpot, CursorMode.Auto);

    }
    public void OnButtonCursorClick()
    {
        Cursor.SetCursor(clickCursor, clickCursorHotSpot, CursorMode.Auto);
    }
    public void OnButtonCursorExit()
    {
        Cursor.SetCursor(defaultCursor, defaultCursorHotSpot, CursorMode.Auto);
    }

    // Audio
    public void playAudio(AudioClip clip)   
    {
        GetComponent<AudioSource>().PlayOneShot(clip, .8f);    
    }
}
