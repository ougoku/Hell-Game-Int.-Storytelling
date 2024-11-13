using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private Animator playerAnimator;
    public float speed;
    public float input;
    public SpriteRenderer Player_Forward;
    public bool canJump;
    public float playerHealth; // Current health of the player
    public float playerMaxHealth; // Maximum health of the player


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input > 0)
        {
            playerAnimator.SetBool("isWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (input < 0)
        {
            playerAnimator.SetBool("isWalking", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (input == 0)//(input < 1 && input > -1)
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }
}