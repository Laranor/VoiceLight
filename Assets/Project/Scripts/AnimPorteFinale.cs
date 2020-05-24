using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPorteFinale : MonoBehaviour
{
    public Camera avatarCam;
    public Camera finalCam;

    public PlayerMovement move;
    public MouseLook mouse;
    public AvatarLighting avatar;

    public Light avatarLight;

    private float timer;
    public float timeToActivate;
    // Start is called before the first frame update
    public bool cinematic;

    public Light spot;

    public Animator anim;

    public Renderer crystal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cinematic)
        {
            timer += Time.deltaTime;
            move.enabled = false;
            mouse.enabled = false;
            avatar.enabled = false;
            avatarLight.range = avatarLight.intensity * 5;
            avatarLight.intensity -= 5f * Time.deltaTime;
            Color finalValue = avatarLight.color * avatarLight.intensity / 2;
            crystal.material.SetColor("_EmissionColor", finalValue);

            if (timer > 1 && timer < timeToActivate)
            {
                finalCam.enabled = true;
                avatarCam.enabled = false;
                if(spot.range <= 20)
                    spot.range += 10 * Time.deltaTime;
                anim.SetBool("On", true);
            }
            if (timer > timeToActivate)
            {
                if (spot.range > 0)
                    spot.range -= 10 * Time.deltaTime;
                anim.SetBool("On", false);
            }
            if (timer > timeToActivate + 1)
            {
                EndCinematic();
            }
        }
    }

    public void EndCinematic()
    {
        move.enabled = true;
        mouse.enabled = true;
        avatar.enabled = true;
        finalCam.enabled = false;
        avatarCam.enabled = true;
        cinematic = false;
        timer = 0;
        spot.range = 0;
    }
}
