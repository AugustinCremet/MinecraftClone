using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float runSpeed = 15.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3.0f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] AudioClip swimmingSound, walkingSound;
    Vector2 moveInput;
    Vector3 jumpVelocity = Vector3.zero;
    Rigidbody rb;
    CharacterController controller;
    AudioSource audioSource;
    bool jump;
    bool run;
    bool isGrounded;

    public void ReceiveInput(Vector2 _moveInput)
    {
        moveInput = _moveInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    public void OnRunPressed(float value)
    {
        if(value == 1.0f)
            run = true;
        else
            run = false;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Jump();
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity;
        if(run)
        {
            moveVelocity = (transform.right * moveInput.x + transform.forward * moveInput.y) * runSpeed;
            if(isGrounded)
                PlaySound(walkingSound, 3.0f);
        }
        else
        {
            moveVelocity = (transform.right * moveInput.x + transform.forward * moveInput.y) * speed;
            if(isGrounded)
                PlaySound(walkingSound, 2.0f);
        }
        if(moveVelocity.x == 0.0f && moveVelocity.z == 0.0f)
            audioSource.Pause();

        controller.Move(moveVelocity * Time.deltaTime);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        
        if(isGrounded)
        {
            jumpVelocity.y = 0.0f;
        }

        if(jump)
        {
            if(isGrounded)
            {
                jumpVelocity.y = Mathf.Sqrt(-2.0f * jumpHeight * gravity); 
            }
            jump = false;
        }

        jumpVelocity.y += gravity * Time.deltaTime;
        controller.Move(jumpVelocity * Time.deltaTime);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Water")
        {
            isGrounded = false;
            PlaySound(swimmingSound, 1.0f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Water")
        {
            isGrounded = true;
            audioSource.Pause();
        }
    }

    void PlaySound(AudioClip currentSound, float pitch)
    {
        audioSource.clip = currentSound;
        audioSource.pitch = pitch;
        if(!audioSource.isPlaying)
            audioSource.Play();
    }
}
