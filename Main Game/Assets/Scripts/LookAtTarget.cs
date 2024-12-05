using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public Sprite down, up, side;
    public bool flipSides;

    Transform myTransform;
    SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.position - myTransform.position).normalized;

        if (direction.y >= .5)
            myRenderer.sprite = up;
        else if (direction.y <= -.5)
            myRenderer.sprite = down;
        else 
        {
            myRenderer.sprite = side;

            if (direction.x > 0)
                myRenderer.flipX = flipSides ? false : true;
            else
                myRenderer.flipX = flipSides ? true : false;
        }
    }
}
