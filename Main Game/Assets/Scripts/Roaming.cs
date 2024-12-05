using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : MonoBehaviour
{
    public float speed;
    public MinMax pauseDuration;
    public Vector2 roamBounds;
    [Header("Leave None If No Directional Animation")]
    public Animator myAnimator;
    [Header("Down-0, Up-1, Left-2, Right-3")]
    public DirectionPair[] directionPairs;

    Rigidbody2D myRigidbody;
    int direction = 0;
    Vector3 origin;
    Vector2 waypoint;
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        if(!Application.isPlaying)
            origin = transform.position;

        Gizmos.DrawWireCube(origin, roamBounds);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        origin = myRigidbody.position;
        waypoint = RandomWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            if (Vector2.Distance(waypoint, myRigidbody.position) < .05f) // Reached Target
            {
                waypoint = RandomWaypoint();
                StartCoroutine(Pause());
            }
            else
            {
                Vector2 moveDirection = (waypoint - myRigidbody.position).normalized;
                myRigidbody.MovePosition(myRigidbody.position + (moveDirection * speed * Time.deltaTime));

                if (myAnimator != null)
                {
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
                }
            }
        }
    }

    IEnumerator Pause()
    {
        isPaused = true;

        if (myAnimator != null)
            myAnimator.Play(directionPairs[direction].idle);

        yield return new WaitForSeconds(Random.Range(pauseDuration.min, pauseDuration.max));

        isPaused = false;
    }

    Vector2 RandomWaypoint()
    {
        Vector2 tempWaypoint = origin;
        float xOffset = roamBounds.x * .5f;
        float yOffset = roamBounds.y * .5f;

        //Random.Range(min, max)
        tempWaypoint.x += Random.Range(-xOffset, xOffset);
        tempWaypoint.y += Random.Range(-yOffset, yOffset);

        return tempWaypoint;
    }
}
