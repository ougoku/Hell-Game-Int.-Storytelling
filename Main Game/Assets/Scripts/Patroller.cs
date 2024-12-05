using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{

    public float speed;
    public MinMax pauseDuration;
    public LoopMode loopMode;
    public List <Vector3> waypoints = new List<Vector3>();
    [Header("Leave None If No Directional Animation")]
    public Animator myAnimator;
    [Header("Down-0, Up-1, Left-2, Right-3")]
    public DirectionPair[] directionPairs;

    Rigidbody2D myRigidbody;
    int direction = 1, target = 0, moveDirection = 0;
    Vector3 origin;
    bool isPaused;

    [System.Serializable]
    public class DirectionPair
    {
        public string idle, movement;
    }

    [System.Serializable]
    public class MinMax 
    {
        public float min, max;
    }

    public enum LoopMode { Loop, PingPong};


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        List<Vector3> _waypoints = new List<Vector3>(waypoints);
        Vector3 startingPoint = Vector3.zero;

        if (!Application.isPlaying)
        {
            _waypoints.Insert(0, Vector3.zero);
            startingPoint = transform.position;
        }

        for (int i = 0; i < _waypoints.Count; i++)
        {
            Gizmos.DrawWireSphere(startingPoint + _waypoints[i], .1f);

            if(i>0)
                Gizmos.DrawLine(startingPoint + _waypoints[i-1], startingPoint + _waypoints[i]);
        }

        if(loopMode == LoopMode.Loop)
            Gizmos.DrawLine(startingPoint + _waypoints[0], startingPoint + _waypoints[_waypoints.Count - 1]);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        origin = myRigidbody.position;
        waypoints.Insert(0, Vector3.zero);

        for (int i=0; i<waypoints.Count; i++) 
        {
            waypoints[i] += origin;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Vector2 targetPosition = waypoints[target];

            if (Vector2.Distance(targetPosition, myRigidbody.position) < .05f) // Reached Target
            {
                if (loopMode == LoopMode.Loop && target == waypoints.Count - 1) // Loop
                    target = -1;
                else if (loopMode == LoopMode.PingPong) // PingPong
                {
                    if (direction == 1 && target == waypoints.Count - 1)
                        direction = -1;
                    else if (direction == -1 && target == 0)
                        direction = 1;
                }

                target += direction;
                StartCoroutine(Pause());
            }
            else 
            {
                Vector2 move = (targetPosition - myRigidbody.position).normalized;
                myRigidbody.MovePosition(myRigidbody.position + (move * speed * Time.deltaTime));

                if (myAnimator != null)
                {
                    if (move.y < -0.5) //Down Movement
                    {
                        moveDirection = 0;
                        myAnimator.Play(directionPairs[moveDirection].movement);
                    }
                    else if (move.y > 0.5) //Up Movement
                    {
                        moveDirection = 1;
                        myAnimator.Play(directionPairs[moveDirection].movement);
                    }
                    else if (move.x < 0) //Left Movement
                    {
                        moveDirection = 2;
                        myAnimator.Play(directionPairs[moveDirection].movement);
                    }
                    else if (move.x > 0) //Right Movement
                    {
                        moveDirection = 3;
                        myAnimator.Play(directionPairs[moveDirection].movement);
                    }
                }
            }
        }
    }

    IEnumerator Pause() 
    {
        isPaused = true;

        if (myAnimator != null)
            myAnimator.Play(directionPairs[moveDirection].idle);

        yield return new WaitForSeconds(Random.Range(pauseDuration.min, pauseDuration.max));

        isPaused = false;
    }
}
