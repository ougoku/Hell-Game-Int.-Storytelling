using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Controller;

public class Controller : MonoBehaviour
{
    public float speed;
    [Header("Down-0, Up-1, Left-2, Right-3")]
    public int direction = 0; // down=0, up=1, left=2, right=3
    public DirectionPair[] directionPairs;
    public Animator myAnimator;
    
    Rigidbody2D myRigidbody;

    public static bool isPaused;
    static List <SpawnpointPairs> spawnpoints = new List<SpawnpointPairs>();

    [System.Serializable]
    public class DirectionPair
    {
        public string idle;
        public string movement;
    }

    public class SpawnpointPairs 
    {
        public int sceneIndex;
        public Vector2 position;

        //constructor. captures given scene index and a position into their own variables
        public SpawnpointPairs(int _sceneIndex, Vector2 _position) 
        { 
            sceneIndex = _sceneIndex;
            position = _position;
        }
    }

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        CheckSpawnpoints();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            //Input.GetAxis has a value range of -1 to 1 
            //0 means neutral. -1 means all the way to the up/left. 1 means all the way down/ right
            Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            myRigidbody.MovePosition(myRigidbody.position + (moveDirection * speed * Time.deltaTime));

            if (moveDirection.y < -0.5) //Down Movement
            {
                direction = 0;
                myAnimator.Play(directionPairs[direction].movement);
            }
            else if (moveDirection.y > 0.5) //Up Movement
            {
                direction = 1;
                myAnimator.Play(directionPairs[direction].movement);
            }
            else if (moveDirection.x < 0) //Left Movement
            {
                direction = 2;
                myAnimator.Play(directionPairs[direction].movement);
            }
            else if (moveDirection.x > 0) //Right Movement
            {
                direction = 3;
                myAnimator.Play(directionPairs[direction].movement);
            }
            else //Idles
            {
                myAnimator.Play(directionPairs[direction].idle);
            }
        }
        else //Idles
        {
            myAnimator.Play(directionPairs[direction].idle);
        }
    }

    void CheckSpawnpoints() 
    {
        bool spawnpointFound = false;

        for (int i = 0; i < spawnpoints.Count; i++)
        {
            if (spawnpoints[i].sceneIndex == SceneManager.GetActiveScene().buildIndex)
            {
                spawnpointFound = true;
                myRigidbody.position = spawnpoints[i].position;
                break;
            }
        }

        if(!spawnpointFound)
            spawnpoints.Add(new SpawnpointPairs(SceneManager.GetActiveScene().buildIndex, myRigidbody.position));

    }

    public static void SetNewSpawnpoint(SpawnpointPairs spawnpointPair) 
    {
        for (int i = 0; i < spawnpoints.Count; i++)
        {
            if (spawnpoints[i].sceneIndex == spawnpointPair.sceneIndex) 
            {
                spawnpoints[i] = spawnpointPair;
                break;
            }
        }
    }
}
