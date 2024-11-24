using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private PlayerInput playerInput;
    private PlayerController playerController;

    [Header("Player Movement")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;

    [Header("Player Stats")]
    [SerializeField] private float sprintStamina;
    [SerializeField] private float breathStamina;

    public Slider sprintSlider;
    public Slider breathSlider;

    [Header("Trackers")]
    public bool isCrouching;
    public bool isSprinting;
    public 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerController = new PlayerController();

        sprintSlider.value = 1;
        breathSlider.value = 1;
    }

    void Update()
    {
        // when not moving color blue
        if (rb.velocity.magnitude == 0) {sr.color = Color.blue;}

        if (isSprinting)
            DecreaseStamina();
        if (isCrouching)
            DecreaseBreath();
        if(!isSprinting)
            IncreaseStamina();
        if(!isCrouching)
            IncreaseBreath();
    }

    #region Sprint/Crouch Bar
    public void DecreaseStamina()
    {
        sprintSlider.value -= (1 / sprintStamina);
    }

    public void IncreaseStamina()
    {
        sprintSlider.value += (1 / sprintStamina);
    }

    public void DecreaseBreath()
    {
        breathSlider.value -= (1 / breathStamina);
    }

    public void IncreaseBreath()
    {
        breathSlider.value += (1 / breathStamina);
    }
    #endregion 

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
        // when crtl is held, change color and spd
        else if (isCrouching)
        {
            rb.velocity = inputValue.Get<Vector2>() * crouchSpeed;
            sr.color = Color.black;
        }
        // on WASD, input system will take the Vector2 from pressing WASD and multiple it with move speed and move
        else
        {
            rb.velocity = inputValue.Get<Vector2>() * walkSpeed;
            sr.color = Color.green;
        }
    }

    #region Sprint/Crouch Input Functions
    private void OnSprintStart()
    {
        isSprinting = true;
    }

    private void OnSprintFinish()
    {
        isSprinting = false;
    }

    private void OnCrouchStart()
    {
        isCrouching = true;
    }

    private void OnCrouchFinish()
    {
        isCrouching = false;
    }
    #endregion

    // --------------------------------Built-In Unity Functions-------------------------------- //
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
