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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
    }
}

