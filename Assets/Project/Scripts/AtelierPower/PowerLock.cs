using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLock : MonoBehaviour
{
    public bool locked1 = true;
    public bool locked2 = true;
    public bool locked3 = true;

    public bool open;
    public GameObject door;
    [SerializeField] private float time = 0;

    public float timeTarget = 1;

    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;

    public AvatarLighting avatarScript;
    public Light avatarLight;

    [SerializeField] private float dbValue;
    public float dbTarget;

    public Renderer crystal1;
    public Renderer crystal2;
    public Renderer crystal3;

    public AnimPorteFinale cinematic;
    private void Start()
    {
        crystal1.material.DisableKeyword("_EMISSION");
        crystal2.material.DisableKeyword("_EMISSION");
        crystal3.material.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (open)
            door.transform.position += new Vector3(0, -0.05f, 0);
        if (door.transform.position.y < -10)
        {
            cinematic.cinematic = true;
            cinematic.power = true;
            Destroy(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Avatar")
        {
            if (locked1)
            {
                Unlocking(locked1, lock1, crystal1);
            }
            if (locked2 && !locked1)
            {
                Unlocking(locked2, lock2, crystal2);
            }
            if (locked3 && !locked2)
            {
                Unlocking(locked3, lock3, crystal3);
            }
        }
    }

    private void Unlocking(bool locked, GameObject lockBody, Renderer crystal)
    {
        if (locked)
        {
            dbValue = avatarScript.DbValue;
            crystal.material.EnableKeyword("_EMISSION");
            Color finalValue = Color.white * time;
            crystal.material.SetColor("_EmissionColor", finalValue);

            if (dbValue > dbTarget)
            {
                time += Time.deltaTime;

                
                if (time > timeTarget)
                {
                    if(locked == locked1)
                    {
                        locked1 = false;
                        time = 0;
                    }
                    else if (locked == locked2)
                    {
                        locked2 = false;
                        time = 0;
                    }
                    else if (locked == locked3)
                    {
                        locked3 = false;
                        time = 0;
                        open = true;
                    }
                    
                }
                if (lockBody.transform.localScale.z >= 0.3f)
                    lockBody.transform.localScale -= new Vector3(0, 0, 2.7f / timeTarget) * Time.deltaTime;
            }
            else
            {
                if(time > 0)
                    time -= Time.deltaTime;
                if (lockBody.transform.localScale.z < 3f)
                    lockBody.transform.localScale += new Vector3(0, 0, 2.7f / timeTarget) * Time.deltaTime;
            }
        }
    }
}
