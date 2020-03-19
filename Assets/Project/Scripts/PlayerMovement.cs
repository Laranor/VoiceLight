using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 inputs = Vector3.zero;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance = 0.2f;
    public Transform groundCheck;
    public LayerMask dontReloadMask;

    [SerializeField] private bool isBeingLaunched;
    [SerializeField] private float launchTime = 0;
    private float launchTimer;

    [SerializeField] private float fallSpeed = 10;

    private FMOD.Studio.EventInstance walkSound;
    [SerializeField] private bool walking = true;

    public Transform checkpoint;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        walkSound = FMODUnity.RuntimeManager.CreateInstance("event:/01_A_IMPLEMENTER/Walk_01");
    }

    void Update()
    {
        if (transform.position.y <= -20)
        {
            transform.position = checkpoint.position;
        }
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
        walkSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (Input.GetKey(KeyCode.Z) && isGrounded || Input.GetKey(KeyCode.Q) && isGrounded || Input.GetKey(KeyCode.S) && isGrounded || Input.GetKey(KeyCode.D) && isGrounded)
        {
            if(!walking)
            {
                walkSound.start();
                walking = true;
            }
        }
        else
        {
            walking = false;
            walkSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
            
        if(Input.GetKey(KeyCode.LeftControl))
        {
            speed = 7.5f;
            walkSound.setParameterByName("Parameter 3", 1);
        }
        else
        {
            speed = 5f;
            walkSound.setParameterByName("Parameter 3", 0);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            FMODUnity.RuntimeManager.PlayOneShot("event:/01_A_IMPLEMENTER/Jump_01", gameObject.transform.position);
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/01_A_IMPLEMENTER/Land_01", gameObject.transform.position);
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

