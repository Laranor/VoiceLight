using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform target;
    public Transform monsterTarget;
    Vector3 destination;
    NavMeshAgent agent;
    float soundDetection;
    SoundDetection detection;
    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        detection = GetComponent<SoundDetection>();
        destination = agent.destination;
    }

    void Update()
    {
        if(detection.targets.Count > 0)
        {
            target = detection.GetLoudestObject(detection.targets).transform;
            if (target.GetComponentInChildren<Light>())
            {
                if (target.GetComponentInChildren<Light>().intensity < 1)
                    target = monsterTarget;
            }
        }
        if (detection.targets.Count == 0)
        {
            target = monsterTarget;
        }
        // Update destination if the target moves one unit
        if (Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
    }
}