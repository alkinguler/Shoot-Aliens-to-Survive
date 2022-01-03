using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //character controller
    public CharacterController controller;
    public PlayerStats playerStats;

    //movement parameters
    public float speed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public int currentStamina;

    //ground and height
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float sprintingMultiplier = 1.5f;

    public bool isGrounded;
    public bool isSprinting;

    //velocity
    private Vector3 velocity;


    // Update is called once per frame
    void Update()
    {
        //check if it touches the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        currentStamina = playerStats.currentStamina;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moving by changing axis
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //jumping when pressed space script
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //running while holding shift script if stamina is not 0 //&& currentStamina > 0
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        //gravity effect
        velocity.y += gravity * Time.deltaTime;

        //move the controller times velocity (for jumping)
        controller.Move(velocity * Time.deltaTime);


        if (isSprinting == true)
        {
            controller.Move(move * speed * Time.deltaTime * sprintingMultiplier);
        }


    }

    public bool GetIsSprinting()
    {
        return isSprinting;
    }
    
    
}
