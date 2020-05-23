using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfDoor : MonoBehaviour
{
    public GameObject door;

    public AvatarLighting avatarScript;

    [SerializeField] private float dbValue;

    private bool open;
    public Animator anim;

    private float timer;

    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    public GameObject crystal;

    [SerializeField] private float speed;
    public float diviseur;
    public float dbSeuil;

    public bool walking;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            timer += Time.deltaTime;
            anim.SetBool("Lock", true);
            if(timer > 2)
            {
                door.transform.position += new Vector3(0, -0.05f, 0);

                if (door.transform.position.y < -80)
                {
                    Destroy(this);
                }
            }
        }

        if (currentWayPoint == this.wayPointList.Length)
        {
            open = true;
        }

        if(!open)
        {
            if (dbValue > dbSeuil)
            {
                walking = true;
            }
            else
            {
                walking = false;
            }
            if (currentWayPoint < this.wayPointList.Length)
            {
                if (targetWayPoint == null)
                    targetWayPoint = wayPointList[currentWayPoint];
                walk();
            }
            if (walking)
            {
                speed = (dbValue + 80) / diviseur;
            }
            else
            {
                speed += 2f * Time.deltaTime;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            dbValue = avatarScript.DbValue;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        dbValue = -80;
    }
    void walk()
    {
        // move towards the target
        crystal.transform.position = Vector3.MoveTowards(crystal.transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (crystal.transform.position == targetWayPoint.position)
        {
            if(walking)
            {
                if (currentWayPoint < this.wayPointList.Length)
                    currentWayPoint++;
            }
            else
            {
                if(currentWayPoint > 0)
                    currentWayPoint--;
            }
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
