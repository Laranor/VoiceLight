using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPont : MonoBehaviour
{
    public Light avatarLight;

    [SerializeField] private float time = 0;

    [SerializeField] private float timeTarget = 1;
    [SerializeField] private float intensityTarget = 2;
    public float intensity;

    public Pont pont1;
    public Pont pont2;
    public Pont pont3;
    public Pont pont4;
    public Pont pont5;
    private void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        intensity = collision.gameObject.GetComponentInChildren<Light>().intensity;
        if (collision.gameObject.name == "Avatar")
        {
            //collision.gameObject.GetComponentInChildren<Light>().enabled = false;
            if (collision.gameObject.GetComponentInChildren<Light>().intensity >= intensityTarget)
            {
                time += Time.deltaTime;
                if (time > timeTarget)
                {
                    if (!pont1.on)
                    {
                        pont1.on = true;
                        time = 0;
                    }
                    else if (pont1.on && !pont2.on)
                    {
                        pont2.on = true;
                        time = 0;
                    }
                    else if (pont2.on && !pont3.on)
                    {
                        pont3.on = true;
                        time = 0;
                    }
                    else if (pont3.on && !pont4.on)
                    {
                        pont4.on = true;
                        time = 0;
                    }
                    else if (pont4.on && !pont5.on)
                    {
                        pont5.on = true;
                        time = 0;
                    }
                    else if (pont5.on && time > 2)
                    {
                        pont1.on = false;
                        pont2.on = false;
                        pont3.on = false;
                        pont4.on = false;
                        pont5.on = false;
                        time = 0;
                    }
                }
            }
            else
                time = 0;
        }
    }
}
