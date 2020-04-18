using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    TestAudio script;
    public Porte opening;
    Animator anim;
    public GameObject cam;
    public PlayerMovement move;
    public MouseLook mouse;
    public AvatarLighting avatar;

    void Start()
    {
        script = GetComponentInChildren<TestAudio>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!opening.enabled)
        {
            script.musicInstance.start();
            anim.SetBool("Opened", true);
            cam.transform.rotation = new Quaternion(0,0,0,0);
            move.enabled = false;
            mouse.enabled = false;
            avatar.enabled = false;
        }
    }
    private void OnDestroy()
    {
        move.enabled = true;
        mouse.enabled = true;
        avatar.enabled = true;
    }
}
