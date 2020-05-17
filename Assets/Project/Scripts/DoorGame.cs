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
        }
        if (lockNum == 2)
        {
            if (crystal2.transform.position.y > min2 && crystal2.transform.position.y < max2)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
        }
        if (lockNum == 3)
        {
            if (crystal3.transform.position.y > min3 && crystal3.transform.position.y < max3)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
        }
        if (lockNum == 4)
        {
            if (crystal4.transform.position.y > min4 && crystal4.transform.position.y < max4)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;
        }
        if (timer >= timeTarget)
        {
            lockNum += 1;
            timer = 0;
        }
        if (lockNum >= 5)
            open = true;
        if (open)
            door.transform.position += new Vector3(0, -0.05f, 0);

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
        if (pitchValue > seuilPitch && pitchValue > 0)
        {
            if (crystal.transform.position.y < maxHeight)
            {
                crystal.transform.position += new Vector3(0, 0.5f + speed, 0) * Time.deltaTime;
            }
        }
        if (pitchValue <= seuilPitch && pitchValue > 0)
        {
            if (crystal.transform.position.y > minHeight)
            {
                crystal.transform.position -= new Vector3(0, 0.5f + speed, 0) * Time.deltaTime;
            }
        }
    }

}

