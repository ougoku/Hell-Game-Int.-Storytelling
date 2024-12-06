using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkullDragDrop : MonoBehaviour
{
    //create a Vector2 variable (xy) and call it mousePosition. 
    public Vector3 mousePosition;
    //tracks the position of the mouse during gameplay
    public Vector2 hotSpot = Vector2.zero;
    //goes along with the setting in PlaySettings
    public float mouseToObjectDistance;
    //distance between the mouse to an object  
    public float mouseDistance;

    //assigns cursor textures :D
    public Texture2D interactiveCursor;                 

    //determines what the mouse cursor is being rendered by (Auto = hardware rendering. ForceSoftware = software rendering)
    //we're using Auto cus it'll run better as it doesn't depend on the game (software) to be rendered (if game lags it will lag and we don't want that :^))
    private CursorMode cursorMode = CursorMode.Auto;
    
    //variable to store initial object position so it can snap back later 
    public Vector3 startPos;
   
    void Start()
    {
        //store initial object position
        startPos = this.transform.position;                                             
    }

    void Update()
    {
        //distance = object position - mouse position
        mouseToObjectDistance = Vector3.Distance(transform.position,mousePosition);  
        //take the position of the mouse and assigns it to the Vector2 variable
        mousePosition = Input.mousePosition;           
        //get the location of the mouse and constrain it to the camera
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);                  
        mouseDistance = 2;

        //changes mouse cursor on hover and click + lets player drag object
        if (mouseToObjectDistance <= mouseDistance)
        {
            //make cursor look for texture, hotspot, and cursormode before changing it
            Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
        }
    }
    void OnMouseOver()
    {
       //make cursor look for texture, hotspot, and cursormode before changing it
       Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
    }
    private void OnMouseDrag()
    {
        Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
        //assigns the position of the object to the position of our mouse
        transform.position = new Vector3(mousePosition.x, mousePosition.y, startPos.z);
    }
}