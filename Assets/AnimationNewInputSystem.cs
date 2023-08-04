using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Rendering;

public class AnimationNewInputSystem : MonoBehaviour
{
    private Animator animator;
    public MovementScript movementScript;
    public Rigidbody2D rb;
    private string currentAnimation;

    private float horizontal;
    private float afterJump = 0.6f;
    private int counter = 0;
    private bool hasLanded;
    private bool isRunning;


    // Animation States
    const string PLAYER_IDLE = "piggieIdle";
    const string PLAYER_RUN = "piggieRun";
    const string PLAYER_JUMP = "piggieJump";
    const string PLAYER_DOUBLE_JUMP = "piggieJump";
    const string PLAYER_DASH = "piggieDash";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(PLAYER_IDLE);
    }


    // Update is called once per frame
    void Update()
    {
        if (movementScript.IsGrounded())
        {
            if (hasLanded == true)
            {
                ChangeAnimationState(PLAYER_IDLE);
                setFalse(hasLanded);

                Debug.Log("Yes has landed");
            }

            if (rb.velocity.x != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }

            if (rb.velocity.x == 0)
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        if (!movementScript.IsGrounded())
        {
            if (rb.velocity.y != 0 && rb.velocity.y >= -10f)
            {
                ChangeAnimationState(PLAYER_JUMP);
            }

            if (rb.velocity.y < -10f)
            {
                ChangeAnimationState(PLAYER_DOUBLE_JUMP);
            }
        }
    }

    

    private void setFalse(bool hasLanded)
    {
        this.hasLanded = false;
    }

    // Adds jump animation.
    public void jumpAnimation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!movementScript.IsGrounded())
            {
                ChangeAnimationState(PLAYER_DOUBLE_JUMP);
            }
        }

        if (context.canceled && movementScript.IsGrounded())
        {
            hasLanded = true;

            ChangeAnimationState(PLAYER_IDLE);

            Debug.Log("Cancelled");
        }
    }

    public void runAnimation(InputAction.CallbackContext context)
    {

    }


    public void dashAnimation(InputAction.CallbackContext context)
    {

    }

    void ChangeAnimationState(string newState)
    {
        // stops interruption between animations
        if (currentAnimation == newState) return;

        animator.Play(newState);

        // reassign current state
        currentAnimation = newState;
    }
}

