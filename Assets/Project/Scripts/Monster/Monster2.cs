using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster2 : MonoBehaviour
{
    public Transform target;
    public Transform monsterTarget;
    public List<Transform> monsterDestination;
    public Transform lastAvatarPosition;
    int i = 0;
    Vector3 destination;
    NavMeshAgent agent;

    public Light avatarLight;
    public GameObject avatar;

    public float intensityGoal = 1;
    [SerializeField] private float detectedIntensity;

    public float chasingSpeed;
    public float baseSpeed;

    public bool chasing;
    public float distance;

    public Renderer crystalFront;
    public Vector4 colorCrystal;
    public float maxValue;
    public float changeColor;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        monsterTarget = monsterDestination[0];
    }

    void Update()
    {
        detectedIntensity = avatarLight.intensity - (avatarLight.intensity * ((avatarLight.gameObject.transform.position - transform.position).magnitude / avatarLight.range));
        distance = (avatar.transform.position - transform.position).magnitude;
        if(distance < maxValue)
        {
            if (colorCrystal.x < maxValue)
                colorCrystal = new Vector4(-distance + maxValue + 2, -distance + maxValue + 2 - changeColor, 0, 0);
        }
        else
        {
            colorCrystal = new Vector4(2, 2, 0, 0);
        }
        crystalFront.material.SetColor("_EmissionColor", colorCrystal);
        if (detectedIntensity >= intensityGoal)
        {
            target = avatar.transform;
            agent.speed = chasingSpeed;
            lastAvatarPosition.position = avatar.transform.position;
            chasing = true;
        }
        if(chasing)
        {
            if(changeColor < -distance + maxValue + 2)
                changeColor += 10 * Time.deltaTime;
            if (detectedIntensity < intensityGoal)
            {
                target = lastAvatarPosition;
                if (((lastAvatarPosition.transform.position - transform.position).magnitude) <= 6)
                    chasing = false;
            }
            if (distance <= 4)
            {
                avatar.GetComponent<Death>().DeathReset();
            }
        }
        else
        {
            if(changeColor > 0)
                changeColor -= 10 * Time.deltaTime;
            target = monsterTarget;
            agent.speed = baseSpeed;
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
        if (other.transform == monsterDestination[i] && ((other.transform.position - transform.position).magnitude) <= 6)
        {
            i += 1;
            if (i == monsterDestination.Count)
                i = 0;

            monsterTarget = monsterDestination[i];
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Avatar")
        {
            collision.gameObject.GetComponent<Death>().DeathReset();
        }
    }*/
}
