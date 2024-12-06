using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject triggeringObject;
    public TriggerEvent eventType;
    public bool loopable;
    public AnimationPair[] animationPairs;

    [Header("Only Trigger W/ Item (-1 if no item required)")]
    public int itemIndex = -1;

    [Header("Scene Changing Options (-1 for no change)")]
    public int nextScene = -1;

    [Header("Update Spawnpoint for this Scene")]
    public bool setNewSceneSpawnpoint;
    public Vector2 newSpawnpoint = new Vector2(0, 0);

    public enum TriggerEvent { OnEnter, OnExit };

    [System.Serializable]
    public class AnimationPair
    {
        public string clipName;
        public Animator myAnimator;
    }

    private void OnDrawGizmosSelected()
    {
        if (setNewSceneSpawnpoint)
        {
            Gizmos.color = Color.cyan;
            Vector3 _newSpawnpoint = newSpawnpoint;
            Gizmos.DrawWireSphere(transform.position + _newSpawnpoint, .1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((triggeringObject == null || collider.gameObject == triggeringObject) && (itemIndex == -1 || Inventory._items[itemIndex].hasCollected))
        {
            if (nextScene > -1)
                SceneChanger.nextScene = nextScene;

            if (setNewSceneSpawnpoint)
                Controller.SetNewSpawnpoint(new Controller.SpawnpointPairs(SceneManager.GetActiveScene().buildIndex, newSpawnpoint + new Vector2(transform.position.x, transform.position.y)));

            if (eventType == TriggerEvent.OnEnter)
            {
                foreach (var pair in animationPairs)
                {
                    if (loopable)
                        pair.myAnimator.Play(pair.clipName, -1, 0);
                    else
                        pair.myAnimator.Play(pair.clipName);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (triggeringObject == null || collider.gameObject == triggeringObject)
        {
            if (nextScene > -1)
                SceneChanger.nextScene = nextScene;

            if (setNewSceneSpawnpoint)
                Controller.SetNewSpawnpoint(new Controller.SpawnpointPairs(SceneManager.GetActiveScene().buildIndex, newSpawnpoint + new Vector2(transform.position.x, transform.position.y)));

            if (eventType == TriggerEvent.OnExit)
            {
                foreach (var pair in animationPairs)
                {
                    if (loopable)
                        pair.myAnimator.Play(pair.clipName, -1, 0);
                    else
                        pair.myAnimator.Play(pair.clipName);
                }
            }
        }
    }
}
