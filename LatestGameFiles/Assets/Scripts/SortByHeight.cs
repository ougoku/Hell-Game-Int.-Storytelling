using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByHeight : MonoBehaviour
{
    public bool constantly;
    public float yOffset = 0;

    SpriteRenderer myRenderer;
    Transform myTransform;
    ParticleSystemRenderer myParticle;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();

        if (GetComponent<SpriteRenderer>())
        {
            myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sortingOrder = Mathf.RoundToInt((myTransform.position.y + yOffset) * -100);
        }

        if (GetComponent<ParticleSystemRenderer>())
        {
            myParticle = GetComponent<ParticleSystemRenderer>();
            myParticle.sortingOrder = Mathf.RoundToInt((myTransform.position.y + yOffset) * -100);
        }
    }

    void Update()
    {
        if (constantly) 
        {
            if (GetComponent<SpriteRenderer>())
            {
                myRenderer = GetComponent<SpriteRenderer>();
                myRenderer.sortingOrder = Mathf.RoundToInt((myTransform.position.y + yOffset) * -100);
            }

            if (GetComponent<ParticleSystemRenderer>())
            {
                myParticle = GetComponent<ParticleSystemRenderer>();
                myParticle.sortingOrder = Mathf.RoundToInt((myTransform.position.y + yOffset) * -100);
            }
        }
    }
}
