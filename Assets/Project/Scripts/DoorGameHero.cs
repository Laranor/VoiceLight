using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGameHero : MonoBehaviour
{
    public AvatarLighting avatar;

    [SerializeField] private float pitchValue;
    [SerializeField] private float dbValue;

    public float seuilPitch;

    public GameObject blueCrystal;
    private Material blueMat;
    public GameObject redCrystal;
    private Material redMat;

    public GameObject crystal1;
    private Material mat1;
    public GameObject crystal2;
    private Material mat2;
    public GameObject crystal3;
    private Material mat3;
    public GameObject crystal4;
    private Material mat4;
    public GameObject crystal5;
    private Material mat5;

    [SerializeField] private int lockNum = 1;
    public bool open = false;
    public GameObject door;

    private float timer;
    public float timerReset;

    private bool colorBlue;
    void Start()
    {
        blueMat = blueCrystal.GetComponentInChildren<Renderer>().material;
        redMat = redCrystal.GetComponentInChildren<Renderer>().material;
        mat1 = crystal1.GetComponentInChildren<Renderer>().material;
        mat2 = crystal2.GetComponentInChildren<Renderer>().material;
        mat3 = crystal3.GetComponentInChildren<Renderer>().material;
        mat4 = crystal4.GetComponentInChildren<Renderer>().material;
        mat5 = crystal5.GetComponentInChildren<Renderer>().material;
    }

    private void Update()
    {
        if(lockNum >= 6)
            open = true;
        if (open)
            door.transform.position += new Vector3(0, -0.05f, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Avatar")
        {
            pitchValue = avatar.PitchValue;
            dbValue = avatar.DbValue;
            timer += Time.deltaTime;
            if(timer >= timerReset)
            {
                if(colorBlue)
                {
                    blueMat.EnableKeyword("_EMISSION");
                    redMat.DisableKeyword("_EMISSION");
                    colorBlue = false;

                    if (pitchValue > seuilPitch)
                    {
                        Debug.Log("yes");
                        Unlock();
                    }
                    else
                        ResetLock();
                }
                else
                {
                    redMat.EnableKeyword("_EMISSION");
                    blueMat.DisableKeyword("_EMISSION");
                    colorBlue = true;
                    if (pitchValue <= seuilPitch && dbValue > -40)
                    {
                        Debug.Log("no");
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
        redMat.DisableKeyword("_EMISSION");
        blueMat.DisableKeyword("_EMISSION");
        ResetLock();
        timer = 0;
    }

    private void Unlock()
    {
        if (lockNum == 1)
        {
            mat1.EnableKeyword("_EMISSION");
        }
        if (lockNum == 2)
        {
            mat2.EnableKeyword("_EMISSION");
        }
        if (lockNum == 3)
        {
            mat3.EnableKeyword("_EMISSION");
        }
        if (lockNum == 4)
        {
            mat4.EnableKeyword("_EMISSION");
        }
        if (lockNum == 5)
        {
            mat5.EnableKeyword("_EMISSION");
        }
        lockNum += 1;
    }

    private void ResetLock()
    {
        mat1.DisableKeyword("_EMISSION");
        mat2.DisableKeyword("_EMISSION");
        mat3.DisableKeyword("_EMISSION");
        mat4.DisableKeyword("_EMISSION");
        mat5.DisableKeyword("_EMISSION");
        lockNum = 1;
    }
}
