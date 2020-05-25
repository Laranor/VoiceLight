using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform target;
    public Transform monsterTarget;
    public List<Transform> monsterDestination;
    int i = 0;
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
        monsterTarget = monsterDestination[0];
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
    private void OnTriggerStay(Collider other)
    {
        if (other.transform == monsterDestination[i] && ((other.transform.position - transform.position).magnitude) <= 3)
        {
            i += 1;
            if (i == monsterDestination.Count)
                i = 0;

            monsterTarget = monsterDestination[i];
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Avatar")
        {
            collision.gameObject.GetComponent<Death>().DeathReset(gameObject.transform);
        }
    }
}