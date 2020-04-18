using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform target;
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
        target = detection.GetLoudestObject(detection.targets).transform;
        // Update destination if the target moves one unit
        if (Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
    }
}