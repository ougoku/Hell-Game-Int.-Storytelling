using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    //create a Vector2 variable (xy) and call it mousePosition. 
    public Vector2 mousePosition;
    public Vector3 objectPosition;
    //tracks the position of the mouse during gameplay
    public Vector2 hotSpot = Vector2.zero;
    //goes along with the setting in PlaySettings
    public float mouseToObjectDistance;
    //distance between the mouse to an object  
    public double mouseDistance;

    //assigns cursor textures :D
    public Texture2D interactiveCursor;    
    public Texture2D defaultCursor;

    //determines what the mouse cursor is being rendered by (Auto = hardware rendering. ForceSoftware = software rendering)
    //we're using Auto cus it'll run better as it doesn't depend on the game (software) to be rendered (if game lags it will lag and we don't want that :^))
    private CursorMode cursorMode = CursorMode.Auto;

    //reference to the slidingPuzzleMechanics script
    private slidingPuzzleMechanics puzzleScript;
    //reference to the gamemanager game object
    private GameObject puzzleObject;
    //reference to list of puzzle tiles
    public List<Transform> listOfPieces;
    //reference to the sprite list for the tile
    private Sprite[] tileSprites;
    //reference to the board size so we don't have to hardcode a number
    private int boardSize;

    public GameObject fullImage;

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.localPosition;

        //calls the accessor methods in the tileSlider script so we can get the info from there 
        puzzleObject = GameObject.Find("Gamemanager-GameObject");
        puzzleScript = puzzleObject.GetComponent<slidingPuzzleMechanics>();
        listOfPieces = puzzleScript.getTileList();
        boardSize = puzzleScript.getBoardSize();
        tileSprites = puzzleScript.getTileSprites();
        fullImage = puzzleScript.getFullTileImage();
        
    }

    void OnMouseOver()
    {
       //make cursor look for texture, hotspot, and cursormode before changing it
       Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
       if (Input.GetMouseButtonDown(0))
       {
            //iterate through the list of pieces and to find the corresponding hit piece
		    for (int listIndex = 0; listIndex < listOfPieces.Count; listIndex++)
		    {
                //if we clicked a tile, check to see if we can make any valid moves
                if (objectPosition == listOfPieces[listIndex].localPosition)
                {
                    //Debug.Log("You hit tile " + listIndex);
                    int currentTileIndex = listIndex;
                    int aboveIndex = currentTileIndex - boardSize;
                    int belowIndex = currentTileIndex + boardSize;
                    int leftIndex = currentTileIndex - 1;
                    int rightIndex = currentTileIndex + 1;

                    //Debug.Log("Possible swaps: " + aboveIndex + " " + belowIndex + " " + leftIndex + " " + rightIndex); 
                    
                    //check for possible swaps, if there are, then break 
                    //if statements galore!!!!
                    if (isSwapPossible(currentTileIndex, aboveIndex))
                    {
                        break;
                    }
                    if (isSwapPossible(currentTileIndex, belowIndex))
                    {
                        break;
                    }

                    //makes sure that only the middle and left col tiles can move right
                    if (currentTileIndex % boardSize == 1 || currentTileIndex % boardSize == 0)
                    {
                        if (isSwapPossible(currentTileIndex, rightIndex))
                        {
                            break;
                        }
                    }
                    //makes sure that only the middle and right col tiles can move left
                    if (currentTileIndex % boardSize == 1 || currentTileIndex % boardSize == 2)
                    {
                        if (isSwapPossible(currentTileIndex, leftIndex))
                        {
                            break;
                        }
                    }
                }
            }
       }
    }

    //change mouse back to normal when player takes their mouse off the puzzle
    void OnMouseExit()
    {
        //change cursor back to normal 
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    //check for any possible tile swaps and swap
    private bool isSwapPossible(int currentTileIndex, int targetTileIndex)
    {
        //check if target tile is in range of the exisiting tiles and that the target is empty
        if(0 <= targetTileIndex && targetTileIndex < listOfPieces.Count && listOfPieces[targetTileIndex].gameObject.activeSelf == false)
        {
            //Debug.Log("Tile " + targetTileIndex + " is free to swap");
            //swap their sprites
			(listOfPieces[currentTileIndex].gameObject.GetComponent<SpriteRenderer>().sprite, listOfPieces[targetTileIndex].gameObject.GetComponent<SpriteRenderer>().sprite) = (listOfPieces[targetTileIndex].gameObject.GetComponent<SpriteRenderer>().sprite, listOfPieces[currentTileIndex].gameObject.GetComponent<SpriteRenderer>().sprite);
            //set the current tile as inactive and target tile as active
            listOfPieces[currentTileIndex].gameObject.SetActive(false);
            listOfPieces[targetTileIndex].gameObject.SetActive(true);
            return true;
        }
        return false;
    }
} 
