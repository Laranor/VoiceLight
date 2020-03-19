using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte2 : MonoBehaviour
{
    [SerializeField] private float timeWait = 0;
    [SerializeField] private bool sound = false;

    [SerializeField] private float timeToStop = 0;

    public Lock2 lock1;
    public Lock2 lock2;
    public Lock2 lock3;
    public Lock2 lock4;
    public Lock2 lock5;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!lock1.locked && !lock2.locked && !lock3.locked && !lock4.locked && !lock5.locked)
        {
            if (!sound)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/01_A_IMPLEMENTER/Door_Open_01", transform.position);
                sound = true;
            }
            timeWait += Time.deltaTime;
            if (timeWait > 2.5f)
            {
                gameObject.transform.Rotate(new Vector3(0, 0.285f, 0), Space.World);
                timeToStop += Time.deltaTime;
                if (timeToStop >= 8.5f)
                {
                    enabled = false;
                    lock1.enabled = false;
                    lock2.enabled = false;
                    lock3.enabled = false;
                    lock4.enabled = false;
                    lock5.enabled = false;
                }
            }
        }

    }
}