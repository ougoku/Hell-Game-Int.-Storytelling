using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    //note: make sure the gameobject this is attached to is on a UI layer or something so it doesn't move the character around when dragged

    //create a Vector2 variable (xy) and call it mousePosition. 
    private Vector2 mousePosition;
    //tracks the position of the mouse during gameplay
    private Vector2 hotSpot = Vector2.zero;
    //goes along with the setting in PlaySettings
    private float mouseToObjectDistance;
    //distance between the mouse to an object  
    private float mouseDistance;

    //assigns cursor textures :D
    public Texture2D interactiveCursor;                 
    public Texture2D defaultCursor; 
    //determines what the mouse cursor is being rendered by (Auto = hardware rendering. ForceSoftware = software rendering)
    //we're using Auto cus it'll run better as it doesn't depend on the game (software) to be rendered (if game lags it will lag and we don't want that :^))
    private CursorMode cursorMode = CursorMode.Auto;
    
    //variable to store initial object position so it can snap back later 
    private Vector3 startPos;
    public float objectToStartDistance;
    public Sprite slotSprite;
    public ParticleSystem heartParticles;

    // Start is called before the first frame update
    void Start()
    {
        //store initial object position
        startPos = this.transform.localPosition;
    }
    void Update()
    {
        slotSprite = this.GetComponent<SpriteRenderer>().sprite;

        //distance = object position - mouse position
        mouseToObjectDistance = Vector3.Distance(transform.position,mousePosition); 
        objectToStartDistance = Vector3.Distance(transform.localPosition,startPos); 
        //take the position of the mouse and assigns it to the Vector2 variable
        mousePosition = Input.mousePosition;           
        //get the location of the mouse and constrain it to the camera
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);                  
        mouseDistance = 3;
        //changes mouse cursor on hover and click + lets player drag object
        if (mouseToObjectDistance <= mouseDistance)
        {
            //make cursor look for texture, hotspot, and cursormode before changing it
            Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
        }

        // keep inventory updated
        int listMarker = int.Parse(this.name.Substring(this.name.Length - 1));
        Debug.Log("checking slot" + listMarker);
        checkInventory(listMarker);
    }
    void checkInventory(int listMarker)
    {
        for (int i = 0; i < Inventory._items.Length; i++)
        {
            if (i == listMarker && Inventory._items[listMarker].hasCollected == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = Inventory._items[listMarker].icon;
            }
        }
        
    }
    private void OnMouseDrag()
    {
        Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
        //assigns the position of the object to the position of our mouse
        if(Input.GetMouseButton(0))
        {
            transform.position = new Vector3(mousePosition.x, mousePosition.y, -5);
        }

    }
    private void OnMouseExit()
    {
        if(!Input.GetMouseButton(0))
        {
            Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);

            int numberIndex = this.name.Length - 1; //last char of sprite name is a # that ids which one it is
            //gets the number index of the current inventory slot
		    int listMarker = int.Parse(this.name.Substring(numberIndex));

            //grab the gameobject we wanna toss out our inventory
            GameObject target = Inventory._items[listMarker].currentItem;

            //snap the object back into the inventory slot if it's still relatively close'
            if(objectToStartDistance <= 2)
            {
                this.transform.localPosition = startPos;
            }
            //if it's far then just plop it back out into the world'
            else
            {
                if (target != null)
                {
                    //make it active again and put it where we toss it
                    target.transform.position = this.transform.position;
                    target.SetActive(true);

                    //empty inventory slot
                    emptyPlayerInventorySlot(listMarker);
                    //get rid of sprite in item slot
                    this.GetComponent<SpriteRenderer>().sprite = null;
                }
                //snap the now empty inventory slot back into the frame
                this.transform.localPosition = startPos;
                
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            //iterate through player inventory to find corresponding gameobject
            for (int i = 0; i < Inventory._items.Length; i++)
            {
                if(Inventory._items[i].icon == slotSprite && Inventory._items[i].itemName.Contains("foodItem") == true)
                {
                     Debug.Log("yummery");
                     this.GetComponent<SpriteRenderer>().sprite = null;
                     //activate heart particle System
                     heartParticles.Play();
                     //deactivate the object so that she "eats" it
                     if (Inventory._items[i].currentItem != null && Inventory._items[i].currentItem.activeSelf == true)
                     {
                         Inventory._items[i].currentItem.SetActive(false);
                     }
                     
                     emptyPlayerInventorySlot(i);
                     
                }
            }
        }
    }
    private void emptyPlayerInventorySlot(int itemIndex)
    {
        Inventory._items[itemIndex].itemName = "None";
        Inventory._items[itemIndex].hasCollected = false;
        Inventory._items[itemIndex].icon = null;
        Inventory._items[itemIndex].currentItem = null;
    }
}
