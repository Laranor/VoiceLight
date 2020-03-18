using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCube : MonoBehaviour
{
    public AvatarLighting avatar;

    public Lock2 lock1;
    public Lock2 lock2;
    public Lock2 lock3;
    public Lock2 lock4;
    public Lock2 lock5;

    [SerializeField] private float value;
    private void Update()
    {
        value = avatar.PitchValue;
    }
    private void Start()
    {
        if (gameObject.name == "LightCube1")
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        if (gameObject.name == "LightCube2")
            GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        if (gameObject.name == "LightCube3")
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        if (gameObject.name == "LightCube4")
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        if (gameObject.name == "LightCube5")
            GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0.5f, 0, 1, 1));
    }
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.name == "LightCube1" && ((other.transform.position - transform.position).magnitude <= 4))
        {
            if(lock1.locked && avatar.PitchValue < 500 && avatar.PitchValue > 0)
            {
                lock1.Unlocking();
                Material mymat = GetComponent<Renderer>().material;
                mymat.EnableKeyword("_EMISSION");
                mymat.SetColor("_EmissionColor", Color.red);
                enabled = false;
            }
        }
        if (gameObject.name == "LightCube2" && ((other.transform.position - transform.position).magnitude <= 4))
        {
            if (lock2.locked && avatar.PitchValue < 1000 && avatar.PitchValue > 500)
            {
                lock2.Unlocking();
                Material mymat = GetComponent<Renderer>().material;
                mymat.EnableKeyword("_EMISSION");
                mymat.SetColor("_EmissionColor", Color.yellow);
                enabled = false;
            }
        }
        if (gameObject.name == "LightCube3" && ((other.transform.position - transform.position).magnitude <= 4))
        {
            if (lock3.locked && avatar.PitchValue < 1500 && avatar.PitchValue > 1000)
            {
                lock3.Unlocking();
                Material mymat = GetComponent<Renderer>().material;
                mymat.EnableKeyword("_EMISSION");
                mymat.SetColor("_EmissionColor", Color.green);
                enabled = false;
            }
        }
        if (gameObject.name == "LightCube4" && ((other.transform.position - transform.position).magnitude <= 4))
        {
            if (lock4.locked && avatar.PitchValue < 2000 && avatar.PitchValue > 1500)
            {
                lock4.Unlocking();
                Material mymat = GetComponent<Renderer>().material;
                mymat.EnableKeyword("_EMISSION");
                mymat.SetColor("_EmissionColor", Color.blue);
                enabled = false;
            }
        }
        if (gameObject.name == "LightCube5" && ((other.transform.position - transform.position).magnitude <= 4))
        {
            if (lock5.locked && avatar.PitchValue < 2500 && avatar.PitchValue > 2000)
            {
                lock5.Unlocking();
                Material mymat = GetComponent<Renderer>().material;
                mymat.EnableKeyword("_EMISSION");
                mymat.SetColor("_EmissionColor", new Vector4(0.5f,0,1,1));
                enabled = false;
            }
        }
    }
}
