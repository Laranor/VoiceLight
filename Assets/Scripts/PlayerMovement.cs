using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;

    private Rigidbody rb;
    private Vector3 inputs = Vector3.zero;
    
    public bool isGrounded;
    public float groundDistance = 0.2f;
    public Transform groundCheck;
    public LayerMask dontReloadMask;

    public bool isBeingLaunched;
    public float launchTime;
    private float launchTimer;

    public float fallSpeed = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isGrounded)    
        {
            if (transform.position.y < -70)
                fallSpeed += 10 * Time.deltaTime;
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
            fallSpeed += 0.5f * Time.deltaTime;
            
        }
        else
            fallSpeed = 1;
            
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, dontReloadMask);
        
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);

        if (isBeingLaunched)
        {
            launchTimer += Time.deltaTime;
            
            if (launchTimer >= launchTime)
            {
                isBeingLaunched = false;
                launchTimer = 0;
            }
        }
    }
    
    void FixedUpdate()
    {
        inputs = transform.TransformDirection(inputs);
        
        rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
        
        //Air Movement
        if (Input.GetButton("Vertical") && !isGrounded && !isBeingLaunched)
        {
            Vector2 velocity2 = new Vector2(rb.velocity.x, rb.velocity.z);
            Vector2 facing2 = new Vector2(transform.forward.x, transform.forward.z);
            facing2.Normalize();

            Vector2 newVelocity2 = facing2 * velocity2.magnitude;
            
            Vector3 newVelocity = new Vector3(newVelocity2.x, 0, newVelocity2.y);
            newVelocity = Vector3.Lerp(rb.velocity, newVelocity, 0.1f);
            
            newVelocity.y = rb.velocity.y;
            
            rb.velocity = newVelocity;
        }
    }
}

