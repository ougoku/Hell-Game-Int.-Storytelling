using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script loosely referenced from this [https://www.youtube.com/watch?v=IgBjJ-bexeo] youtube video

public class slidingPuzzleMechanics: MonoBehaviour
{
	//reference to gameboard transform (you can drag the gameboard gameobject in here from the inspector tab)
	public Transform gameboardTransform;
	//reference to gamepiece transform (you can drag the prefab in here from the inspector tab)
	public Transform piecePrefabTransform;
	//list to hold all tile pieces
	public List<Transform> listOfPieces;
 	//list to hold all the different tile sprites
	public Sprite[] tileSprites;
	//holds the empty slot in puzzle
	public int emptyLocation;
	//sets size of the game (int x int)
	public int boardSize;
	private float borderThickness;
 	//holds the full tile image that appears when the game is completed
	public GameObject fullImage;
 	//holds the mirror object so that when the puzzle is complete, then this is set active for the player to collect
	public GameObject LustMirror;
 
	public Vector3 mouseToObjectDistance;
	public Vector3 mousePosition;

	void Start()
	{
		//sets size of puzzle to a 3x3 grid and the size of the border between tiles
		boardSize = 3;
		borderThickness = .02f;
		//initialize list
		listOfPieces = new List<Transform>();
		//shuffle board first 
		shuffleBoard();
		//method to create the game pieces
		createGamePieces(borderThickness, boardSize);

	}

	//shuffles pieces before creation
	private void shuffleBoard()
	{
		//store the last sprite
		Sprite emptySprite = tileSprites[tileSprites.Length - 1];
		int inversionCount = 0;
		do
		{
			//use the Knuth/Fisher-Yates shuffle to shuffle sprite array
			for (int i = tileSprites.Length - 1; i > 0; i--)
			{
				int randomIndex = Random.Range(0, tileSprites.Length);

				Sprite temp = tileSprites[i];
				tileSprites[i] = tileSprites[randomIndex];
				tileSprites[randomIndex] = temp;
			}
			//count number of inversions. if it's not even then we'll have to reshuffle 
			inversionCount = checkValidPuzzle();
		}  while (inversionCount % 2 != 0);
	}

	//iterates through sprite array and counts how many inversions there are
 	//function referenced from this lovely geeksforgeeks article [https://www.geeksforgeeks.org/check-instance-15-puzzle-solvable/]
	private int checkValidPuzzle()
	{
		int inversionCount = 0;
  		//lil print test to print out the sprites after they got shuffled and before the inversions get counted
		//Debug.Log("Sprites: "+ tileSprites[0] + tileSprites[1] + tileSprites[2] + tileSprites[3] + tileSprites[4] + tileSprites[5] + tileSprites[6] + tileSprites[7] + tileSprites[8]);
		for (int i = 0; i < tileSprites.Length - 1; i++) 
		{
            		for (int j = i + 1; j < tileSprites.Length; j++) 
			{
				//grabs the int value of the sprite name for comparisons
				int numberIndex = tileSprites[i].name.Length - 1; //last char of sprite name is a # that ids which one it is
				int iString = int.Parse(tileSprites[i].name.Substring(numberIndex));
				int jString = int.Parse(tileSprites[j].name.Substring(numberIndex));
    
                		if ((iString != 8 && jString != 8) && (iString > jString))
				{
					inversionCount++;
				}
            		}
        	}
		//Debug.Log("Inversion Count: " + inversionCount);
		return inversionCount;
	}

	//creates game tiles
	private void createGamePieces(float borderThickness, int boardSize)
	{
		//create width of each tile using typecasting (wow)
		float width = 1/((float)boardSize);
		int tileIndex = 0;
		//for loop to instantiate each game piece and place them down
		for (int row = 0; row < boardSize; row++)
		{
			for (int col = 0; col < boardSize; col++)
			{
				//creates each game piece :^)
				Transform newPiece = Instantiate(piecePrefabTransform, gameboardTransform);
				//add new piece to list
				listOfPieces.Add(newPiece);
				//assign sprite to the piece
				newPiece.gameObject.GetComponent<SpriteRenderer>().sprite = tileSprites[tileIndex];
				tileIndex++;

				//place each piece onto the gameboard (gameboard will go from -1 to +1 along the x axis for it to be centered)
    				//i tweaked the measurements a little and now this only really works if your full tile image is 1064x1064 before it gets split up otherwise the positioning is off
				newPiece.localPosition = new Vector3(-1 + (2 * boardSize * col) + borderThickness, +1 - (2 * boardSize * row) - borderThickness, 0);

				//scale the piece
				newPiece.localScale = ((5 * width) - borderThickness)  * Vector3.one;
				//name each piece using its index in the puzzle
				newPiece.name = $"{(row * boardSize) + col}";

				//covers the case of the empty space.
				if ((row == boardSize - 1) && (col == boardSize - 1))
				{
					emptyLocation = (boardSize * boardSize) - 1;
					newPiece.gameObject.SetActive(false);
				}
			}
		}
	}
	void Update()
	{
		//check if game is complete 
        	bool isComplete = isGameComplete();
		if (isComplete == true)
		{
		    gameboardTransform.gameObject.SetActive(false);
		    fullImage.SetActive(true);
		    //make the mirror visible so player can collect it
		    LustMirror.SetActive(true);
		}
	}
	//check if game is complete
	private bool isGameComplete()
	{
		//iterate through game pieces and if they have the correct sprites then game is completed
		for (int i = 0; i < listOfPieces.Count; i++)
		{
			//breaking this up into different variables so i don't have some egregiously long single variable
			int numberIndex = tileSprites[i].name.Length - 1;
			string currentTileSprite = listOfPieces[i].gameObject.GetComponent<SpriteRenderer>().sprite.name;
			int spriteNumber = int.Parse(currentTileSprite.Substring(numberIndex));

			//Debug.Log("Tile " + i + " has sprite " + spriteNumber);
			//if the current tile != sprite number then game isn't complete
			if(i != spriteNumber)
			{
				return false;
			}
		}
		return true;
	}

 	//accessors for TileController 
	public List<Transform> getTileList()
	{
		return this.listOfPieces;
	}
	public int getBoardSize()
	{
		return this.boardSize;
	}
	public Sprite[] getTileSprites()
	{
		return this.tileSprites;
	}
	public GameObject getFullTileImage()
	{
		return this.fullImage;
	}

}
