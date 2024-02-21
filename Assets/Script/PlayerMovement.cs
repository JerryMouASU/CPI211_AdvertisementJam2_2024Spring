
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Cinemachine;

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
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private Vector3 spawnLocation;

    private void Start()
    {
        spawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastdirection = currentdirection;
        anim.SetFloat("Speed", Mathf.Clamp(speed,0,speed));
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("VerticalSpeed", velocity.y);
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded == true)
        {
            jumpHeight = defaultJumpHeight;
            amountOfJumpsSinceGrounded = 0;
            spawnLocation = transform.position;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * transform.localScale.x * -2 * gravity);
            if (amountOfJumpsSinceGrounded > 0)
            {
                anim.Play(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 0.0f);
            }
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.GetComponent<PlayerSizeController>().cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
            this.GetComponent<PlayerSizeController>().cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
            this.GetComponent<PlayerSizeController>().cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue = 0;
            this.GetComponent<PlayerSizeController>().cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0;
        }
        else
        {
            this.GetComponent<PlayerSizeController>().cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
            this.GetComponent<PlayerSizeController>().cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
        }


        if (transform.position.y < -30) transform.position = spawnLocation;
    }
}
