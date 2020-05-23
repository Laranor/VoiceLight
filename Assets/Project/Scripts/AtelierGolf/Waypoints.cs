using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    public AvatarLighting avatarScript;
    public GameObject avatar;

    [SerializeField] private float pitchValue;
    [SerializeField] private float dbValue;

    private float speed;
    public float diviseur;
    public bool open;

    public float distanceToActivate;

    void Update()
    {
        if ((avatar.transform.position - transform.position).magnitude < distanceToActivate)
        {
            pitchValue = avatarScript.PitchValue;
            dbValue = avatarScript.DbValue;
            speed = (dbValue + 80) / diviseur;
            if (currentWayPoint < this.wayPointList.Length)
            {
                if (targetWayPoint == null)
                    targetWayPoint = wayPointList[currentWayPoint];
                walk();
            }
        }
        if(currentWayPoint == this.wayPointList.Length)
        {
            open = true;
        }
    }

    void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            if (currentWayPoint < this.wayPointList.Length)
                currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}