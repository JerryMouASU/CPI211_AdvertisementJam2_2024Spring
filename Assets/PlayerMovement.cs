
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float acceleration = 0.5f;
    public float decelerationconstant = 4f;
    public float maxspeed = 10f;
    float speed = 0;

    public float gravity = -9.81f;
    public float defaultJumpHeight = 3;
    float jumpHeight = 0;
    float amountOfJumpsSinceGrounded;
    public float jumpAtrophy;

    Vector3 velocity;
    Vector3 lastdirection, currentdirection;
    bool isGrounded, outofjumps;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        lastdirection = currentdirection;
        anim.SetFloat("Speed", speed);
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("VerticalSpeed", velocity.y);
        anim.SetBool("Pop", outofjumps);
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded == true)
        {
            jumpHeight = defaultJumpHeight;
            amountOfJumpsSinceGrounded = 0;
            outofjumps = false;
            anim.SetBool("Puffed", false);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && outofjumps == false)
        {
            if (isGrounded == false)
            {
                if (amountOfJumpsSinceGrounded < 6)
                {
                    jumpHeight -= jumpAtrophy;
                }
                else
                {
                    outofjumps = true;
                    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                }
            }
            amountOfJumpsSinceGrounded++;
            if (amountOfJumpsSinceGrounded > 0)
            {
                anim.SetBool("Puffed", true);
                anim.Play(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 0.0f);
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            currentdirection = moveDir;
            if (speed < maxspeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            if (speed > 0)
            {
                speed -= acceleration * decelerationconstant * Time.deltaTime;
                controller.Move(lastdirection.normalized * speed * Time.deltaTime);
            }
        }
    }
}
