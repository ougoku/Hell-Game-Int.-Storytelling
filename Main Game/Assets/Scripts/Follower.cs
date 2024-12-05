using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Follower : MonoBehaviour
{
    public Transform target;
    public float speed, stopRadius, followRadius, teleportRadius;
    public Vector2 targetOffset, startPosition;
    [Header("Leave None If No Directional Animation")]
    public Animator myAnimator;
    [Header("Down-0, Up-1, Left-2, Right-3")]
    public DirectionPair[] directionPairs;

    Rigidbody2D myRigidbody;
    int direction = 0;
    bool isFollowing = false;

    [System.Serializable]
    public class DirectionPair
    {
        public string idle, movement;
    }

    private void OnDrawGizmosSelected()
    {
        if (target)
        {
            Gizmos.color = Color.red;
            Vector3 _targetOffset = targetOffset;
            Gizmos.DrawWireSphere(target.position + _targetOffset, stopRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.position + _targetOffset, followRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(target.position + _targetOffset, teleportRadius);

            Vector3 _startPosition = startPosition;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target.position + _startPosition, .1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        Vector3 _startPosition = startPosition;
        myRigidbody.position = target.position + _startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = target.position;

        if (!isFollowing && Vector2.Distance(myRigidbody.position, targetPosition + targetOffset) > followRadius)
        {
            isFollowing = true;
        }
        else if (isFollowing && Vector2.Distance(myRigidbody.position, targetPosition + targetOffset) > stopRadius) 
        {
            Vector2 moveDirection = (targetPosition - myRigidbody.position).normalized;
            myRigidbody.MovePosition(myRigidbody.position + (moveDirection * speed * Time.deltaTime));

            if (myAnimator != null) {
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
        else //Idles
        {
            isFollowing = false;
            myAnimator.Play(directionPairs[direction].idle);
        }

        if (Vector2.Distance(myRigidbody.position, targetPosition + targetOffset) > teleportRadius)
        {
            Vector3 _startPosition = startPosition;
            myRigidbody.position = target.position + _startPosition;
        }
    }
}
