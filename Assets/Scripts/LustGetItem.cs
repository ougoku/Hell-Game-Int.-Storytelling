using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustGetItem : MonoBehaviour
{
    public GameObject GoodLust;
    public GameObject BadLust;
    public GameObject NormalLust;
    public GameObject LustMirrorObject;
    public GameObject giantHole;
    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.tag == "Inventory")
        {
            
            if (item.GetComponent<SpriteRenderer>().sprite.name == "LustMirror")
            {
                NormalLust.SetActive(false);
                //activate good lust and deactivate normal lust
                LustMirrorObject.transform.position = this.transform.position;
                LustMirrorObject.SetActive(true);
                BadLust.SetActive(false);
                GoodLust.SetActive(true);
            }
            else
            {
                Debug.Log("wrong thing");
                NormalLust.SetActive(false);
                BadLust.SetActive(true);
                GoodLust.SetActive(false);

                this.GetComponent<SpriteRenderer>().sprite = null;
                
                giantHole.SetActive(true);
            }
        }
    }
}
