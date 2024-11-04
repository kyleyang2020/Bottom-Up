using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Concerning the Player Input System, there are 4 ways to do behavior, Send Message, Broadcast Message, etc.
 * Send Message only looks for the method on the specific GameObject that the Player Input is attached too
 * Broadcast can do the same, but also on its children, besides that it's all the same
 */

public class Movmement2D : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public PlayerInput playerInput;
    private PlayerController playerController;

    [Header("Player Stats")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;

    [Header("Trackers")]
    public bool isCrouching;
    public bool isSprinting;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerController = new PlayerController();
    }

    void Update()
    {
        if (rb.velocity.magnitude == 0)
        {
            sr.color = Color.blue;
        }
    }

    // ------------------------------Unity Input System Functions------------------------------ //

    // movement, sprint not working
    private void OnWASD(InputValue inputValue)
    {
        // if sprint (a button in the PlayerController Input Action is pressed) it'll change the speed of movement
        if (isSprinting)
        {
            rb.velocity = inputValue.Get<Vector2>() * sprintSpeed;
            sr.color = Color.red;
        }
        // on WASD, input system will take the Vector2 from pressing WASD and multiple it with move speed and move
        else
        {
            rb.velocity = inputValue.Get<Vector2>() * walkSpeed;
            sr.color = Color.green;
        }
    }

    private void OnSprintStart()
    {
        isSprinting = true;
    }

    private void OnSprintFinish()
    {
        isSprinting = false;
    }

    private void onCrouch()
    {
        Debug.Log("crouch");
    }

    // --------------------------------Built-In Unity Functions-------------------------------- //
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
