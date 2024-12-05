using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Vector3 offset;

    Transform myTransform;

    private void OnDrawGizmosSelected()
    {
        if (target) 
        {
            Gizmos.color = Color.black;
            Vector3 _offset = offset;
            _offset.z = -10;
            Gizmos.DrawWireSphere(target.position + _offset, .1f);
        }
    }

    void Start()
    {
        myTransform = GetComponent<Transform>();
        offset.z = -10;
    }

    void Update()
    {
        if(Vector3.Distance(target.position, myTransform.position) > 10)
            myTransform.position = target.position + offset;
        else
            myTransform.position = Vector3.Lerp(myTransform.position, target.position + offset, Time.fixedDeltaTime * speed);
    }
}
