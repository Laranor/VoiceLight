using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repere : MonoBehaviour
{
    public Renderer crystal;
    public GameObject avatar;
    public AvatarLighting avatarLight;

    private float timerActivation;
    public float timeToActivate;

    private float timerOn;
    public float timeToReset;

    public float distanceMin;

    public bool on;

    void Start()
    {
        crystal.material.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if ((avatar.transform.position - transform.position).magnitude < distanceMin && !on && avatarLight.DbValue > -50)
        {
            timerActivation += Time.deltaTime;
            if (timerActivation > timeToActivate)
            {
                on = true;
                crystal.material.EnableKeyword("_EMISSION");
            }
        }
        else
            timerActivation = 0;

        if (on && timerOn < timeToReset)
        {
            timerOn += Time.deltaTime;
        }
        if (timerOn >= timeToReset)
        {
            on = false;
            timerOn = 0;
            crystal.material.DisableKeyword("_EMISSION");
        }
    }
}
