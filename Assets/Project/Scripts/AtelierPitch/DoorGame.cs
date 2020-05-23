using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGame : MonoBehaviour
{
    public AvatarLighting avatar;

    [SerializeField] private float pitchValue;
    [SerializeField] private float dbValue;

    public float maxHeight;
    public float minHeight;
    public float seuilPitch;
    public float speed;

    public GameObject crystal1;
    public GameObject crystal2;
    public GameObject crystal3;
    public GameObject crystal4;

    [SerializeField] private int lockNum = 1;

    public float min1;
    public float max1;
    public float min2;
    public float max2;
    public float min3;
    public float max3;
    public float min4;
    public float max4;

    private float timer;
    public float timeTarget;

    public bool open = false;

    public GameObject door;

    public Renderer crystale1;
    public Renderer crystale2;
    public Renderer crystale3;
    public Renderer crystale4;

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;

    public Transform receptacle1;
    public Transform receptacle2;
    public Transform receptacle3;
    public Transform receptacle4;

    void Update()
    {
        if(lockNum == 1)
        {
            if (crystal1.transform.position.y > min1 && crystal1.transform.position.y < max1)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
            crystale1.material.EnableKeyword("_EMISSION");
            crystale2.material.DisableKeyword("_EMISSION");
            crystale3.material.DisableKeyword("_EMISSION");
            crystale4.material.DisableKeyword("_EMISSION");
        }
        if (lockNum == 2)
        {
            if (crystal2.transform.position.y > min2 && crystal2.transform.position.y < max2)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
            crystale2.material.EnableKeyword("_EMISSION");
        }
        if (lockNum == 3)
        {
            if (crystal3.transform.position.y > min3 && crystal3.transform.position.y < max3)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
            crystale3.material.EnableKeyword("_EMISSION");
        }
        if (lockNum == 4)
        {
            if (crystal4.transform.position.y > min4 && crystal4.transform.position.y < max4)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
            crystale4.material.EnableKeyword("_EMISSION");
        }
        if (timer >= timeTarget)
        {
            if(lockNum == 1)
            {
                anim1.SetBool("Lock", true);
                crystal1.transform.position = receptacle1.position + new Vector3(0,0,0.192f);
            }
            if (lockNum == 2)
            {
                anim2.SetBool("Lock", true);
                crystal2.transform.position = receptacle2.position + new Vector3(0, 0, 0.192f);
            }
            if (lockNum == 3)
            {
                anim3.SetBool("Lock", true);
                crystal3.transform.position = receptacle3.position + new Vector3(0, 0, 0.192f);
            }
            if (lockNum == 4)
            {
                anim4.SetBool("Lock", true);
                crystal4.transform.position = receptacle4.position + new Vector3(0, 0, 0.192f);
            }
            lockNum += 1;
            timer = 0;
        }
        if (lockNum >= 5)
            open = true;
        if (open)
            door.transform.position += new Vector3(0, -0.05f, 0);
        if(door.transform.position.y < -80)
        {
            Destroy(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            pitchValue = avatar.PitchValue;
            dbValue = avatar.DbValue;
            speed = (dbValue + 80) / 80;
            if (lockNum == 1)
            {
                CursorMove(crystal1);
            }
            if (lockNum == 2)
            {
                CursorMove(crystal2);
            }
            if (lockNum == 3)
            {
                CursorMove(crystal3);
            }
            if (lockNum == 4)
            {
                CursorMove(crystal4);
            }
        }
    }

    private void CursorMove(GameObject crystal)
    {
        if (pitchValue > seuilPitch)
        {
            if (crystal.transform.position.y < maxHeight)
            {
                crystal.transform.position += new Vector3(0, 0.5f + speed, 0) * Time.deltaTime;
            }
        }
        if (pitchValue <= seuilPitch && dbValue > -50)
        {
            if (crystal.transform.position.y > minHeight)
            {
                crystal.transform.position -= new Vector3(0, 0.5f + speed, 0) * Time.deltaTime;
            }
        }
    }

}

