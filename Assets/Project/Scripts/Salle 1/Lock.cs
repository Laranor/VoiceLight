using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool locked = true;
    public Light avatarLight;
    public Light cubeLight;
    [SerializeField] private float intensity;
    [SerializeField] private float time = 0;

    [SerializeField] private float timeTarget = 1;
    [SerializeField] private float intensityTarget = 2;

    public Lock lock1;
    public Lock lock2;
    public Lock lock3;
    public Lock lock4;
    public Lock lock5;

    private void OnTriggerStay(Collider other)
    {
        if(gameObject.name == "Cylinder1")
        {
            Unlocking(other.gameObject);
            if (cubeLight.intensity > intensityTarget)
                locked = false;
            if (transform.position.x <= -1.3f && !locked)
                transform.position += new Vector3(0.01f, 0, 0);
        }
        if (gameObject.name == "Cylinder2")
        {
            if(!lock1.locked)
            {
                Unlocking(other.gameObject);
                
                if (transform.position.x <= -1.3f && !locked)
                    transform.position += new Vector3(0.01f, 0, 0);
            }
        }
        if (gameObject.name == "Cylinder3")
        {
            if (!lock2.locked)
            {
                Unlocking(other.gameObject);
                if (transform.position.x <= -1.3f && !locked)
                    transform.position += new Vector3(0.01f, 0, 0);
            }
        }
        if (gameObject.name == "Cylinder4")
        {
            if (!lock3.locked)
            {
                Unlocking(other.gameObject);
                if (transform.position.x <= -1.3f && !locked)
                    transform.position += new Vector3(0.01f, 0, 0);
            }
        }
        if (gameObject.name == "Cylinder5")
        {
            if (!lock4.locked)
            {
                Unlocking(other.gameObject);
                if (transform.position.x <= -1.3f && !locked)
                    transform.position += new Vector3(0.01f, 0, 0);
            }
        }
    }
    private void Unlocking (GameObject light)
    {
        if (locked && ((light.transform.position - transform.position).magnitude <= 4))
        {
            if (light.name == "Light")
            {
                intensity = avatarLight.intensity - (avatarLight.intensity * ((light.transform.position - transform.position).magnitude / avatarLight.range));
                if (intensity > intensityTarget)
                {

                    time += Time.deltaTime;
                    if (time > timeTarget)
                    {
                        locked = false;
                    }
                }
            }
            else
                time = 0;
        }
    }
}
