using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHero : MonoBehaviour
{
    public AvatarLighting avatar;

    [SerializeField] private float pitchValue;
    [SerializeField] private float dbValue;

    public float seuilPitch;

    public Renderer[] blueCrystals;
    public Renderer[] redCrystals;

    public Renderer[] crystals1;
    public Renderer[] crystals2;
    public Renderer[] crystals3;
    public Renderer[] crystals4;
    public Renderer[] crystals5;
    public Renderer[] crystals6;
    public Renderer[] crystals7;

    [SerializeField] private int lockNum = 1;
    public bool open = false;
    public GameObject door;

    private float timer;
    public float timerReset;

    private bool colorBlue = true;
    public EndCrystal end; 
    void Start()
    {

    }

    private void Update()
    {
        if (lockNum >= 8)
            open = true;
        if (open)
            end.baseOn = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Avatar" && !open)
        {
            if (colorBlue)
            {
                for (int i = 0; i < blueCrystals.Length; i++)
                {
                    blueCrystals[i].material.EnableKeyword("_EMISSION");
                }
                for (int i = 0; i < redCrystals.Length; i++)
                {
                    redCrystals[i].material.DisableKeyword("_EMISSION");
                }
            }
            else
            {
                for (int i = 0; i < blueCrystals.Length; i++)
                {
                    blueCrystals[i].material.DisableKeyword("_EMISSION");
                }
                for (int i = 0; i < redCrystals.Length; i++)
                {
                    redCrystals[i].material.EnableKeyword("_EMISSION");
                }
            }
            pitchValue = avatar.PitchValue;
            dbValue = avatar.DbValue;
            timer += Time.deltaTime;
            if (timer >= timerReset)
            {
                if (colorBlue)
                {
                    if (pitchValue > seuilPitch)
                    {
                        Unlock();
                    }
                    else
                        ResetLock();
                }
                else
                {
                    if (pitchValue <= seuilPitch && dbValue > -50)
                    {
                        Unlock();
                    }
                    else
                        ResetLock();
                }
                timer = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!open)
        {
            for (int i = 0; i < blueCrystals.Length; i++)
            {
                blueCrystals[i].material.DisableKeyword("_EMISSION");
            }
            for (int i = 0; i < redCrystals.Length; i++)
            {
                redCrystals[i].material.DisableKeyword("_EMISSION");
            }
            ResetLock();
            timer = 0;
        }
    }

    private void Unlock()
    {
        if (lockNum == 1)
        {
            for (int i = 0; i < crystals1.Length; i++)
            {
                crystals1[i].material.EnableKeyword("_EMISSION");
            }
        }
        if (lockNum == 2)
        {
            for (int i = 0; i < crystals2.Length; i++)
            {
                crystals2[i].material.EnableKeyword("_EMISSION");
            }
        }
        if (lockNum == 3)
        {
            for (int i = 0; i < crystals3.Length; i++)
            {
                crystals3[i].material.EnableKeyword("_EMISSION");
            }
        }
        if (lockNum == 4)
        {
            for (int i = 0; i < crystals4.Length; i++)
            {
                crystals4[i].material.EnableKeyword("_EMISSION");
            }
        }
        if (lockNum == 5)
        {
            for (int i = 0; i < crystals5.Length; i++)
            {
                crystals5[i].material.EnableKeyword("_EMISSION");
            }
        }
        if (lockNum == 6)
        {
            for (int i = 0; i < crystals6.Length; i++)
            {
                crystals6[i].material.EnableKeyword("_EMISSION");
            }
        }
        if (lockNum == 7)
        {
            for (int i = 0; i < crystals7.Length; i++)
            {
                crystals7[i].material.EnableKeyword("_EMISSION");
            }
        }
        lockNum += 1;
        SetLock();
    }

    private void ResetLock()
    {
        for (int i = 0; i < crystals1.Length; i++)
        {
            crystals1[i].material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < crystals2.Length; i++)
        {
            crystals2[i].material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < crystals3.Length; i++)
        {
            crystals3[i].material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < crystals4.Length; i++)
        {
            crystals4[i].material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < crystals5.Length; i++)
        {
            crystals5[i].material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < crystals6.Length; i++)
        {
            crystals6[i].material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < crystals7.Length; i++)
        {
            crystals7[i].material.DisableKeyword("_EMISSION");
        }

        lockNum = 1;
        colorBlue = true;
    }

    private void SetLock()
    {
        if (lockNum == 1)
        {
            colorBlue = true;
        }
        if (lockNum == 2)
        {
            colorBlue = false;
        }
        if (lockNum == 3)
        {
            colorBlue = true;
        }
        if (lockNum == 4)
        {
            colorBlue = true;
        }
        if (lockNum == 5)
        {
            colorBlue = false;
        }
        if (lockNum == 6)
        {
            colorBlue = false;
        }
        if (lockNum == 7)
        {
            colorBlue = true;
        }
    }
}