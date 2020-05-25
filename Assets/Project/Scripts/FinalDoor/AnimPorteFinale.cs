using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPorteFinale : MonoBehaviour
{
    public Camera avatarCam;
    public Camera finalCam;

    public FMODUnity.StudioListener audioListenerFMOD;
    public AudioListener audioListener;

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

    public bool hero;
    public bool pitch;
    public bool power;
    public bool golf;

    public Renderer[] heros;
    public Renderer[] pitchs;
    public Renderer[] powers;
    public Renderer[] golfs;

    public float maxIntensity;

    private float heroI;
    private float pitchI;
    private float powerI;
    private float golfI;

    public GameObject monstre4;
    public GameObject monstre5;
    public GameObject monstre6;
    public int nbMonstre= 3;
    void Start()
    {
        for (int i = 0; i < heros.Length; i++)
        {
            heros[i].material.SetColor("_EmissionColor", new Vector4(0, 0, 0, 0));
        }
        for (int i = 0; i < pitchs.Length; i++)
        {
            pitchs[i].material.SetColor("_EmissionColor", new Vector4(0, 0, 0, 0));
        }
        for (int i = 0; i < powers.Length; i++)
        {
            powers[i].material.SetColor("_EmissionColor", new Vector4(0, 0, 0, 0));
        }
        for (int i = 0; i < golfs.Length; i++)
        {
            golfs[i].material.SetColor("_EmissionColor", new Vector4(0, 0, 0, 0));
        }
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
                audioListener.enabled = true;
                audioListenerFMOD.enabled = true;
                if (spot.range <= 20)
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
        if (hero)
        {
            if (heroI <= maxIntensity)
            {
                heroI += maxIntensity / timeToActivate * Time.deltaTime;
                for (int i = 0; i < heros.Length; i++)
                {
                    heros[i].material.SetColor("_EmissionColor", new Vector4(0, 0, heroI, 0));
                }
            }
        }
        if (pitch)
        {
            if (pitchI <= maxIntensity)
            {
                pitchI += maxIntensity / timeToActivate * Time.deltaTime;
                for (int i = 0; i < pitchs.Length; i++)
                {
                    pitchs[i].material.SetColor("_EmissionColor", new Vector4(pitchI, 0, 0, 0));
                }
            }
        }
        if (golf)
        {
            if (golfI <= maxIntensity)
            {
                golfI += maxIntensity / timeToActivate * Time.deltaTime;
                for (int i = 0; i < golfs.Length; i++)
                {
                    golfs[i].material.SetColor("_EmissionColor", new Vector4(0, golfI, 0, 0));
                }
            }
        }
        if (power)
        {
            if (powerI <= maxIntensity)
            {
                powerI += maxIntensity / timeToActivate * Time.deltaTime;
                for (int i = 0; i < powers.Length; i++)
                {
                    powers[i].material.SetColor("_EmissionColor", new Vector4(powerI, powerI, powerI, 0));
                }
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
        audioListener.enabled = false;
        audioListenerFMOD.enabled = false;
        cinematic = false;
        timer = 0;
        spot.range = 0;
        if(nbMonstre == 3)
        {
            nbMonstre += 1;
            monstre4.SetActive(true);
        }
        else if (nbMonstre == 4)
        {
            nbMonstre += 1;
            monstre5.SetActive(true);
        }
        else if(nbMonstre == 5)
        {
            monstre6.SetActive(true);
        }

    }
}
